using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour 
{
	public static UIManager instance = null;

	public Scrollbar healthbar;
	public Text healthAmount;

	public Button throwSpearButton;
	public Button pullOutSpearButton;

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
	}
	
	// Update is called once per frame
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

		UpdateHUD();

		if (PlayerManager.instance.selectedSkill == Constants.Skill.ThrowSpear)
		{
			throwSpearButton.gameObject.SetActive(true);
			pullOutSpearButton.gameObject.SetActive(false);
		}

		if (PlayerManager.instance.selectedSkill == Constants.Skill.PullOutSpear)
		{
			throwSpearButton.gameObject.SetActive(false);
			pullOutSpearButton.gameObject.SetActive(true);
		}
	}

	/// <summary>
	/// Updates the HUD in the UI system.
	/// </summary>
	public void UpdateHUD()
	{
		healthbar.size = PlayerManager.instance.playerHealths / 42f;

		if (PlayerManager.instance.playerHealths > 0)
		{
			healthAmount.text = PlayerManager.instance.playerHealths.ToString();
		}
		else
		{
			healthAmount.text = "0";
			StartCoroutine(GameManager.instance.GameOver());
		}
	}
}
