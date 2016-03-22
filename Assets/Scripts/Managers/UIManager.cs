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
	public Button FirstAnswerButton;
	public Text FirstAnswerText;
	public Button SecondAnswerButton;
	public Text SecondAnswerText;
	public Button ThirdAnswerButton;
	public Text ThirdAnswerText;
	public Button ForthAnswerButton;
	public Text ForthAnswerText;
	public Text modalDialogMessageText;


	// Ocasional items
	public GameObject screenOverlay;

	public Image questHUD;


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

	/// <summary>
	/// Shows the modal dialog panel. And populates the text based on the TextManager.instance.branch
	/// </summary>
	public void ShowModalDialogPanel()
	{
		GameManager.instance.Pause(true);
		modalDialogPanel.SetActive (true);
		TextManager.instance.PopulateModalDialogWithText ();

	}


	/// <summary>
	/// Sets/Changes the modal dialog text.
	/// </summary>
	/// <param name="message">Message.</param>
	/// <param name="answer">Answer.</param>
	/// <param name="finishGame">If set to <c>true</c> finish game.</param>
	/// <param name="firstLevel">If set to <c>true</c> first level.</param>
	public void SetModalDialogText(string message, Answer answer, bool finishGame = false, bool firstLevel = false) //TODO remove extra params - need to handle these differently.
	{
		HideExtraAnswers();
		FirstAnswerButton.onClick.RemoveAllListeners();// clean up previous listeners
		modalDialogMessageText.text = message;
		FirstAnswerText.text = answer.buttonText;
		FirstAnswerButton.onClick.AddListener(() => answer.AnswerClick()); //may need to revisit this!
	}

	public void SetModalDialogTextMultiple(string message, Answer answer1, Answer answer2 = null, Answer answer3 = null, Answer answer4 = null) 
	{
		HideExtraAnswers();
		modalDialogMessageText.text = message;
		FirstAnswerButton.onClick.RemoveAllListeners();// clean up previous listeners
		FirstAnswerText.text = answer1.buttonText;
		FirstAnswerButton.onClick.AddListener(() => answer1.AnswerClick()); 

		if (answer2 != null) 
		{
			SecondAnswerButton.onClick.RemoveAllListeners ();
			SecondAnswerText.text = answer2.buttonText;
			SecondAnswerButton.gameObject.SetActive (true);
			SecondAnswerButton.onClick.AddListener (() => answer2.AnswerClick ()); 
		}
		if (answer3 != null) 
		{
			ThirdAnswerButton.onClick.RemoveAllListeners ();
			ThirdAnswerText.text = answer3.buttonText;
			ThirdAnswerButton.gameObject.SetActive (true);
			ThirdAnswerButton.onClick.AddListener (() => answer3.AnswerClick ()); 
		}
		if (answer4 != null) 
		{
			ForthAnswerButton.onClick.RemoveAllListeners ();
			ForthAnswerText.text = answer4.buttonText;
			ForthAnswerButton.gameObject.SetActive (true);
			ForthAnswerButton.onClick.AddListener (() => answer4.AnswerClick ()); 
		}
	}

	public void HideExtraAnswers()
	{
		if (SecondAnswerButton.gameObject.activeSelf) {
			SecondAnswerButton.onClick.RemoveAllListeners ();
			SecondAnswerButton.gameObject.SetActive (false);
		}
		if (ThirdAnswerButton.gameObject.activeSelf) {
			ThirdAnswerButton.onClick.RemoveAllListeners ();
			ThirdAnswerButton.gameObject.SetActive (false);
		}
		if (ForthAnswerButton.gameObject.activeSelf) {
			ForthAnswerButton.onClick.RemoveAllListeners ();
			ForthAnswerButton.gameObject.SetActive (false);
		}
	}

	/// <summary>
	/// Hides the modal dialog panel.
	/// </summary>
	/// <param name="firstLevel">If set to <c>true</c> first level.</param>
	public void HideModalDialogPanel(bool firstLevel = false)
	{
		FirstAnswerButton.onClick.RemoveAllListeners();
		modalDialogPanel.SetActive(false);
		modalDialogMessageText.text = "";
		FirstAnswerText.text = "";
		GameManager.instance.Pause(false);
	}

	public void ShowItem(Constants.Item item)
	{
		switch (item)
		{
//			case Constants.Item.Stake:
//				stake.gameObject.SetActive(true);
//			break;
//			case Constants.Item.Crusifix:
//				stake.gameObject.SetActive(false);
//				crusifix.gameObject.SetActive(true);
//			break;
//			case Constants.Item.Garlic:
//				crusifix.gameObject.SetActive(false);
//				garlic.gameObject.SetActive(true);
//				garlicHUD.gameObject.SetActive(true);
//			break;
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

	//TODO: check this and set the ed color if player gets hit.
	public void SetScreenOverlayColor(Color color){
		screenOverlay.GetComponent<Image>().color = color;

	}

	#endregion
}
