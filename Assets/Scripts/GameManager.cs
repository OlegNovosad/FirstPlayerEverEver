using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
	List<Room> currentRooms = new List<Room>();
	public static GameManager instance = null;

	void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(gameObject);
		}

		instance = this;

		DontDestroyOnLoad(gameObject);
	}

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
		foreach (GameObject room in rooms)
		{
			currentRooms.Add(room.GetComponent<Room>());
		}
	}
}
