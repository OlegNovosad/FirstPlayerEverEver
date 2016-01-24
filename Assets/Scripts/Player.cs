using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public AudioClip[] moveSounds;				//1 of 2 Audio clips to play when player moves.
	public AudioClip mmmSound;
	public AudioClip manGetTired;
	public AudioClip collectSound;
	public AudioClip grandpaSound;

	public Sprite spikeOff;
	public Sprite spikeOn;

	[SerializeField] private float maxSpeed = 2f;                    // The fastest the player can travel in the x axis.

    const float ceilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
    private Animator animator;            // Reference to the player's animator component.
    private bool facingRight = true;  // For determining which way the player is currently facing.

    public Ray movingRay;
    public Vector3 targetPosition;

    float horizontal = 0;
    float vertical = 0;

	public GameObject level2Wall;
	public GameObject level4Wall;
	public GameObject garlic;

	public float flowersNumber = Constants.FlowersCount; // magic number

	private bool phraseUsed;

	public string[] phrases = {
		"I wanna poooo..."
	};

	private bool hasGarlic = false;
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
		horizontal = Input.GetAxisRaw ("Horizontal");
		
		//Get input from the input manager, round it to an integer and store in vertical to set y axis move direction
		vertical = Input.GetAxisRaw ("Vertical");

		//Check if we have a non-zero value for horizontal or vertical
		if (horizontal != 0 || vertical != 0)
		{
			Move(horizontal, vertical);
			SoundManager.instance.RandomizeSfx(randomMoveSound);
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
				GameManager.instance.ShowTooltipMessage(Constants.FlowerMessage);
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

				GameManager.instance.StartPoisoning();
				break;
		case "Grandpa":
			SoundManager.instance.PlayPlayersSingle (grandpaSound);
			if (GameManager.instance.isFirstLevel) {
				GameManager.instance.ShowModalDialogPanel ("This is the first time ever. I invented it and called the game.", "Where is my wife?", false, true);
				if (GameObject.Find ("/Canvas/GameNameText") && GameObject.Find ("/Canvas/GameNameText").activeSelf) {
					GameObject.Find ("/Canvas/GameNameText").SetActive (false);
				}
				if (GameObject.Find ("wizard") && GameObject.Find ("wizard").activeSelf) {
					GameObject.Find ("wizard").SetActive (false);
				}
			}
//			else if (GameManager.instance.isLastLevel) {
//			}
			else {
				if (GameManager.instance.questState == Constants.QuestState.Done) {
					GameManager.instance.ShowModalDialogPanel ("Well done. You can continue your journey.", "Ok");
					Destroy (level2Wall);
					return;
				}

				if (GameManager.instance.questState == Constants.QuestState.None) {
					GameManager.instance.questState = Constants.QuestState.Started;
					GameManager.instance.ShowModalDialogPanel ("Your quests start here. Collect all flowers before you die. MUHAHHAHA.", "Ok");
					return;
				}

				if (GameManager.instance.questState == Constants.QuestState.Started) {
					GameManager.instance.ShowModalDialogPanel ("Go do quest, you lazy boy.", "Ok");
					return;
				}

				if (GameManager.instance.questState == Constants.QuestState.InProgress) {
					GameManager.instance.ShowModalDialogPanel ("Mmm...i can smell it.", "Ok");
					return;
				}
			}



				break;
			case "Phrase":
				if (!phraseUsed)
				{
					SoundManager.instance.PlayPlayersSingle(manGetTired);
					GameManager.instance.ShowTooltipMessage(phrases[Random.Range(0, phrases.Length)]);
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
				if (PlayerManager.instance.hasKey) {
					GameObject.Find ("Lock").SetActive (false);
					GameManager.instance.ShowModalDialogPanel ("Why would anyone try to unlock a lock hanging on the stones?", "I don'no...");
					GameObject.Find ("LevelHiddenPassageWall1").SetActive (false);
					GameObject.Find ("LevelHiddenPassageWall2").SetActive (false);
					GameObject.Find ("/Canvas/HUD/KeyImage").SetActive (false);
				} else {
					GameManager.instance.ShowTooltipMessage ("Mmmm? Me don't know what this is.");
				}
			break;
			case "Bat":
				PlayerManager.instance.Damage(3);

				if (hasSpear)
				{
				GameManager.instance.DamageBat(10);
				}
				else
				{
					GameManager.instance.DamageBat(5);
				}

				StartCoroutine(RestartTrigger(other));
				break;
			case "Vampire":
				if (hasGarlic)
				{
					Destroy(GameManager.instance.vampire);
					Destroy(level4Wall);
				}
				else
				{
					PlayerManager.instance.Damage(50);
				}
				break;
			case "Garlic":
				if (GameManager.instance.vampire.GetComponent<Vampire>().turn)
				{
					Destroy(garlic);
					hasGarlic = true;
					SoundManager.instance.PlayPlayersSingle(mmmSound);
				}
				break;

		case "Lvl1_dialog1":
			GameManager.instance.ShowModalDialogPanel ("Just explore the cave, young hero in this first game ever. Ever...", "mmm...");
			GameObject.Find ("/Level/Wall (4)/Floor1 (1)/messageObj");
				break;
			default: 
				
				break;
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
				GameManager.instance.HideTooltipMessage();		
				break;
			case "Phrase":
				GameManager.instance.HideTooltipMessage();
				break;
			default: 
				
				break;
		}
	}

    private void Awake()
    {
        // Setting up references.
        animator = GetComponent<Animator>();
    }


    private void FixedUpdate()
    {
        // Set the vertical animation
//		animator.SetFloat("vSpeed", Rigidbody2D.velocity.y);
    }

    public void Move(float hMove, float vMove)
    {
        // The Speed animator parameter is set to the absolute value of the horizontal input.
//        animator.SetFloat("Speed", Mathf.Abs(move));

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
            // ... flip the player.
            Flip();
        }
    }

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
