using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class Answer{

	public String buttonText;
	/// <summary>
	///  The action on button click: 
	/// 0 - show next text; 
	/// 1 - close the modal dialog & switch the branch to next; 
	/// 2 - close the modal dialog & don't switch branch - reset index to the beginning of the branch; 
	/// 3 - close the modal dialog & launch selection pop up (skills/items etc.) (not ending level!)
	/// Custom branch switch:
	/// 4 - switch the branch to B1 - and close pop up
	/// 5 - switch the branch to B2 - don't close pop up
	/// 6 - switch the branch to B3 - don't close pop up
	/// 7 - switch the branch to B4 - don't close pop up
	/// 8 - switch the branch to B5 - don't close pop up
	/// 9 - switch the branch to B2_1 - don't close pop up
	/// 10 - switch the branch to B2_2 - don't close pop up
	/// 11 - switch the branch to B4_1 - don't close pop up
	/// 12 - switch the branch to B4_2 - don't close pop up
	/// 99 - hide the dialog and proceed with level ending (skills/animation etc.)
	/// 100 - death - restart;
	/// </summary>
	public int action;


	/// <summary>
	/// Handles the click on the answer based on ActionId.
	/// </summary>
	public void AnswerClick(){
		switch (action) 
		{
		case 0:
			//show next text and button
			TextManager.instance.textIndex++;
			TextManager.instance.PopulateModalDialogWithText ();
			break;
		case 1:
			//close the modal dialog & switch the branch to next; 
			TextManager.instance.branch++;
			TextManager.instance.textIndex = 0;
			UIManager.instance.HideModalDialogPanel();
			break;
		case 2:
			//close the modal dialog & don't switch branch - reset index to the beginning of the branch; 
			TextManager.instance.textIndex = 0;
			UIManager.instance.HideModalDialogPanel();
			break;
		case 3:
			//close the modal dialog & launch selection pop up (skills/items etc.) (not ending level!)
			//TODO: need to dev this for chests.
			switch (TextManager.instance.currentLevel) 
			{
			case 1:
				//nothing so far
				break;
			case 2:
				
				UIManager.instance.ShowSelectDialogPanel();
				//TODO switch off colider from an oldman

				break;
			case 3:
				//nothing so far
				break;
			case 4:
				//the changing effects and bat stealing Boobaraka
				break;
			case 5:
				//nothing so far
				break;
			case 6:
				//cat attacking oldman and opening the secret passage
				break;
			case 7:
				//eating lettuce and opening the secret passage
				//switch off colider from an oldman
				break;
			case 8:
				//picking a clothing.
				//opening a secret passage
				//switch off colider from an altar
				break;
			case 9:
				break;
			case 10:
				break;
			default:
				break;
			}

			break;
		case 4:
			//switch the branch to B1 and close pop up
			//multiple handlers for level 6 in order to switch rooms 
			TextManager.instance.branch = 1;
			TextManager.instance.textIndex = 0;
			UIManager.instance.HideModalDialogPanel();
			break;
		case 5:
			//switch the branch to B2 - don't close pop up //????
			TextManager.instance.branch = 2;
			TextManager.instance.textIndex = 0;
			TextManager.instance.PopulateModalDialogWithText ();
			break;
		case 6:
			//switch the branch to B3 - don't close pop up
			TextManager.instance.branch = 3;
			TextManager.instance.textIndex = 0;
			TextManager.instance.PopulateModalDialogWithText ();
			break;
		case 7:
			//switch the branch to B4 - don't close pop up
			TextManager.instance.branch = 4;
			TextManager.instance.textIndex = 0;
			TextManager.instance.PopulateModalDialogWithText ();
			break;
		case 8:
			//switch the branch to B5 - don't close pop up
			TextManager.instance.branch = 5;
			TextManager.instance.textIndex = 0;
			TextManager.instance.PopulateModalDialogWithText ();
			break;
		case 9:
			//switch the branch to B2_1 - don't close pop up
			TextManager.instance.branch = 21;
			TextManager.instance.textIndex = 0;
			TextManager.instance.PopulateModalDialogWithText ();
			break;
		case 10:
			//switch the branch to B2_2 - don't close pop up
			TextManager.instance.branch = 22;
			TextManager.instance.textIndex = 0;
			TextManager.instance.PopulateModalDialogWithText ();
			break;
		case 11:
			//switch the branch to B4_1 - don't close pop up
			TextManager.instance.branch = 41;
			TextManager.instance.textIndex = 0;
			TextManager.instance.PopulateModalDialogWithText ();
			break;
		case 12:
			//switch the branch to B4_2 - don't close pop up
			TextManager.instance.branch = 42;
			TextManager.instance.textIndex = 0;
			TextManager.instance.PopulateModalDialogWithText ();
			break;
		case 99:
			//hide the dialog and proceed with level ending (skills/animation etc.)
			//Multiple endings based on level
			UIManager.instance.HideModalDialogPanel ();
			//Level endings based on dialog click.
			switch (TextManager.instance.currentLevel) 
			{
			case 1:
				//nothing so far
				break;
			case 2:
				//open the secret passage
				for (int i = 0; i < PlayerManager.instance.player.level2Walls.Length; i++) {
//					Destroy (PlayerManager.instance.player.level2Walls[i]);
				}
				//switch off colider from an oldman
				break;
			case 3:
				//nothing so far
				break;
			case 4:
				//the changing effects and bat stealing Boobaraka
				break;
			case 5:
				//nothing so far
				break;
			case 6:
				//cat attacking oldman and opening the secret passage
				break;
			case 7:
				//eating lettuce and opening the secret passage
				//switch off colider from an oldman
				break;
			case 8:
				//picking a clothing.
				//opening a secret passage
				//switch off colider from an altar
				break;
			case 9:
				break;
			case 10:
				break;
			default:
				break;
			}
			break;
		case 100:
			//death - restart; not sure if I need this one.
			break;
		default:
			Debug.Log ("no action id set for the button click");
			break;
		}
	}
}
