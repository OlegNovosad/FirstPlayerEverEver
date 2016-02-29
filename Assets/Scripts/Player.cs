using UnityEngine;
using System.Collections;

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

    private Animator animator;            // Reference to the player's animator component.
    public bool facingRight = true;  // For determining which way the player is currently facing.

    public Vector3 targetPosition;

    float horizontal = 0;
    float vertical = 0;

	public GameObject [] level2Walls;
	public GameObject [] level3Walls;
	public GameObject [] level4Walls;
	public GameObject garlic;

	public float questFlowers;

	private bool phraseUsed;

	public Sprite vampire;
	public Sprite princess;

	public string[] phrases = {
		"I wanna poooo..."
	};

	private AudioClip randomMoveSound;

    private void Awake()
    {
        // Setting up references.
        animator = GetComponent<Animator>();
    }
	
	//Start overrides the Start function of MovingObject
	void Start ()
	{
		//Get a component reference to the Player's animator component
		animator = GetComponent<Animator>();
		targetPosition = transform.position;

		questFlowers = GameObject.FindGameObjectsWithTag("QuestFlower").Length;
		randomMoveSound = moveSounds[Random.Range(0, moveSounds.Length)];
	}

	private void Update ()
	{
		if (GameManager.instance.isPaused)
		{
			return;
		}


		#if UNITY_ANDROID || UNITY_IOS

		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Moved || touch.phase ==  TouchPhase.Stationary)
			{
				Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));

	            if (touchPosition != transform.position)
	            {
					animator.SetBool("IsMoving", true);
					Move(touchPosition);
	            }
	            else
	            {
					animator.SetBool("IsMoving", false);
	            }
            }
        }

	    #endif

		#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX|| UNITY_WEBPLAYER

		//Get input from the input manager, round it to an integer and store in horizontal to set x axis move direction
		horizontal = Input.GetAxisRaw ("Horizontal");
		
		//Get input from the input manager, round it to an integer and store in vertical to set y axis move direction
		vertical = Input.GetAxisRaw ("Vertical");

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

		#endif
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
				Destroy(other.gameObject);
				StartCoroutine(UIManager.instance.ShowTooltipMessageWithDelay(Constants.FlowerMessage, 2f));
				SoundManager.instance.PlayPlayersSingle(mmmSound);
				SoundManager.instance.PlayOtherSingle(collectSound);
				PlayerManager.instance.AddFlower(1);
				break;
			case "Fire":
				UIManager.instance.ShowTooltipMessage(Constants.FireMessage);
				SoundManager.instance.PlayPlayersSingle(mmmSound);
				break;
			case "QuestFlower":
				questFlowers--;
				Destroy(other.gameObject);
				SoundManager.instance.PlayOtherSingle(collectSound);

				if (questFlowers <= 0)
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
				if (!PlayerManager.instance.hasSpear && !other.gameObject.GetComponent<Spear>().isThrown)
				{
					PlayerManager.instance.Damage(3);
					other.gameObject.transform.localPosition = new Vector3(other.gameObject.transform.localPosition.x, other.gameObject.transform.localPosition.y, gameObject.transform.position.z + .5f);
					other.gameObject.transform.SetParent(gameObject.transform);
					PlayerManager.instance.spearsInBack.Add(other.gameObject.GetComponent<Spear>());
				}
				break;
			case "Wizard":
				SoundManager.instance.PlayOldman();
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

    private void Move(Vector3 position)
    {
		// If the input is moving the player right and the player is facing left...
		if (position.x > transform.position.x && !facingRight)
        {
            // ... flip the player.
            Flip();
        }
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (position.x < transform.position.x && facingRight)
        {
            Flip();
        }

		transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime);
    }

    private void Move(float hMove, float vMove)
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
