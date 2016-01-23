using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
	List<Room> currentRooms = new List<Room>();

	// Use this for initialization
	void Start()
	{
		
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	void OnLevelWasLoaded(int level)
	{
		GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");
		foreach (Room room in rooms)
		{
			currentRooms.Add(room);
		}
	}
}
