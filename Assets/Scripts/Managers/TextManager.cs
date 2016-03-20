using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class TextManager : MonoBehaviour {
	public static TextManager instance = null;

	public int currentLevel;
	public int textIndex;
	public int branch;

	private DialogTextConstants dialogTextConstants;

	void Awake ()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy (gameObject);
		}
	}


	void Start()
	{
		dialogTextConstants = new DialogTextConstants();
		dialogTextConstants.SetPhraseListForModalDialog(currentLevel);//not generating...
		branch = 1;
		textIndex = 0;
	}

	/// <summary>
	/// Populates the modal dialog with text based on branch. 
	/// This is mostly done automatically. 
	/// If you need to populate text from branch without particular order, just change the TextManager.instance.branch to desired branch before launching modal pop up. 
	/// </summary>
	public void PopulateModalDialogWithText(){

		//Main dialogs without branched decisions
		switch (branch) 
		{
		case 1:
			UIManager.instance.SetModalDialogText (dialogTextConstants.phrasesB1 [textIndex], dialogTextConstants.answersB1[textIndex]);
			break;
		case 2:
			UIManager.instance.SetModalDialogText (dialogTextConstants.phrasesB2 [textIndex], dialogTextConstants.answersB2 [textIndex]);
			break;
		case 3:
			UIManager.instance.SetModalDialogText (dialogTextConstants.phrasesB3 [textIndex], dialogTextConstants.answersB3 [textIndex]);
			break;
		case 4:
			UIManager.instance.SetModalDialogText (dialogTextConstants.phrasesB4 [textIndex], dialogTextConstants.answersB4 [textIndex]);
			break;
		case 5:
			UIManager.instance.SetModalDialogText (dialogTextConstants.phrasesB5 [textIndex], dialogTextConstants.answersB5 [textIndex]);
			break;
		case 21: //branch 2_1
			UIManager.instance.SetModalDialogText (dialogTextConstants.phrasesB2_1 [textIndex], dialogTextConstants.answersB2_1 [textIndex]);
			break;
		case 22: //branch 2_2
			UIManager.instance.SetModalDialogText (dialogTextConstants.phrasesB2_2 [textIndex], dialogTextConstants.answersB2_2 [textIndex]);
			break;
		case 41: //branch 4_1
			UIManager.instance.SetModalDialogText (dialogTextConstants.phrasesB4_1 [textIndex], dialogTextConstants.answersB4_1 [textIndex]);
			break;
		case 42: //branch 4_2
			UIManager.instance.SetModalDialogText (dialogTextConstants.phrasesB4_2 [textIndex], dialogTextConstants.answersB4_2 [textIndex]);
			break;
		default:
			break;
		}


	}

}
