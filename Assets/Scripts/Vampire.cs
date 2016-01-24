using UnityEngine;
using System.Collections;

public class Vampire : MonoBehaviour 
{
	private Vector3[] steps = new Vector3[] {
		new Vector3(11f, 1f, 0f), new Vector3(15.15f, 4f, 0f),
		new Vector3(10f, 9f, 0f), new Vector3(5.3f, 9f, 0f),
		new Vector3(7.7f, 7.3f, 0f), new Vector3(10f, 9f, 0f), new Vector3(5.3f, 9f, 0f), 
		new Vector3(0.2f, 6f, 0f), new Vector3(4.2f, 2f, 0f)
	};

	private int step = 0;
	public float timer = 1f;

	public bool turn = false;
	
	// Update is called once per frame
	void Update()
	{
		if (turn)
		{
			timer -= Time.deltaTime;
			if (timer <= 0f)
			{
				timer = 1f;
				// Move Player
				if (step < steps.Length)
				{
					transform.position = steps[step++];
				}
				else
				{
					step = 0;
					transform.position = steps[step++];
				}
			}
		}
	}
}