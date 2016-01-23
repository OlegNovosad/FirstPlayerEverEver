using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour 
{
	public float restartLevelDelay = 1f;		//Delay time in seconds to restart level.
	public AudioClip moveSound1;				//1 of 2 Audio clips to play when player moves.
	public AudioClip moveSound2;				//2 of 2 Audio clips to play when player moves.
	public AudioClip eatSound1;					//1 of 2 Audio clips to play when player collects a food object.
	public AudioClip eatSound2;					//2 of 2 Audio clips to play when player collects a food object.
	public AudioClip drinkSound1;				//1 of 2 Audio clips to play when player collects a soda object.
	public AudioClip drinkSound2;				//2 of 2 Audio clips to play when player collects a soda object.
	public AudioClip gameOverSound;				//Audio clip to play when player dies.

	[SerializeField] private float maxSpeed = 2f;                    // The fastest the player can travel in the x axis.

    const float ceilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
    private Animator animator;            // Reference to the player's animator component.
    private bool facingRight = true;  // For determining which way the player is currently facing.

    public Ray movingRay;
    public Vector3 targetPosition;

    float horizontal = 0;
    float vertical = 0;

    private BoxCollider2D boxCollider2D;
    private Rigidbody2D rb2D;
	
	//Start overrides the Start function of MovingObject
	void Start ()
	{
		//Get a component reference to the Player's animator component
		animator = GetComponent<Animator>();
		targetPosition = transform.position;
		boxCollider2D = GetComponent<BoxCollider2D>();
		rb2D = GetComponent<Rigidbody2D>();
	}

	private void Update ()
	{
		//Get input from the input manager, round it to an integer and store in horizontal to set x axis move direction
		horizontal = Input.GetAxisRaw ("Horizontal");
		
		//Get input from the input manager, round it to an integer and store in vertical to set y axis move direction
		vertical = Input.GetAxisRaw ("Vertical");

		//Check if we have a non-zero value for horizontal or vertical
		if (horizontal != 0 || vertical != 0)
		{
			Move(horizontal, vertical);
		}
	}

	//OnTriggerEnter2D is sent when another object enters a trigger collider attached to this object (2D physics only).
	private void OnTriggerEnter2D (Collider2D other)
	{
		switch (other.tag)
		{
			case "Next":
				LoadNextLevel();
				break;
			case "Death":
				Restart();
				break;
			default: 
				
				break;
		}
	}
	
	//Restart reloads the scene when called.
	private void Restart()
	{
		//Load the last scene loaded, in this case Main, the only scene in the game.
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	private void LoadNextLevel()
	{
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		if (SceneManager.GetAllScenes().Length > currentSceneIndex)
		{
			SceneManager.LoadScene(currentSceneIndex + 1);
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
