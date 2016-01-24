using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour 
{
	public static PlayerManager instance = null;

	public Constants.Skill selectedSkill = Constants.Skill.None;
	public int playerHealths = Constants.MaxPlayerHealth;

	public Scrollbar healthbar;
	public Text healthAmount;

	void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(gameObject);
		}

		instance = this;
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
			GameManager.instance.GameOver();
		}
	}
}
