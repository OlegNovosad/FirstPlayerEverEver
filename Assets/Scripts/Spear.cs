using UnityEngine;
using System.Collections;

public class Spear : MonoBehaviour 
{
	private Player player;
	private Vector3 velocity = Vector3.zero;
	public float speed;
	private Vector3 direction;

	public bool isThrown = false;

	void Start()
	{
		player = GameObject.Find("Player").GetComponent<Player>();

		if (player.facingRight)
		{
			direction = Vector3.right;
		}
		else
		{
			Vector3 theScale = transform.localScale;
	        theScale.x *= -1;
	        transform.localScale = theScale;

			direction = Vector3.left;
		}
	}
	
	// Update is called once per frame
	void Update()
	{
		if (!GameManager.instance.isPaused)
		{
			if (!isThrown)
			{
				transform.position = Vector3.SmoothDamp(transform.position, player.transform.position, ref velocity, 0.1f);
			}
			else
			{
				transform.Translate(direction * speed * Time.deltaTime);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		switch (other.tag)
		{
			case "Wall":
				if (isThrown)
				{
					Destroy(gameObject);
				}
			break;
			case "Bat":
				if (isThrown)
				{
					Destroy(other.gameObject);
					Destroy(gameObject);
				}
			break;
			default: break;
		}
	}
}
