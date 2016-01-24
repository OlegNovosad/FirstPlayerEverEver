using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
	List<Room> currentRooms = new List<Room>();
	public static GameManager instance = null;
	public GameObject bat;
	public GameObject vampire;

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
	public AudioClip hurtSound;
	public AudioClip batSqueak;
	public AudioClip intovampire;

	private bool isPoisoning = false;
	private float timeLeft = 42; // equal to hp number

	public int chestOpened = 0;
	public int batHP = 30;
	public int vampireHP = 9999;

	public bool isFirstLevel;


	public Constants.QuestState questState = Constants.QuestState.None;

	void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(gameObject);
		}

		instance = this;
		if (SceneManager.GetActiveScene ().name == "Level1") {
			isFirstLevel = true;
			ShowModalDialogPanel ("Hello stranger \nare \nyou ready to play\nfirst game ever... ever?!", "What?", false, true);
		}
	}

	void Update()
	{
		if (isPoisoning && !isPaused)
		{
			timeLeft -= Time.deltaTime;
			PlayerManager.instance.Damage(PlayerManager.instance.playerHealths - (int) timeLeft);
			SoundManager.instance.PlayPlayersSingle(hurtSound);
		}
	}

	/// <summary>
	/// Damages the bat.
	/// </summary>
	/// <param name="value">Value.</param>
	public void DamageBat(int value)
	{
		batHP -= value;
		SoundManager.instance.PlayPlayersSingle(batSqueak);

		if (batHP <= 0)
		{
			Destroy(bat);
			SoundManager.instance.PlayPlayersSingle(intovampire);
			vampire.GetComponent<SpriteRenderer>().enabled = true;
			vampire.GetComponent<BoxCollider2D>().isTrigger = true;
			ShowModalDialogPanel("You better run!!1!11!1!", "1!1!");
			vampire.GetComponent<Vampire>().turn = true;
		}
	}

	/// <summary>
	/// Shows the tooltip message.
	/// </summary>
	/// <param name="message">Message.</param>
	public void ShowTooltipMessage(string message)
	{
		tooltipPanel.SetActive(true);
		tooltipPanelText.text = message;
	}

	/// <summary>
	/// Hides the tooltip message.
	/// </summary>
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

	public void ShowModalDialogPanel(string message, string buttonText, bool finishGame = false, bool firstLevel = false)
	{
		modalDialogPanel.SetActive(true);
		modalDialogMessageText.text = message;
		modalDialogButtonText.text = buttonText;

		if (finishGame)
		{
			modalDialogButton.onClick.AddListener(() => Restart());
		}
		else
		{
			modalDialogButton.onClick.AddListener(() => HideModalDialogPanel(firstLevel));
		}

		isPaused = true;
	}

	public void HideModalDialogPanel(bool firstLevel = false)
	{
		modalDialogButton.onClick.RemoveAllListeners();
		modalDialogPanel.SetActive(false);
		modalDialogMessageText.text = "";
		modalDialogButtonText.text = "";
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

	public void StartPoisoning()
	{
		screenOverlay.SetActive(!screenOverlay.activeSelf);
		isPoisoning = !isPoisoning;
	}

	public IEnumerator GameOver()
	{
		yield return new WaitForSeconds(0.2f);
		SoundManager.instance.PlayPlayersSingle(deathSound);
		GameManager.instance.ShowModalDialogPanel("Having fun? No games allowed!", "Restart", true);
	}

	public void StartFromTheBeginning()
	{
		SceneManager.LoadScene(5);
	}

	public void StartAgain()
	{
		SceneManager.LoadScene(0);
	}
}
