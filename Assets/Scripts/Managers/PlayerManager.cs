using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour 
{
	public static PlayerManager instance = null;

	public List<Constants.Skill> selectedSkill = new List<Constants.Skill>() { Constants.Skill.None };
	public int playerHealths = Constants.MaxPlayerHealth;

	public Spear spear;

	public List<Spear> spearsInBack = new List<Spear>();

	public bool hasKey;
	public bool hasSpear = false;
	public bool hasGarlic = false;

	public Player player;

	public int flowersCollected = 0;

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

		DontDestroyOnLoad(this);

		hasKey = false;
	}

	public void Damage(int value)
	{
		playerHealths -= value;
		UIManager.instance.UpdateHUD();
	}

	/// <summary>
	/// Throws the spear.
	/// If player has spear - will throw the spear in direction facing.
	/// Otherwise player will received spear in his back.
	/// </summary>
	public void ThrowSpear()
	{
		if (hasSpear)
		{
			Spear s = Instantiate(spear, player.transform.position, Quaternion.identity) as Spear;
			s.isThrown = true;
			hasSpear = false;
			return;
		}
		else
		{
			if (spearsInBack.Count > 0)
			{
				// throw spear
				StartCoroutine(UIManager.instance.ShowTooltipMessageWithDelay("I have to pull it out to throw.", 2f));
			}
			else
			{
				Instantiate(spear, Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0 + Camera.main.transform.position.y * 2, spear.transform.position.z - Camera.main.transform.position.z)), Quaternion.identity);	
			}
		}
	}

	/// <summary>
	/// Pulls the out spear. Will change throw spear ability usage.
	/// </summary>
	public void PullOutSpear()
	{
		if (spearsInBack.Count > 0)
		{
			hasSpear = true;
			Destroy(player.transform.GetChild(0).gameObject);
			spearsInBack.RemoveAt(0);
		}
	}

	public void AddFlower(int amount) 
	{
		flowersCollected += amount;
	}
}