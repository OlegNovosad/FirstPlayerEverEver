using UnityEngine;
using System.Collections;
using CnControls;

public class Player : MonoBehaviour 
{
	public AudioClip[] moveSounds;				//1 of 2 Audio clips to play when player moves.
	public AudioClip mmmSound;
	public AudioClip manGetTired;
	public AudioClip collectSound;
	public AudioClip grandpaSound;
	public AudioClip cipo4kaSound;

	public Sprite spikeOff;
	public Sprite spikeOn;

	[SerializeField] private float maxSpeed = 2f;                    // The fastest the player can travel in the x axis.

    const float ceilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
    private Animator animator;            // Reference to the player's animator component.
    private bool facingRight = true;  // For determining which way the player is currently facing.

    public Vector3 targetPosition;

    float horizontal = 0;
    float vertical = 0;

	public GameObject [] level2Walls;
	public GameObject [] level3Walls;
	public GameObject [] level4Walls;
	public GameObject garlic;

	public float flowersNumber = Constants.FlowersCount; // magic number

	private bool phraseUsed;

	public Sprite vampire;
	public Sprite princess;

	public string[] phrases = {
		"I wanna poooo..."
	};
		
	private bool hasSpear = false;

	private AudioClip randomMoveSound;
	
	//Start overrides the Start function of MovingObject
	void Start ()
	{
		//Get a component reference to the Player's animator component
		animator = GetComponent<Animator>();
		targetPosition = transform.position;

		randomMoveSound = moveSounds[Random.Range(0, moveSounds.Length)];
	}

	private void Update ()
	{
		if (GameManager.instance.isPaused)
		{
			return;
		}

		//Get input from the input manager, round it to an integer and store in horizontal to set x axis move direction
		horizontal = CnInputManager.GetAxisRaw ("Horizontal");
		
		//Get input from the input manager, round it to an integer and store in vertical to set y axis move direction
		vertical = CnInputManager.GetAxisRaw ("Vertical");

		//Check if we have a non-zero value for horizontal or vertical
		if (horizontal != 0 || vertical != 0)
		{
			animator.SetBool("IsMoving", true);
			Move(horizontal, vertical);
			SoundManager.instance.RandomizeSfx(randomMoveSound);
		}
		else
		{
			animator.SetBool("IsMoving", false);
		}
	}

