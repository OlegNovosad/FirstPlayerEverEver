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

	public void ThrowSpear(GameObject player)
	{
		if (hasSpear)
		{
			// throw spear
		}
		else
		{
			GameObject s = Instantiate(spear, Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0 + Camera.main.transform.position.y * 2, 0 - Camera.main.transform.position.z)), Quaternion.Euler(new Vector3(0, 0, 30))) as GameObject;
			StartCoroutine(Move(s.transform, player.transform, 2f));
			spears.Add(s);
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

	private IEnumerator Move(Transform source, Transform target, float duration)
	{
		while (Vector3.Distance(source.position, target.position) > 1f)
		{
			//Move Player
			source.Translate(target.position * 2f * Time.deltaTime);
			yield return null;
		}

		source.position = target.position;
		source.SetParent(target);
	}
}