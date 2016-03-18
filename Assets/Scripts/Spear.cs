using UnityEngine;
using System.Collections;

public class Spear : MonoBehaviour 
{
	private Vector3 velocity = Vector3.zero;
	public float speed;
	private Vector3 direction;

	public bool isThrown = false;
	private Player player;

	void Start()
	{
		
	}

	public void CalculateDirection(){
		player = PlayerManager.instance.player;

		if (player.facingRight) {
			direction = Vector3.right;
		} else {
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
//				transform.position = Vector3.SmoothDamp(transform.position, PlayerManager.instance.player.transform.position, ref velocity, 0.1f); 
			} 
			else 
			{
				if (PlayerManager.instance.player != null) 
				{
					CalculateDirection ();
					transform.Translate (direction * speed * Time.deltaTime);
				} 
				else 
				{
					Debug.Log ("player is empty! direction can't be calculated. Look into Spear and PlayerManager for details");
				}
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
					//TODO: stuck it in the wall
				}
			break;
			case "Bat":
				if (isThrown)
				{
					GameManager.instance.InitVampire(other.gameObject.transform.position);
					Destroy(other.gameObject);
					Destroy(gameObject); //TODO: stuck it non the place of the vampire!
                }
			break;
			default: break;
		}
	}
}
