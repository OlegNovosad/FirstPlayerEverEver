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
		//set buttons:
		UIManager.instance.throwSpearButton.gameObject.SetActive (false);
		if (instance.hasSpear && PlayerManager.instance.selectedSkill.Contains (Constants.Skill.PullOutSpear)) 
		{ 
			UIManager.instance.pullOutSpearButton.gameObject.SetActive (true);
		}

		//Throwing
		if (instance.hasSpear)
		{
			instance.player.contactedSpear.isThrown = true;
			hasSpear = false;
			//TODO: throwing animation here.
			//Instantiate (instance.player.contactedSpear, Camera.main.ScreenToWorldPoint (new Vector3 (0.0f, 0 + Camera.main.transform.position.y * 2, instance.player.contactedSpear.transform.position.z - Camera.main.transform.position.z)), Quaternion.identity);
			return;
		} 
		else 
		{
			if (spearPiercedPlayer) //if player got spear in the back while thrown spear 
            {
				StartCoroutine(UIManager.instance.ShowTooltipMessageWithDelay("I have to pull it out to throw.", 2f)); //TODO: review text
			}
			else //no spear in the back
			{
				UIManager.instance.ShowTooltipMessageWithDelay("I need to find a spear.", 2f); //TODO: review text
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
			SetSpearPosition();
            instance.hasSpear = true;
        }
        //Case 2: Player is standing on the Spear (contact) and can pull it out:
		else if (PlayerManager.instance.player.contactsWithSpear) {
            //TODO PullOut simple animation 
			SetSpearPosition();
			//TODO move the spear to correct position
			instance.hasSpear = true;
        }
        //changing button to ThrowSpear if this skill is available.
		UIManager.instance.pullOutSpearButton.gameObject.SetActive (false);
		if (instance.hasSpear && PlayerManager.instance.selectedSkill.Contains(Constants.Skill.ThrowSpear)) { 
		    UIManager.instance.throwSpearButton.gameObject.SetActive (true);
        }
    }

	public void SetSpearPosition(){
		PlayerManager.instance.player.contactedSpear.transform.SetParent (PlayerManager.instance.player.rightArm.transform);
		PlayerManager.instance.player.contactedSpear.transform.localScale = new Vector3(1, 1, 1);
		PlayerManager.instance.player.contactedSpear.transform.localPosition = new Vector3(0.1f, -0.4f, 1);
		PlayerManager.instance.player.contactedSpear.transform.Rotate (new Vector3 (0, 0, 215f));	
		PlayerManager.instance.player.rightArm.transform.Rotate (new Vector3 (0, 0, 155f));
		PlayerManager.instance.player.contactedSpear.GetComponent<SpriteRenderer> ().sortingOrder = 5;
		PlayerManager.instance.player.contactedSpear.GetComponent<SpriteRenderer> ().sortingLayerName = "Boobaraka";
	}

	public void AddFlower(int amount) 
	{
		flowersCollected += amount;
	}
}