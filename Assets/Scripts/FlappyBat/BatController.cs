using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class BatController : MonoBehaviour 
{
	Rigidbody2D rb2D;
	private int score;

	public Text scoreText;

	// The force which is added when the player jumps
	// This can be changed in the Inspector window
	public Vector2 jumpForce = new Vector2(0, 300);

	void Start()
	{
		rb2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Jump
		if (Input.GetKeyUp(KeyCode.Space))
		{
			rb2D.velocity = Vector2.zero;
			rb2D.AddForce(jumpForce);
		}

		// Die by being off screen
		Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
		if (screenPosition.y > Screen.height || screenPosition.y < 0)
		{
			Die();
		}
	}
	
	// Die by collision
	void OnTriggerEnter2D(Collider2D other)
	{
		switch (other.tag)
		{
			case "Obstacle":
				Die();
			break;
			case "Pass":
				score++;
				scoreText.text = "Score: " + score;
			break;
			case "Flower":
				// TODO: temporary add +10 to score, probably flowers will be counted
				// in different field.
				score += 10;
				scoreText.text = "Score: " + score;
				Destroy(other.gameObject);
			break;
			default: break;
		}
	}
	
	void Die()
	{
		score = 0;
		scoreText.text = "Score: " + score;

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
