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

	public GameObject dpad;

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

	public void ShowDPad()
	{
		dpad.SetActive(true);
	}

	public void HideDPad()
	{
		dpad.SetActive(false);
	}

	public void DisplaySkill()
	{
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
