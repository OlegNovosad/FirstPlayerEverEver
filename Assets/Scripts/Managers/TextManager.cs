using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class TextManager : MonoBehaviour {
	public static TextManager instance = null;

	public int currentLevel;
	public int answerIndex;
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
		answerIndex = 0;
		textIndex = 0;
	}

	/// <summary>
	/// Populates the modal dialog with text based on branch. 
	/// This is mostly done automatically. 
	/// If you need to populate text from branch without particular order, just change the TextManager.instance.branch to desired branch before launching modal pop up. 
	/// </summary>
	public void PopulateModalDialogWithText(){

		//Main dialogs without branched decisions
		switch (branch) {
		case 1:
			//showing a group of items:
			if (dialogTextConstants.answersB1 [answerIndex].isInGroup) {
				//show four items:
				if (dialogTextConstants.answersB1 [answerIndex].numResponses == 4) {
					UIManager.instance.SetModalDialogTextMultiple (dialogTextConstants.phrasesB1 [textIndex], dialogTextConstants.answersB1 [answerIndex], dialogTextConstants.answersB1 [answerIndex + 1], dialogTextConstants.answersB1 [answerIndex + 2], dialogTextConstants.answersB1 [answerIndex + 3]);
					answerIndex = answerIndex + 3;
				} 
				//show three items:
				else if (dialogTextConstants.answersB1 [answerIndex].numResponses == 3) {
					UIManager.instance.SetModalDialogTextMultiple (dialogTextConstants.phrasesB1 [textIndex], dialogTextConstants.answersB1 [answerIndex], dialogTextConstants.answersB1 [answerIndex + 1], dialogTextConstants.answersB1 [answerIndex + 2]);
					answerIndex = answerIndex + 2;
				}
				//show two items:
				else if (dialogTextConstants.answersB1 [answerIndex].numResponses == 2) {
					UIManager.instance.SetModalDialogTextMultiple (dialogTextConstants.phrasesB1 [textIndex], dialogTextConstants.answersB1 [answerIndex], dialogTextConstants.answersB1 [answerIndex + 1]);
					answerIndex = answerIndex + 1;
				}
			} else {
				UIManager.instance.SetModalDialogText (dialogTextConstants.phrasesB1 [textIndex], dialogTextConstants.answersB1 [answerIndex]);
			}
			break;
		case 2:
			//showing a group of items:
			if (dialogTextConstants.answersB2 [answerIndex].isInGroup) 
			{
				//show four items:
				if (dialogTextConstants.answersB2 [answerIndex].numResponses == 4) 
				{
					UIManager.instance.SetModalDialogTextMultiple (dialogTextConstants.phrasesB2 [textIndex], dialogTextConstants.answersB2 [answerIndex], dialogTextConstants.answersB2 [answerIndex + 1], dialogTextConstants.answersB2 [answerIndex + 2], dialogTextConstants.answersB2 [answerIndex + 3]);
					answerIndex = answerIndex + 3;
				} 
				//show three items:
				else if (dialogTextConstants.answersB2 [answerIndex].numResponses == 3) 
				{
					UIManager.instance.SetModalDialogTextMultiple (dialogTextConstants.phrasesB2 [textIndex], dialogTextConstants.answersB2 [answerIndex], dialogTextConstants.answersB2 [answerIndex + 1], dialogTextConstants.answersB2 [answerIndex + 2]);
					answerIndex = answerIndex + 2;
				}
				//show two items:
				else if (dialogTextConstants.answersB2 [answerIndex].numResponses == 2) 
				{
					UIManager.instance.SetModalDialogTextMultiple (dialogTextConstants.phrasesB2 [textIndex], dialogTextConstants.answersB2 [answerIndex], dialogTextConstants.answersB2 [answerIndex + 1]);
					answerIndex = answerIndex + 1;
				}
			} else {
			UIManager.instance.SetModalDialogText (dialogTextConstants.phrasesB2 [textIndex], dialogTextConstants.answersB2 [answerIndex]);
			}
			break;
		case 3:
			//showing a group of items:
			if (dialogTextConstants.answersB3 [answerIndex].isInGroup) {
				//show four items:
				if (dialogTextConstants.answersB3 [answerIndex].numResponses == 4) {
					UIManager.instance.SetModalDialogTextMultiple (dialogTextConstants.phrasesB3 [textIndex], dialogTextConstants.answersB3 [answerIndex], dialogTextConstants.answersB3 [answerIndex + 1], dialogTextConstants.answersB3 [answerIndex + 2], dialogTextConstants.answersB3 [answerIndex + 3]);
					answerIndex = answerIndex + 3;
				} 
				//show three items:
				else if (dialogTextConstants.answersB3 [answerIndex].numResponses == 3) {
					UIManager.instance.SetModalDialogTextMultiple (dialogTextConstants.phrasesB3 [textIndex], dialogTextConstants.answersB3 [answerIndex], dialogTextConstants.answersB3 [answerIndex + 1], dialogTextConstants.answersB3 [answerIndex + 2]);
					answerIndex = answerIndex + 2;
				}
				//show two items:
				else if (dialogTextConstants.answersB3 [answerIndex].numResponses == 2) {
					UIManager.instance.SetModalDialogTextMultiple (dialogTextConstants.phrasesB3 [textIndex], dialogTextConstants.answersB3 [answerIndex], dialogTextConstants.answersB3 [answerIndex + 1]);
					answerIndex = answerIndex + 1;
				}
			} else {
				UIManager.instance.SetModalDialogText (dialogTextConstants.phrasesB3 [textIndex], dialogTextConstants.answersB3 [answerIndex]);
			}
			break;
		case 4:
			//showing a group of items:
			if (dialogTextConstants.answersB4 [answerIndex].isInGroup) {
				//show four items:
				if (dialogTextConstants.answersB4 [answerIndex].numResponses == 4) {
					UIManager.instance.SetModalDialogTextMultiple (dialogTextConstants.phrasesB4 [textIndex], dialogTextConstants.answersB4 [answerIndex], dialogTextConstants.answersB4 [answerIndex + 1], dialogTextConstants.answersB4 [answerIndex + 2], dialogTextConstants.answersB4 [answerIndex + 3]);
					answerIndex = answerIndex + 3;
				} 
				//show three items:
				else if (dialogTextConstants.answersB4 [answerIndex].numResponses == 3) {
					UIManager.instance.SetModalDialogTextMultiple (dialogTextConstants.phrasesB4 [textIndex], dialogTextConstants.answersB4 [answerIndex], dialogTextConstants.answersB4 [answerIndex + 1], dialogTextConstants.answersB4 [answerIndex + 2]);
					answerIndex = answerIndex + 2;
				}
				//show two items:
				else if (dialogTextConstants.answersB4 [answerIndex].numResponses == 2) {
					UIManager.instance.SetModalDialogTextMultiple (dialogTextConstants.phrasesB4 [textIndex], dialogTextConstants.answersB4 [answerIndex], dialogTextConstants.answersB4 [answerIndex + 1]);
					answerIndex = answerIndex + 1;
				}
			} else {
				UIManager.instance.SetModalDialogText (dialogTextConstants.phrasesB4 [textIndex], dialogTextConstants.answersB4 [answerIndex]);
			}
			break;
		case 5:
			//showing a group of items:
			if (dialogTextConstants.answersB5 [answerIndex].isInGroup) {
				//show four items:
				if (dialogTextConstants.answersB5 [answerIndex].numResponses == 4) {
					UIManager.instance.SetModalDialogTextMultiple (dialogTextConstants.phrasesB5 [textIndex], dialogTextConstants.answersB5 [answerIndex], dialogTextConstants.answersB5 [answerIndex + 1], dialogTextConstants.answersB5 [answerIndex + 2], dialogTextConstants.answersB5 [answerIndex + 3]);
					answerIndex = answerIndex + 3;
				} 
				//show three items:
				else if (dialogTextConstants.answersB5 [answerIndex].numResponses == 3) {
					UIManager.instance.SetModalDialogTextMultiple (dialogTextConstants.phrasesB5 [textIndex], dialogTextConstants.answersB5 [answerIndex], dialogTextConstants.answersB5 [answerIndex + 1], dialogTextConstants.answersB5 [answerIndex + 2]);
					answerIndex = answerIndex + 2;
				}
				//show two items:
				else if (dialogTextConstants.answersB5 [answerIndex].numResponses == 2) {
					UIManager.instance.SetModalDialogTextMultiple (dialogTextConstants.phrasesB5 [textIndex], dialogTextConstants.answersB5 [answerIndex], dialogTextConstants.answersB5 [answerIndex + 1]);
					answerIndex = answerIndex + 1;
				}
			} else {
				UIManager.instance.SetModalDialogText (dialogTextConstants.phrasesB5 [textIndex], dialogTextConstants.answersB5 [answerIndex]);
			}
			break;
		case 21: //branch 2_1
			//showing a group of items:
			if (dialogTextConstants.answersB2_1 [answerIndex].isInGroup) {
				//show four items:
				if (dialogTextConstants.answersB2_1 [answerIndex].numResponses == 4) {
					UIManager.instance.SetModalDialogTextMultiple (dialogTextConstants.phrasesB2_1 [textIndex], dialogTextConstants.answersB2_1 [answerIndex], dialogTextConstants.answersB2_1 [answerIndex + 1], dialogTextConstants.answersB2_1 [answerIndex + 2], dialogTextConstants.answersB2_1 [answerIndex + 3]);
					answerIndex = answerIndex + 3;
				} 
				//show three items:
				else if (dialogTextConstants.answersB2_1 [answerIndex].numResponses == 3) {
					UIManager.instance.SetModalDialogTextMultiple (dialogTextConstants.phrasesB2_1 [textIndex], dialogTextConstants.answersB2_1 [answerIndex], dialogTextConstants.answersB2_1 [answerIndex + 1], dialogTextConstants.answersB2_1 [answerIndex + 2]);
					answerIndex = answerIndex + 2;
				}
				//show two items:
				else if (dialogTextConstants.answersB2_1 [answerIndex].numResponses == 2) {
					UIManager.instance.SetModalDialogTextMultiple (dialogTextConstants.phrasesB2_1 [textIndex], dialogTextConstants.answersB2_1 [answerIndex], dialogTextConstants.answersB2_1 [answerIndex + 1]);
					answerIndex = answerIndex + 1;
				}
			} else {
				UIManager.instance.SetModalDialogText (dialogTextConstants.phrasesB2_1 [textIndex], dialogTextConstants.answersB2_1 [answerIndex]);
			}
			break;
		case 22: //branch 2_2
			//showing a group of items:
			if (dialogTextConstants.answersB2_2 [answerIndex].isInGroup) {
				//show four items:
				if (dialogTextConstants.answersB2_2 [answerIndex].numResponses == 4) {
					UIManager.instance.SetModalDialogTextMultiple (dialogTextConstants.phrasesB2_2 [textIndex], dialogTextConstants.answersB2_2 [answerIndex], dialogTextConstants.answersB2_2 [answerIndex + 1], dialogTextConstants.answersB2_2 [answerIndex + 2], dialogTextConstants.answersB2_2 [answerIndex + 3]);
					answerIndex = answerIndex + 3;
				} 
				//show three items:
				else if (dialogTextConstants.answersB2_2 [answerIndex].numResponses == 3) {
					UIManager.instance.SetModalDialogTextMultiple (dialogTextConstants.phrasesB2_2 [textIndex], dialogTextConstants.answersB2_2 [answerIndex], dialogTextConstants.answersB2_2 [answerIndex + 1], dialogTextConstants.answersB2_2 [answerIndex + 2]);
					answerIndex = answerIndex + 2;
				}
				//show two items:
				else if (dialogTextConstants.answersB2_2 [answerIndex].numResponses == 2) {
					UIManager.instance.SetModalDialogTextMultiple (dialogTextConstants.phrasesB2_2 [textIndex], dialogTextConstants.answersB2_2 [answerIndex], dialogTextConstants.answersB2_2 [answerIndex + 1]);
					answerIndex = answerIndex + 1;
				}
			} else {
				UIManager.instance.SetModalDialogText (dialogTextConstants.phrasesB2_2 [textIndex], dialogTextConstants.answersB2_2 [answerIndex]);
			}
			break;
		case 41: //branch 4_1
			//showing a group of items:
			if (dialogTextConstants.answersB4_1 [answerIndex].isInGroup) {
				//show four items:
				if (dialogTextConstants.answersB4_1 [answerIndex].numResponses == 4) {
					UIManager.instance.SetModalDialogTextMultiple (dialogTextConstants.phrasesB4_1 [textIndex], dialogTextConstants.answersB4_1 [answerIndex], dialogTextConstants.answersB4_1 [answerIndex + 1], dialogTextConstants.answersB4_1 [answerIndex + 2], dialogTextConstants.answersB4_1 [answerIndex + 3]);
					answerIndex = answerIndex + 3;
				} 
				//show three items:
				else if (dialogTextConstants.answersB4_1 [answerIndex].numResponses == 3) {
					UIManager.instance.SetModalDialogTextMultiple (dialogTextConstants.phrasesB4_1 [textIndex], dialogTextConstants.answersB4_1 [answerIndex], dialogTextConstants.answersB4_1 [answerIndex + 1], dialogTextConstants.answersB4_1 [answerIndex + 2]);
					answerIndex = answerIndex + 2;
				}
				//show two items:
				else if (dialogTextConstants.answersB4_1 [answerIndex].numResponses == 2) {
					UIManager.instance.SetModalDialogTextMultiple (dialogTextConstants.phrasesB4_1 [textIndex], dialogTextConstants.answersB4_1 [answerIndex], dialogTextConstants.answersB4_1 [answerIndex + 1]);
					answerIndex = answerIndex + 1;
				}
			} else {
				UIManager.instance.SetModalDialogText (dialogTextConstants.phrasesB4_1 [textIndex], dialogTextConstants.answersB4_1 [answerIndex]);
			}
			break;
		case 42: //branch 4_2
			//showing a group of items:
			if (dialogTextConstants.answersB4_2 [answerIndex].isInGroup) {
				//show four items:
				if (dialogTextConstants.answersB4_2 [answerIndex].numResponses == 4) {
					UIManager.instance.SetModalDialogTextMultiple (dialogTextConstants.phrasesB4_2 [textIndex], dialogTextConstants.answersB4_2 [answerIndex], dialogTextConstants.answersB4_2 [answerIndex + 1], dialogTextConstants.answersB4_2 [answerIndex + 2], dialogTextConstants.answersB4_2 [answerIndex + 3]);
					answerIndex = answerIndex + 3;
				} 
				//show three items:
				else if (dialogTextConstants.answersB4_2 [answerIndex].numResponses == 3) {
					UIManager.instance.SetModalDialogTextMultiple (dialogTextConstants.phrasesB4_2 [textIndex], dialogTextConstants.answersB4_2 [answerIndex], dialogTextConstants.answersB4_2 [answerIndex + 1], dialogTextConstants.answersB4_2 [answerIndex + 2]);
					answerIndex = answerIndex + 2;
				}
				//show two items:
				else if (dialogTextConstants.answersB4_2 [answerIndex].numResponses == 2) {
					UIManager.instance.SetModalDialogTextMultiple (dialogTextConstants.phrasesB4_2 [textIndex], dialogTextConstants.answersB4_2 [answerIndex], dialogTextConstants.answersB4_2 [answerIndex + 1]);
					answerIndex = answerIndex + 1;
				}
			} else {
				UIManager.instance.SetModalDialogText (dialogTextConstants.phrasesB4_2 [textIndex], dialogTextConstants.answersB4_2 [answerIndex]);
			}
			break;
		default:
			break;
		}


	}

}
