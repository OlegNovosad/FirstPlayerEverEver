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

	public GameObject tooltipPanel;
	public Text tooltipPanelText;
	public GameObject selectDialogPanel;
	public GameObject modalDialogPanel;
	public Text modalDialogButtonText;
	public Text modalDialogMessageText;
	public Button modalDialogButton;

	// Ocasional items
	public GameObject screenOverlay;
	public Image key;

	public Image stake;
	public Image crusifix;
	public Image garlic;
	public Image garlicHUD;

	public GameObject joystick;

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
	}

	void Start()
	{
		UpdateHUD();
		DisplaySkill();
	}

	#region HUD Managing

	public void ShowJoystick()
	{
		joystick.SetActive(true);
	}

	public void HideJoystick()
	{
		joystick.SetActive(false);
	}

	public void DisplaySkill()
	{
		if (PlayerManager.instance.selectedSkill.Contains(Constants.Skill.ThrowSpear))
		{
			throwSpearButton.gameObject.SetActive(true);
		}

		if (PlayerManager.instance.selectedSkill.Contains(Constants.Skill.PullOutSpear))
		{
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

	#endregion

	#region Popups and tooltips

	/// <summary>
	/// Shows the tooltip message.
	/// </summary>
	/// <param name="message">Message.</param>
	public void ShowTooltipMessage(string message)
	{
		tooltipPanel.SetActive(true);
		tooltipPanelText.text = message;
	}

	public IEnumerator ShowTooltipMessageWithDelay(string message, float delayTime)
	{
		tooltipPanel.SetActive(true);
		tooltipPanelText.text = message;
		yield return new WaitForSeconds(delayTime);
		HideTooltipMessage();
	}

	/// <summary>
	/// Hides the tooltip message.
	/// </summary>
	public void HideTooltipMessage()
	{
		tooltipPanel.SetActive(false);
		tooltipPanelText.text = "";
	}

	public void ShowSelectDialogPanel()
	{
		GameManager.instance.Pause(true);
		selectDialogPanel.SetActive(true);
	}

	public void HideSelectDialogPanel()
	{
		selectDialogPanel.SetActive(false);
		GameManager.instance.Pause(false);
	}

	public void ShowModalDialogPanel(string message, string buttonText, bool finishGame = false, bool firstLevel = false)
	{
		modalDialogPanel.SetActive(true);
		modalDialogMessageText.text = message;
		modalDialogButtonText.text = buttonText;

		if (finishGame)
		{
			modalDialogButton.onClick.AddListener(() => GameManager.instance.Restart());
		}
		else
		{
			modalDialogButton.onClick.AddListener(() => HideModalDialogPanel(firstLevel));
		}

		GameManager.instance.Pause(true);
	}

	public void HideModalDialogPanel(bool firstLevel = false)
	{
		modalDialogButton.onClick.RemoveAllListeners();
		modalDialogPanel.SetActive(false);
		modalDialogMessageText.text = "";
		modalDialogButtonText.text = "";
		GameManager.instance.Pause(false);
	}

	public void ShowItem(Constants.Item item)
	{
		switch (item)
		{
			case Constants.Item.Stake:
				stake.gameObject.SetActive(true);
			break;
			case Constants.Item.Crusifix:
				stake.gameObject.SetActive(false);
				crusifix.gameObject.SetActive(true);
			break;
			case Constants.Item.Garlic:
				crusifix.gameObject.SetActive(false);
				garlic.gameObject.SetActive(true);
				garlicHUD.gameObject.SetActive(true);
			break;
		}
	}

	#endregion

	#region Overlays

	public void ShowScreenOverlay()
	{
		screenOverlay.SetActive(true);
	}

	public void HideScreenOverlay()
	{
		screenOverlay.SetActive(false);
	}

	#endregion
}
