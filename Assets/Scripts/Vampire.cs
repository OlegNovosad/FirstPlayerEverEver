using UnityEngine;
using System.Collections;

public class Vampire : MonoBehaviour 
{
	private Player player;

	private Vector3 velocity = Vector3.zero;

	void Start()
	{
		player = PlayerManager.instance.player;
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