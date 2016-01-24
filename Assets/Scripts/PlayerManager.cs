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

	public GameObject spearSource;
	public GameObject spear;

	public Button throwSpearButton;
	public Button pullOutSpearButton;

	void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(gameObject);
		}

		instance = this;
	}

	void Update()
	{
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
			GameManager.instance.GameOver();
		}
	}

	public void ThrowSpear(GameObject player)
	{
		Damage(6);
		GameObject s = Instantiate(spear, spearSource.transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(-30, 30)))) as GameObject;
		StartCoroutine(Move(s.transform, player.transform, 5f));
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
