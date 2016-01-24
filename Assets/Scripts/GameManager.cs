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
	public AudioClip deathSound;

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
		PlayerManager.instance.playerHealths = Constants.MaxPlayerHealth;
		PlayerManager.instance.healthbar.size = 1;
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

	public void SelectSkill(int choice)
	{
		switch (choice)
		{
			case 0:
				PlayerManager.instance.selectedSkill = Constants.Skill.ThrowSpear;
				break;
			case 1:	
				PlayerManager.instance.selectedSkill = Constants.Skill.PullOutSpear;
				break;
			default: 
				PlayerManager.instance.selectedSkill = Constants.Skill.None;
				break;
		}
		HideSelectDialogPanel();
	}

	public IEnumerator GameOver()
	{
		yield return new WaitForSeconds(0.2f);
		SoundManager.instance.PlayPlayersSingle(deathSound);
		GameManager.instance.ShowModalDialogPanel("Having fun? No games allowed!", "Restart");
	}
}
