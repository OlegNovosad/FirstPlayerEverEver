using UnityEngine;
using System.Collections;

public class Vampire : MonoBehaviour 
{
	private GameObject player;

	private Vector3 velocity = Vector3.zero;

	void Start()
	{
		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update()
	{
		if (!GameManager.instance.isPaused)
		{
			transform.position = Vector3.SmoothDamp(transform.position, player.transform.position, ref velocity, 2f);
		}
	}
}