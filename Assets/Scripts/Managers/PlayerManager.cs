using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour 
{
	public static PlayerManager instance = null;

	public List<Constants.Skill> selectedSkill = new List<Constants.Skill>() { Constants.Skill.None };
	public int playerHealths = Constants.MaxPlayerHealth;

    public bool spearPiercedPlayer = false;
    public List<Spear> spearsInBack = new List<Spear>();

	public bool hasKey;
	public bool hasSpear;
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
		UIManager.instance.throwSpearButton.gameObject.SetActive (false);
		UIManager.instance.pullOutSpearButton.gameObject.SetActive (true);
		if (hasSpear)
		{
			instance.player.contactedSpear.isThrown = true;
			hasSpear = false;
			//TODO: throwing animation here.
			return;
		}
		else 
		{
			if (spearPiercedPlayer)
            {
				StartCoroutine(UIManager.instance.ShowTooltipMessageWithDelay("I have to pull it out to throw.", 2f)); //TODO: review text
			}
			else //no spear in the back
			{
				UIManager.instance.ShowTooltipMessageWithDelay("I need to find a spear.", 2f); //TODO: review text
				//Instantiate (spear, Camera.main.ScreenToWorldPoint (new Vector3 (0.0f, 0 + Camera.main.transform.position.y * 2, spear.transform.position.z - Camera.main.transform.position.z)), Quaternion.identity);
			}
		}
	}

	/// <summary>
	/// Pulls out spear. Will change throw spear ability usage.
	/// </summary>
	public void PullOutSpear()
	{
		//Case 1: Spear is in the back
		if (spearPiercedPlayer) {
			//TODO Pull out from the back animation
			spearsInBack.RemoveAt (0);
            instance.hasSpear = true;
        }
        //Case 2: Player is standing on the Spear (contact) and can pull it out:
		else if (PlayerManager.instance.player.contactsWithSpear) {
            //TODO PullOut simple animation 
            PlayerManager.instance.player.contactedSpear.transform.SetParent(PlayerManager.instance.player.transform);
			instance.hasSpear = true;
        }
        //changing button to ThrowSpear.
		if (instance.hasSpear) { 
		    UIManager.instance.throwSpearButton.gameObject.SetActive (true);
		    UIManager.instance.pullOutSpearButton.gameObject.SetActive (false);
        }
    }

	public void AddFlower(int amount) 
	{
		flowersCollected += amount;
	}
}