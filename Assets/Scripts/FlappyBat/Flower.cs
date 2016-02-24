using UnityEngine;
using System.Collections;

public class Flower : MonoBehaviour {
	public Vector2 velocity = new Vector2(-4, 0);

	// Use this for initialization
	void Start()
	{
		GetComponent<Rigidbody2D>().velocity = velocity;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		switch (other.tag)
		{
			case "Destroyer":
				Destroy(gameObject);
			break;
			default: break;
		}
	}
}
