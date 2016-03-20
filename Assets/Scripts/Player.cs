using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public AudioClip[] moveSounds;				//1 of 2 Audio clips to play when player moves.
	public AudioClip mmmSound;
	public AudioClip manGetTired;
	public AudioClip collectSound;
	public AudioClip grandpaSound;
	public AudioClip princessSound;


	public GameObject rightArm;
	public GameObject leftArm;

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

	private bool phraseUsed; //FIXME: review this thing prior to release
    public bool contactsWithSpear;
	public Spear contactedSpear;
    
    public Sprite vampire;
	public Sprite princess;

	public string[] phrases = {
		"I wanna poooo..."
	};

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
	
	void Start ()
	{
		targetPosition = transform.position;
		questFlowers = GameObject.FindGameObjectsWithTag("QuestFlower").Length;
	}

	private void Update ()
	{
		if (GameManager.instance.isPaused)
		{
			return;
		}
	}

	private void FixedUpdate()
	{
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

		horizontal = Input.GetAxisRaw ("Horizontal");
		vertical = Input.GetAxisRaw ("Vertical");

		if (horizontal != 0 || vertical != 0)
		{
			//			animator.SetBool("IsMoving", true);
			Move(horizontal, vertical);
			//			SoundManager.instance.RandomizeSfx(moveSounds[Random.Range(0, moveSounds.Length)]);
		}
		else
		{
			//			animator.SetBool("IsMoving", false);
		}

		#endif
	}

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
				//set spear to temp var on contact with spear to be able to pull it out
				contactedSpear = other.gameObject.GetComponent<Spear>();
                //If player is not pierced with spear - this contact is because spear is stuck somewhere.
                if (!PlayerManager.instance.spearPiercedPlayer)
                {
                    contactsWithSpear = true;
                    UIManager.instance.ShowTooltipMessage("Boobaraka is strong! He can pull it out!");
                }
                //If he has no spear and it is thrown - the Quest 2 and possibly other levels.
                else if (!PlayerManager.instance.hasSpear && !other.gameObject.GetComponent<Spear>().isThrown)
				{
                    PlayerManager.instance.Damage(3);
					other.gameObject.transform.localPosition = new Vector3(other.gameObject.transform.localPosition.x, other.gameObject.transform.localPosition.y, gameObject.transform.position.z + .5f);
					other.gameObject.transform.SetParent(gameObject.transform);
					PlayerManager.instance.spearsInBack.Add(other.gameObject.GetComponent<Spear>());
					PlayerManager.instance.spearPiercedPlayer = true;
				}
                //Set the HUD button to Pull Out state TEMPORARY!!!!! remove after finished with the spear throwing:

                    UIManager.instance.throwSpearButton.gameObject.SetActive(false);
                    UIManager.instance.pullOutSpearButton.gameObject.SetActive(true);

				break;
		case "Wizard":
			SoundManager.instance.PlayOldman ();
			if (TextManager.instance.currentLevel == 2) 
			{
				switch (GameManager.instance.questState) {
				case Constants.QuestState.Done:
					TextManager.instance.branch = 3;
					UIManager.instance.ShowModalDialogPanel ();
					break;
				case Constants.QuestState.None:
					GameManager.instance.questState = Constants.QuestState.Started;
					UIManager.instance.ShowModalDialogPanel ();//"It's quest time! Collect all flowers before you die.", "Ok");
					break;
				case Constants.QuestState.Started:
					UIManager.instance.ShowModalDialogPanel ();
					break;
				case Constants.QuestState.InProgress:
					UIManager.instance.ShowModalDialogPanel ();
					break;
				}
			} 
			else 
			{
				UIManager.instance.ShowModalDialogPanel ();
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
					UIManager.instance.ShowModalDialogPanel ();//"Why would anyone try to unlock a lock hanging on the stones?", "I don'no...");
					for (int i = 0; i < level3Walls.Length; i++) 
					{
						Destroy (level3Walls[i]);
					}
					GameObject.Find("/Canvas/HUD/KeyImage").SetActive(false); //FIXME: change this to reference GO in the class
				} 
				else 
				{
					UIManager.instance.ShowTooltipMessage("Mmmm? Me don't know what this is.");
				}
			break;
			case "Bat":
				PlayerManager.instance.Damage(3);
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
					PlayerManager.instance.Damage(3);
				}
				break;
			case "Princess":
				SoundManager.instance.PlayPlayersSingle (princessSound);
				UIManager.instance.ShowModalDialogPanel ();//"Honey, I knew you would save me. Now face your doom MU-HA-HA-HA-HA", "What?", true);
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
            case "Spear":
                contactsWithSpear = false;
                UIManager.instance.HideTooltipMessage();
                break;
            default: break;
		}
	}

	/// <summary>
	/// Move to the specified position.
	/// </summary>
	/// <param name="position">Position.</param>
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

    /// <summary>
    /// Move to new target position based on the specified hMove and vMove.
    /// </summary>
    /// <param name="hMove">H move.</param>
    /// <param name="vMove">V move.</param>
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
}
