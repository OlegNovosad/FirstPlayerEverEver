using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
	public static GameManager instance = null;
	public Player player;
	public GameObject bat;
	public GameObject vampire;

	public bool isPaused = false;

	public int totalLevels = 4;

	public AudioClip wallSound;
	public AudioClip deathSound;
	public AudioClip hurtSound;
	public AudioClip batSqueak;
	public AudioClip intovampire;

	public bool isPoisoning = false;
	private float timeLeft = 42; // equal to hp number

	public int chestOpened = 0;
	public int vampireHP = 30;

	public bool isFirstLevel;
	public bool isLastLevel;
	[HideInInspector]
	public bool hasSpearOnStart;

	public Spear spear;



	public Constants.QuestState questState = Constants.QuestState.None;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(this);
		}
	}

	void Start()
	{
        //Update the player reference in player manager on each scene on the load.
		PlayerManager.instance.player = player;
		//Add spear to player if he had one on the prev. level.
		if (PlayerManager.instance.hasSpear) {
			Spear s = Instantiate(spear, PlayerManager.instance.player.transform.position, Quaternion.identity) as Spear;
			PlayerManager.instance.player.contactedSpear = s;
			PlayerManager.instance.SetSpearPosition();
			UIManager.instance.throwSpearButton.gameObject.SetActive(true);
			hasSpearOnStart = true;
		}
        
        
        // TODO: Rework this part when all levels will be implemented.
        if (SceneManager.GetActiveScene().name == "Level1") 
		{
			isFirstLevel = true;
			UIManager.instance.ShowModalDialogPanel("Hello stranger are you ready to play the first game ever... ever?!", "What?", false, true);
		}
		else if (SceneManager.GetActiveScene().name == "Level6") 
		{
			isLastLevel = true;
		} 
		else if (SceneManager.GetActiveScene().name == "Level4")
		{
			// setting the chestOpened to required id depending on the scene
			chestOpened = 4;
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

	public void InitVampire(Vector3 batPosition)
	{
		vampire = Instantiate(vampire, batPosition, Quaternion.identity) as GameObject;
		SoundManager.instance.PlayPlayersSingle(intovampire);
		vampire.GetComponent<SpriteRenderer>().enabled = true;
		vampire.GetComponent<BoxCollider2D>().isTrigger = true;
		UIManager.instance.ShowModalDialogPanel("You better run!!1!11!1!", "1!1!");
	}

	/// <summary>
	/// Pause the game.
	/// </summary>
	/// <param name="pause">If set to <c>true</c> then pause game.</param>
	public void Pause(bool pause)
	{
		isPaused = pause;
	}

	/// <summary>
	/// Restart reloads the scene when called.
	/// </summary>
	public void Restart()
	{
		//Load the last scene loaded, in this case Main, the only scene in the game.
		if (hasSpearOnStart) {
			PlayerManager.instance.hasSpear = true;
		} else {
			PlayerManager.instance.hasSpear = false;
		}
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		PlayerManager.instance.playerHealths = Constants.MaxPlayerHealth;
		UIManager.instance.healthbar.size = 1;
		UIManager.instance.UpdateHUD();
	}

	/// <summary>
	/// Loads the next level.
	/// </summary>
	public void LoadNextLevel()
	{
		SoundManager.instance.PlayPlayersSingle(wallSound);
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		if (currentSceneIndex < GameManager.instance.totalLevels)
		{
			SceneManager.LoadScene(currentSceneIndex + 1);
		}
	}

	/// <summary>
	/// Selects the skill.
	/// </summary>
	/// <param name="choice">Choice of the skill.</param>
	public void SelectSkill(int choice)
	{
        UIManager.instance.HideSelectDialogPanel();
        switch (choice)
		{
			case 0:
				PlayerManager.instance.selectedSkill.Add(Constants.Skill.ThrowSpear);
                PlayerManager.instance.ThrowSpear();
                UIManager.instance.DisplaySkill();
                UIManager.instance.ShowModalDialogPanel("You are not the throwing one, but the spear you need is done!", "Next time I will make you run!");
                break;
			case 1:	
				PlayerManager.instance.selectedSkill.Add(Constants.Skill.PullOutSpear);
                UIManager.instance.DisplaySkill();
                UIManager.instance.ShowModalDialogPanel("What a choice, without doubt. Just find spear and pull it out!", "Go away and scream out loud...");
                break;
			default: break;
		}
				
	}

	public void StartPoisoning()
	{
		UIManager.instance.ShowScreenOverlay();
		isPoisoning = true;
	}

	public void StopPoisoning()
	{
		UIManager.instance.HideScreenOverlay();
		isPoisoning = false;
	}

	/// <summary>
	/// Finishes game.
	/// </summary>
	/// <returns>The over.</returns>
	public IEnumerator GameOver()
	{
		yield return new WaitForSeconds(0.2f);
		SoundManager.instance.PlayPlayersSingle(deathSound);
		UIManager.instance.ShowModalDialogPanel("And that is how the hero dies!", "N-O-O-O-O-O-O!!!", true);
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