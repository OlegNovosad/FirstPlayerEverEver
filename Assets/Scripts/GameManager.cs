using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
	List<Room> currentRooms = new List<Room>();
	public static GameManager instance = null;

	public GameObject tooltipPanel;
	public Text tooltipPanelText;

	public int totalLevels = 4;

	void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(gameObject);
		}

		instance = this;
	}

	void OnLevelWasLoaded(int level)
	{
		GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");
		foreach (GameObject room in rooms)
		{
			currentRooms.Add(room.GetComponent<Room>());
		}
	}

	public void ShowTooltipMessage(string message)
	{
		tooltipPanel.SetActive(true);
		tooltipPanelText.text = message;
	}

	public void HideTooltipMessage()
	{
		tooltipPanel.SetActive(false);
		tooltipPanelText.text = "";
	}
}
