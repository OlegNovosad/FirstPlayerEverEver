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

	public List<Spear> spears = new List<Spear>();

	public bool hasKey;

	public bool hasSpear = false;

	public bool hasGarlic = false;


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

	public void ThrowSpear()
	{
		GameObject player = GameObject.Find("Player");
		if (spears.Count > 0)
		{
			// throw spear
			StartCoroutine(UIManager.instance.ShowTooltipMessageWithDelay("I have to pull it out to throw.", 2f));
			if (hasSpear)
			{
				Instantiate(spear, player.transform.position, Quaternion.identity);
//				spears.RemoveAt(0);
			}
		}
		else
		{
			Instantiate(spear, Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0 + Camera.main.transform.position.y * 2, spear.transform.position.z - Camera.main.transform.position.z)), Quaternion.identity);
		}
	}

	public void PullOutSpear()
	{
		PlayerManager.instance.hasSpear = true;
		GameObject player = GameObject.Find("Player");
		if (spears.Count > 0)
		{
			Destroy(player.transform.GetChild(0).gameObject);
		}
	}
}