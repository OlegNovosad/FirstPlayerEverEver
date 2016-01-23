using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public float restartLevelDelay = 1f;		//Delay time in seconds to restart level.
	public AudioClip[] moveSounds;				//1 of 2 Audio clips to play when player moves.
	public AudioClip mmmSound;
	public AudioClip manGetTired;
	public AudioClip deathSound;

	public AudioClip gameOverSound;				//Audio clip to play when player dies.

	[SerializeField] private float maxSpeed = 2f;                    // The fastest the player can travel in the x axis.

    const float ceilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
    private Animator animator;            // Reference to the player's animator component.
    private bool facingRight = true;  // For determining which way the player is currently facing.

    public Ray movingRay;
    public Vector3 targetPosition;

    float horizontal = 0;
    float vertical = 0;

	private bool phraseUsed;

	public string[] phrases = {
		"I wanna poooo..."
	};

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
				SoundManager.instance.PlayPlayersSingle(deathSound);
				GameManager.instance.ShowModalDialogPanel("Having fun? No games allowed!", "Restart");
				break;
			case "Flower":
				GameManager.instance.ShowTooltipMessage(Constants.FlowerMessage);
				SoundManager.instance.PlayPlayersSingle(mmmSound);
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
				GameManager.instance.ShowSelectDialogPanel();
				break;
			default: 
				
				break;
		}
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