	//OnTriggerEnter2D is sent when another object enters a trigger collider attached to this object (2D physics only).
	private void OnTriggerEnter2D(Collider2D other)
	{
		switch (other.tag)
		{
			case "Next":
				GameManager.instance.LoadNextLevel();
				break;
			case "Death":
				other.gameObject.GetComponent<SpriteRenderer>().sprite = spikeOn;
				StartCoroutine(GameManager.instance.GameOver());
				break;
			case "Flower":
				UIManager.instance.ShowTooltipMessage(Constants.FlowerMessage);
				SoundManager.instance.PlayPlayersSingle(mmmSound);
				break;
			case "Fire":
				UIManager.instance.ShowTooltipMessage(Constants.FireMessage);
				SoundManager.instance.PlayPlayersSingle(mmmSound);
				break;
			case "QuestFlower":
				flowersNumber--;
				Destroy(other.gameObject);
				SoundManager.instance.PlayPlayersSingle(collectSound);

				if (flowersNumber <= 0)
				{
					GameManager.instance.questState = Constants.QuestState.Done;
				}
				break;
			case "QuestRoom":
				if (GameManager.instance.questState != Constants.QuestState.Done)
				{
					GameManager.instance.questState = Constants.QuestState.InProgress;
				}

				if (GameManager.instance.isPoisoning) 
				{
					GameManager.instance.StopPoisoning();
				}
				else
				{
					GameManager.instance.StartPoisoning();
				}
				break;
			case "Spear":
				PlayerManager.instance.Damage(3);
				break;
			case "Wizard":
				SoundManager.instance.PlayPlayersSingle (grandpaSound);
				if (GameManager.instance.isFirstLevel) 
				{
					UIManager.instance.ShowModalDialogPanel ("This is the first time ever. I invented it and called the game.", "Where is my wife?", false, true);
					if (GameObject.Find ("wizard") && GameObject.Find ("wizard").activeSelf) 
					{
						GameObject.Find ("wizard").SetActive (false);
					}
				}
				else if (GameManager.instance.isLastLevel)
				{
					UIManager.instance.ShowModalDialogPanel ("Great choice! You Won in the first game Ever!", "Aww! My wife!");
					GameObject.Find("wizard").GetComponent<SpriteRenderer>().sprite = princess;
				}
				else 
				{
					if (GameManager.instance.questState == Constants.QuestState.Done)
					{
						UIManager.instance.ShowModalDialogPanel ("It was smelly wasn't it? How do you think those flowers grow?", "Urgh...");
						for (int i = 0; i < level2Walls.Length; i++) {
							Destroy (level2Walls[i]);
						}
						return;
					}

					if (GameManager.instance.questState == Constants.QuestState.None) 
					{
						GameManager.instance.questState = Constants.QuestState.Started;
						UIManager.instance.ShowModalDialogPanel ("It's quest time! Collect all flowers before you die.", "Ok");
						return;
					}

					if (GameManager.instance.questState == Constants.QuestState.Started) 
					{
						UIManager.instance.ShowModalDialogPanel ("Go do quest, you lazy boy.", "Ok");
						return;
					}

					if (GameManager.instance.questState == Constants.QuestState.InProgress) 
					{
						UIManager.instance.ShowModalDialogPanel ("Mmm...i can smell it.", "Ok");
						return;
					}
				}
				break;
			case "Phrase":
				if (!phraseUsed)
				{
					SoundManager.instance.PlayPlayersSingle(manGetTired);
					UIManager.instance.ShowTooltipMessage(phrases[Random.Range(0, phrases.Length)]);
					phraseUsed = true;
				}
				break;
			case "Chest":
				Chest chest = other.GetComponent<Chest>();

				if (!chest.isUsed)
				{
					chest.OpenChest();
				}
				break;
			case "Lock":
				if (PlayerManager.instance.hasKey) 
				{
					GameObject.Find("Lock").SetActive(false);
					UIManager.instance.ShowModalDialogPanel("Why would anyone try to unlock a lock hanging on the stones?", "I don'no...");
					for (int i = 0; i < level3Walls.Length; i++) 
					{
						Destroy (level3Walls[i]);
					}
					GameObject.Find("/Canvas/HUD/KeyImage").SetActive(false);
				} 
				else 
				{
					UIManager.instance.ShowTooltipMessage("Mmmm? Me don't know what this is.");
				}
			break;
			case "Bat":
				PlayerManager.instance.Damage(3);

				if (PlayerManager.instance.hasSpear)
				{
					GameManager.instance.DamageBat(10);
				}
				else
				{
					GameManager.instance.DamageBat(5);
				}
				
				if (other != null)
				{
					StartCoroutine(RestartTrigger(other));
				}
				break;
			case "Vampire":
				if (PlayerManager.instance.hasGarlic)
				{
					Destroy(GameManager.instance.vampire);
					for (int i = 0; i < level4Walls.Length; i++) {
						Destroy (level4Walls[i]);
					}
				}
				else
				{
					// TODO: Set conditions in game design on how much and when the vampire hurts player.
					PlayerManager.instance.Damage(6);
				}
				break;
			case "Princess":
				SoundManager.instance.PlayPlayersSingle (cipo4kaSound);
				UIManager.instance.ShowModalDialogPanel ("Honey, I knew you would save me. Now face your doom MU-HA-HA-HA-HA", "What?", true);
				GameObject.Find("princess").GetComponent<SpriteRenderer>().sprite = vampire;
				break;
			case "Exit":
				GameManager.instance.StartFromTheBeginning();
				break;
			case "Tablet":
				Tablet tablet = other.GetComponent<Tablet>();
				UIManager.instance.ShowTooltipMessage(tablet.setTabletMessage());
				break;
			default: break;
		}
	}

	IEnumerator RestartTrigger(Collider2D other)
	{
		yield return new WaitForSeconds(0.2f);
		other.isTrigger = false;
		yield return new WaitForSeconds(0.2f);
		other.isTrigger = true;
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		switch (other.tag)
		{
			case "Flower":
			case "Phrase":
			case "Fire":
			case "Sign":
			case "Lock":
			case "Tablet":
				UIManager.instance.HideTooltipMessage();
				break;
			default: break;
		}
	}

    private void Awake()
    {
        // Setting up references.
        animator = GetComponent<Animator>();
    }

    public void Move(float hMove, float vMove)
    {
		targetPosition = new Vector3(hMove, vMove, 0);

		//Move Player
		transform.Translate(targetPosition * maxSpeed * Time.deltaTime);

        // If the input is moving the player right and the player is facing left...
		if (hMove > 0 && !facingRight)
        {
            // ... flip the player.
            Flip();
        }
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (hMove < 0 && facingRight)
        {
            Flip();
        }
    }

	/// <summary>
	/// Flip the player.
	/// </summary>
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnDrawGizmos()
    {
		Gizmos.DrawLine(transform.position, transform.position + targetPosition);
    }
}
