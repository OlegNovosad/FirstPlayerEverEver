using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
	List<Room> currentRooms = new List<Room>();
	public static GameManager instance = null;

	public GameObject tooltipPanel;
	public Text tooltipPanelText;
	public GameObject selectDialogPanel;
	public GameObject modalDialogPanel;
	public Text modalDialogButtonText;
	public Text modalDialogMessageText;

	public GameObject screenOverlay;

	public Button modalDialogButton;

	public bool isPaused = false;

	public int totalLevels = 4;

	public AudioClip wallSound;

	void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(gameObject);
		}

		instance = this;
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

	public void ShowSelectDialogPanel()
	{
		isPaused = true;
		selectDialogPanel.SetActive(true);
	}

	public void HideSelectDialogPanel()
	{
		selectDialogPanel.SetActive(false);
		isPaused = false;
	}

	public void ShowModalDialogPanel(string message, string buttonText)
	{
		modalDialogPanel.SetActive(true);
		modalDialogMessageText.text = message;
		modalDialogButtonText.text = buttonText;
		modalDialogButton.onClick.AddListener(() => Restart());
		isPaused = true;
	}

	public void HideModalDialogPanel()
	{
		modalDialogPanel.SetActive(false);
		modalDialogMessageText.text = "";
		modalDialogButtonText.text = "";
		modalDialogButton.onClick.AddListener(null);
		isPaused = false;
	}

	//Restart reloads the scene when called.
	public void Restart()
	{
		//Load the last scene loaded, in this case Main, the only scene in the game.
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void LoadNextLevel()
	{
		SoundManager.instance.PlayPlayersSingle(wallSound);
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		if (currentSceneIndex < GameManager.instance.totalLevels)
		{
			SceneManager.LoadScene(currentSceneIndex + 1);
		}
	}
}
