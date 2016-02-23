using UnityEngine;
using System.Collections;

public class Spear : MonoBehaviour 
{
	private GameObject player;

	private Vector3 velocity = Vector3.zero;
	private Vector3 throwVelocity = Vector3.zero;

	void Start()
	{
		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update()
	{
		if (!GameManager.instance.isPaused)
		{
			if (!PlayerManager.instance.hasSpear)
			{
				transform.position = Vector3.SmoothDamp(transform.position, player.transform.position, ref velocity, 0.1f);
			}
			else
			{
				transform.position = Vector3.SmoothDamp(transform.position, Vector3.forward, ref throwVelocity, 1f);
			}
		}
	}
}
