using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour 
{
	public static PlayerManager instance = null;

	public Constants.Skill selectedSkill = Constants.Skill.None;
	public int playerHealths = Constants.MaxPlayerHealth;

	public Scrollbar healthbar;
	public Text healthAmount;

	public GameObject spear;

	public Button throwSpearButton;
	public Button pullOutSpearButton;

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

		healthbar = GameObject.Find("Canvas/HUD/Healthbar").GetComponent<Scrollbar>();
		throwSpearButton = GameObject.Find("Canvas/HUD/Skills/ThrowSpear").GetComponent<Button>();
		pullOutSpearButton = GameObject.Find("Canvas/HUD/Skills/PullOutSpear").GetComponent<Button>();
		healthAmount = GameObject.Find("Canvas/HUD/Healthbar/Amount").GetComponent<Text>();

		hasKey = false;
	}

	void Update()
	{
		if (healthbar == null)
		{
			healthbar = GameObject.Find("Canvas/HUD/Healthbar").GetComponent<Scrollbar>();	
		}

		if (throwSpearButton == null)
		{
			throwSpearButton = GameObject.Find("Canvas/HUD/Skills/ThrowSpear").GetComponent<Button>();	
		}

		if (pullOutSpearButton == null)
		{
			pullOutSpearButton = GameObject.Find("Canvas/HUD/Skills/PullOutSpear").GetComponent<Button>();	
		}

		if (healthAmount == null)
		{
			healthAmount = GameObject.Find("Canvas/HUD/Healthbar/Amount").GetComponent<Text>();
		}

		healthbar.size = playerHealths / 42f;

		if (playerHealths > 0)
		{
			healthAmount.text = playerHealths.ToString();
		}
		else
		{
			healthAmount.text = "0";
			StartCoroutine(GameManager.instance.GameOver());
		}

		if (selectedSkill == Constants.Skill.ThrowSpear)
		{
			throwSpearButton.gameObject.SetActive(true);
			pullOutSpearButton.gameObject.SetActive(false);
		}

		if (selectedSkill == Constants.Skill.PullOutSpear)
		{
			throwSpearButton.gameObject.SetActive(false);
			pullOutSpearButton.gameObject.SetActive(true);
		}
	}

	public void Damage(int value)
	{
		playerHealths -= value;
		healthbar.size = playerHealths / 42f;

		if (playerHealths > 0)
		{
			healthAmount.text = playerHealths.ToString();
		}
		else
		{
			healthAmount.text = "0";
			StartCoroutine(GameManager.instance.GameOver());
		}
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
