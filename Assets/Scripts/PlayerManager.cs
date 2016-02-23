using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour 
{
	public static PlayerManager instance = null;

	public Constants.Skill selectedSkill = Constants.Skill.None;
	public int playerHealths = Constants.MaxPlayerHealth;

	public GameObject spear;

	public List<GameObject> spears = new List<GameObject>();

	public bool hasKey;

	public bool hasSpear = false;

	public GameObject currentSpear = null;

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
		if (hasSpear)
		{
			// throw spear
		}
		else
		{
			GameObject s = Instantiate(spear, Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0 + Camera.main.transform.position.y * 2, spear.transform.position.z - Camera.main.transform.position.z)), Quaternion.identity) as GameObject;
			StartCoroutine(s.transform.Move(player.transform.position, 0.1f));
		}
	}

	public void PullOutSpear(GameObject player)
	{
		if (player.transform.childCount > 0)
		{
			currentSpear = Instantiate(player.transform.GetChild(0).gameObject);
			Destroy(player.transform.GetChild(0).gameObject);
		}
	}
}