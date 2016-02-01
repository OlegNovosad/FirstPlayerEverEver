using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
	public static GameManager instance = null;
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
	public int batHP = 30;
	public int vampireHP = 30;

	public bool isFirstLevel;
	public bool isLastLevel;

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

		if (SceneManager.GetActiveScene().name == "Level1") 
		{
			isFirstLevel = true;
			UIManager.instance.ShowModalDialogPanel("Hello stranger are you ready to play the first game ever... ever?!", "What?", false, true);
		}
		else if (SceneManager.GetActiveScene().name == "Level5") 
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
			Vector3 batPosition = bat.transform.position;
			Destroy(bat);

			InitVampire(batPosition);
		}
	}

	private void InitVampire(Vector3 batPosition)
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
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		PlayerManager.instance.playerHealths = Constants.MaxPlayerHealth;
		UIManager.instance.healthbar.size = 1;
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
		UIManager.instance.HideSelectDialogPanel();
		UIManager.instance.DisplaySkill();
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
		UIManager.instance.ShowModalDialogPanel("Having fun? No games allowed!", "Restart", true);
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