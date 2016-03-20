using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class DialogTextConstants {

	#region Variables
	[HideInInspector]
	public int PhraseId;
	//List of Arrays for each available branch in conversation
	[HideInInspector]
	public List<String> phrasesB1;
	[HideInInspector]
	public List<Answer> answersB1;
	[HideInInspector]
	public List<String> phrasesB2;
	[HideInInspector]
	public List<Answer> answersB2;
	[HideInInspector]
	public List<String> phrasesB2_1;
	[HideInInspector]
	public List<Answer> answersB2_1;
	[HideInInspector]
	public List<String> phrasesB2_2;
	[HideInInspector]
	public List<Answer> answersB2_2;
	[HideInInspector]
	public List<String> phrasesB3;
	[HideInInspector]
	public List<Answer> answersB3;
	[HideInInspector]
	public List<String> phrasesB4;
	[HideInInspector]
	public List<Answer> answersB4;
	[HideInInspector]
	public List<String> phrasesB4_1;
	[HideInInspector]
	public List<Answer> answersB4_1;
	[HideInInspector]
	public List<String> phrasesB4_2;
	[HideInInspector]
	public List<Answer> answersB4_2;
	[HideInInspector]
	public List<String> phrasesB5;
	[HideInInspector]
	public List<Answer> answersB5;
	#endregion


	/// <summary>
	/// Generates the phrase list for modal dialog based on the level.
	/// </summary>
	/// <param name="Level">Level.</param>
	public void SetPhraseListForModalDialog(int Level) {
		switch (Level) {
		#region Lvl1
		case 1:
			phrasesB1 = new List<String> ();
			phrasesB1.Add ("Hello there, young stranger! Are you tough in face of danger?");
			phrasesB1.Add ("Then play my GAME if you are clever.");
			phrasesB1.Add ("You won't forget this experience never. As you are now the First Player Ever...");
			phrasesB1.Add ("As Boobaraka did not eat a lot of berries he was not very smart, so he got himself dragged into this \"Game\" as a First Player Ever...");
			 
			answersB1 = new List<Answer> ();
			answersB1.Add (new Answer());
			answersB1[0].action = 0;
			answersB1[0].buttonText = "Me Boobaraka, tough as ever.";
			answersB1.Add (new Answer());
			answersB1[1].action = 0;
			answersB1[1].buttonText = "...";
			answersB1.Add (new Answer());
			answersB1[2].action = 0;
			answersB1[2].buttonText = "Whatever";
			answersB1.Add (new Answer());
			answersB1[3].action = 1;
			answersB1[3].buttonText = "mmm...Ever";
			break;
			#endregion
			#region Lvl2
		case 2:
			//BRANCH 1
			phrasesB1 = new List<String> ();
			phrasesB1.Add ("Wow, I see you made it through. How about quest for you?");
			phrasesB1.Add ("There's no need in being rude. Wife is definitely good. Do you want the girl you met, or you never saw her yet?");
			phrasesB1.Add ("Don't you be upset, my friend. You will find her at the end.");
			phrasesB1.Add ("You will have to set her free.");
			phrasesB1.Add ("Then you go without rest. Take all flowers, you can see. Bring them afterwards to me.");

			answersB1 = new List<Answer> ();
			answersB1.Add (new Answer());
			answersB1[0].action = 0;
			answersB1[0].buttonText = "Me need wife and no more game, life without wife is lame.";
			answersB1.Add (new Answer());
			answersB1[1].action = 0;
			answersB1[1].buttonText = "Boobaraka did not see. He is lonely, let him be.";
			answersB1.Add (new Answer());
			answersB1[2].action = 0;
			answersB1[2].buttonText = "Will you bring a wife for me?";
			answersB1.Add (new Answer());
			answersB1[3].action = 0;
			answersB1[3].buttonText = "I am ready for the quest.";
			answersB1.Add (new Answer());
			answersB1[4].action = 1;
			answersB1[4].buttonText = "...";
			//BRANCH 2
			phrasesB2 = new List<String> ();
			phrasesB2.Add ("Not all flowers found yet! ");
			phrasesB2.Add ("Flowers don't grow in the cave...");
			phrasesB2.Add ("Don't you to take this quest for granted. The toilet is where they are planted.");

			answersB2 = new List<Answer> ();
			answersB2.Add (new Answer());
			answersB2[0].action = 0;
			answersB2[0].buttonText = "Flowers there smell very bad!";
			answersB2.Add (new Answer());
			answersB2[1].action = 0;
			answersB2[1].buttonText = "...That room makes a throw up wave.";
			answersB2.Add (new Answer());
			answersB2[2].action = 2;
			answersB2[2].buttonText = "...";
			//BRANCH 3
			phrasesB3 = new List<String> ();
			phrasesB3.Add ("All flowers are here indeed. You completed your great deed.");
			phrasesB3.Add ("I smell stink and so will she. Here is something you will need:");

			answersB3 = new List<Answer> ();
			answersB3.Add (new Answer());
			answersB3[0].action = 0;
			answersB3[0].buttonText = "Did you bring a wife to me?";
			answersB3.Add (new Answer());
			answersB3[1].action = 3;
			answersB3[1].buttonText = "...";

			//BRANCH 4
			phrasesB4 = new List<String> ();
			phrasesB4.Add ("You are not the throwing one, but the spear you need is done!");

			answersB4 = new List<Answer> ();
			answersB4.Add (new Answer());
			answersB4[0].action = 99;
			answersB4[0].buttonText = "Now make sure you can run!";

			//BRANCH 5
			phrasesB5 = new List<String> ();
			phrasesB5.Add ("You are not the throwing one, but the spear you need is done!");

			answersB5 = new List<Answer> ();
			answersB5.Add (new Answer());
			answersB5[0].action = 99;
			answersB5[0].buttonText = "Now make sure you can run!";

			break;
			#endregion
			#region Lvl3
		case 3:
			break;
			#endregion
			#region Lvl4
		case 4:
			//BRANCH 1
			phrasesB1 = new List<String> ();
			phrasesB1.Add ("How dare you touching my wife?!");

			answersB1 = new List<Answer> ();
			answersB1.Add (new Answer());
			answersB1[0].action = 1;
			answersB1[0].buttonText = "I never saw her in my life!";

			//BRANCH 2
			phrasesB2 = new List<String> ();
			phrasesB2.Add ("AGGGHHH What's that smell?!");

			answersB2 = new List<Answer> ();
			answersB2.Add (new Answer());
			answersB2[0].action = 1;
			answersB2[0].buttonText = "...";

			//BRANCH 3
			phrasesB3 = new List<String> ();
			phrasesB3.Add ("You're so brave, my hero...ummm");
			phrasesB3.Add ("Eat a mushroom");

			answersB3 = new List<Answer> ();
			answersB3.Add (new Answer());
			answersB3[0].action = 0;
			answersB3[0].buttonText = "...";
			answersB3.Add (new Answer());
			answersB3[1].action = 99;
			answersB3[1].buttonText = "Ugh what's that taste";

			break;
			#endregion
			#region Lvl5
		case 5:

			break;
			#endregion
			#region Lvl6
		case 6:
			//BRANCH 1
			phrasesB1 = new List<String> ();
			phrasesB1.Add ("And here we meet again, young lad.");
			phrasesB1.Add ("Tell what happened, don't be mad!");

			//TODO need to show multiple answers in same dialog here!!! need a way to set them based on branch/conversation.
			answersB1 = new List<Answer> ();
			answersB1.Add (new Answer());
			answersB1[0].action = 0;
			answersB1[0].buttonText = "I don't like this game - it's bad.";
			answersB1.Add (new Answer());
			answersB1[1].action = 1;
			answersB1[1].buttonText = "I can't find a rhyme for that."; 
			//Answers to Phrase 2 go to different branches
			answersB1.Add (new Answer());
			answersB1[2].action = 5; //branch 2
			answersB1[2].buttonText = "I got bitten by a bat.";
			answersB1.Add (new Answer());
			answersB1[3].action = 6; //branch 3
			answersB1[3].buttonText = "I am tired! I am sad!";
			answersB1.Add (new Answer());
			answersB1[3].action = 7; //branch 4
			answersB1[3].buttonText = "Where is wife I should have met?";
			answersB1.Add (new Answer());
			answersB1[3].action = 8; //branch 5
			answersB1[3].buttonText = "Looks like soon you will be dead.";

			//BRANCH 2
			phrasesB2 = new List<String> ();
			phrasesB2.Add ("Oh I see. Estel and Fred.");
			phrasesB2.Add ("Don't be sad.");
			phrasesB2.Add ("I am their good old friend.");
			phrasesB2.Add ("They are nice when they are fed.");
			phrasesB2.Add ("They drink blood if it is red.");

			answersB2 = new List<Answer> ();
			answersB2.Add (new Answer());
			answersB2[0].action = 0;
			answersB2[0].buttonText = "So, you know them?!";
			answersB2.Add (new Answer());
			answersB2[1].action = 0;
			answersB2[1].buttonText = "...";
			answersB2.Add (new Answer());
			answersB2[2].action = 0;
			answersB2[2].buttonText = "...";
			answersB2.Add (new Answer());
			answersB2[3].action = 0;
			answersB2[3].buttonText = "How they eat to not get fat?";
			//two answer group
			answersB2.Add (new Answer());
			answersB2[4].action = 9;//branch 2_1
			answersB2[4].buttonText = "Sounds like it is a threat!"; 
			answersB2.Add (new Answer());
			answersB2[5].action = 10;//branch 2_2
			answersB2[5].buttonText = "I was hurt and almost dead."; 

			//BRANCH 2_1
			phrasesB2_1 = new List<String> ();
			phrasesB2_1.Add ("Nice old tomb is their bed.");
			phrasesB2_1.Add ("Look for it in next room, lad.");

			answersB2_1 = new List<Answer> ();
			answersB2_1.Add (new Answer());
			answersB2_1[0].action = 0;
			answersB2_1[0].buttonText = "...";
			answersB2_1.Add (new Answer());
			answersB2_1[1].action = 4; //switch back to beginning of the dialog
			answersB2_1[1].buttonText = "Hope in sleep they won't get mad.";

			//BRANCH 2_2
			phrasesB2_2 = new List<String> ();
			phrasesB2_2.Add ("There's a lot of ways to that. \nYou can eat in next cave, lad.");
			phrasesB2_2.Add ("Little potion with the red,\nMakes your feeling better, lad.");

			answersB2_2 = new List<Answer> ();
			answersB2_2.Add (new Answer());
			answersB2_2[0].action = 0;
			answersB2_2[0].buttonText = "..?!";
			answersB2_2.Add (new Answer());
			answersB2_2[1].action = 4;//switch back to beginning of the dialog
			answersB2_2[1].buttonText = "I will look to find that";

			//BRANCH 3
			phrasesB3 = new List<String> ();
			phrasesB3.Add ("Is there something you don't get?");
			phrasesB3.Add ("Shield is what you need, I bet!");

			answersB3 = new List<Answer> ();
			answersB3.Add (new Answer());
			answersB3[0].action = 0;
			answersB3[0].buttonText = "Spear is flying to my head!";
			answersB3.Add (new Answer());
			answersB3[1].action = 4;//switch back to beginning of the dialog
			answersB3[1].buttonText = "Interesting, The shield, you said?";

			//BRANCH 4
			phrasesB4 = new List<String> ();
			phrasesB4.Add ("You are halfway through, young lad.");

			answersB4 = new List<Answer> ();
			answersB4.Add (new Answer());
			answersB4[0].action = 11;
			answersB4[0].buttonText = "I wonder if she'd like a pet.";//GOES TO 4_1
			answersB4.Add (new Answer());
			answersB4[1].action = 12;
			answersB4[1].buttonText = "I want something now I meant?";//GOES TO 4_2

			//BRANCH 4_1			
			phrasesB4_1 = new List<String> ();
			phrasesB4_1.Add ("It reminds me of my cat");

			answersB4_1 = new List<Answer> ();
			answersB4_1.Add (new Answer());
			answersB4_1[0].action = 4;
			answersB4_1[0].buttonText = "I will look under the bed.";

			//BRANCH 4_2
			phrasesB4_2 = new List<String> ();
			phrasesB4_2.Add ("Look in next cave under bed.");

			answersB4_2 = new List<Answer> ();
			answersB4_2.Add (new Answer());
			answersB4_2[0].action = 4;
			answersB4_2[0].buttonText = "I will do like you have said.";

			//BRANCH 5
			phrasesB5 = new List<String> ();
			phrasesB5.Add ("You are pulling the wrong thread!");

			answersB5 = new List<Answer> ();
			answersB5.Add (new Answer());
			answersB5[0].action = 4;
			answersB5[0].buttonText = "Easy, oldman, don't get wet."; //If no cat
			answersB5.Add (new Answer());
			answersB5[1].action = 99;
			answersB5[1].buttonText = "Tell this to the cat I met."; //If there is a cat SHOULD END LEVEL... and do stuff.
			break;
			#endregion
			#region Lvl7
		case 7: 
			//BRANCH 1
			phrasesB1 = new List<String> ();
			phrasesB1.Add ("Hello, Boobaraka!");
			phrasesB1.Add ("This game lacks someone called Draka. Once you find him, let him know, that the oldmans cat has grown.");
			phrasesB1.Add ("Draka leaves in deepest caves, he keeps sleeping couple days. If you bring his tooth to me, I am setting yourself free.");
			phrasesB1.Add ("Draka's sleeping and sees cow. He will sleep the day or more, in the cave behind the door. You go now and get his tooth, I will sit and eat lettuce.");

			answersB1 = new List<Answer> ();
			answersB1.Add (new Answer());
			answersB1[0].action = 0;
			answersB1[0].buttonText = "...";
			answersB1.Add (new Answer());
			answersB1[1].action = 0;
			answersB1[1].buttonText = "What?";
			answersB1.Add (new Answer());
			answersB1[2].action = 0;
			answersB1[2].buttonText = "Where is Draka? Tell me now.";
			answersB1.Add (new Answer());
			answersB1[3].action = 1;
			answersB1[3].buttonText = "...";

			//BRANCH 2
			phrasesB2 = new List<String> ();
			phrasesB2.Add ("Who is making me awake? I was dreaming about stake!");
			phrasesB2.Add ("Why the cat is not here now! I don't see him");

			answersB2 = new List<Answer> ();
			answersB2.Add (new Answer());
			answersB2[0].action = 0;
			answersB2[0].buttonText = "Oldman said to wake you up, as his wild cat has grown up.";
			answersB2.Add (new Answer());
			answersB2[1].action = 1;
			answersB2[1].buttonText = "Cat says M-E-O-W!";

			//BRANCH 3
			phrasesB3 = new List<String> ();
			phrasesB3.Add ("Ohh I see you brought a tooth (sets a tooth in his mouth), now I can eat more lettuce!");
			phrasesB3.Add ("Sure, my friend, there's no problém.");

			answersB3 = new List<Answer> ();
			answersB3.Add (new Answer());
			answersB3[0].action = 0;
			answersB3[0].buttonText = "Let me out you, damn old man.";
			answersB3.Add (new Answer());
			answersB3[1].action = 99;
			answersB3[1].buttonText = "...";
			break;
			#endregion
			#region Lvl8
		case 8:
			//BRANCH 1
			phrasesB1 = new List<String> ();
			phrasesB1.Add ("My bones hurt much on rainy weather.");
			phrasesB1.Add ("Escort me to my tomb. I'll pay you further.");

			answersB1 = new List<Answer> ();
			answersB1.Add (new Answer());
			answersB1[0].action = 0;
			answersB1[0].buttonText = "I am the one who brought you up together.";
			answersB1.Add (new Answer());
			answersB1[1].action = 1;
			answersB1[1].buttonText = "I do need clothes from the leather.";

			//BRANCH 2
			phrasesB2 = new List<String> ();
			phrasesB2.Add ("Looks like I totally forgot.");
			phrasesB2.Add ("YES! You are young but not too smart.");
			phrasesB2.Add ("\"My tomb is here!\" Here's where I rot!");

			answersB2 = new List<Answer> ();
			answersB2.Add (new Answer());
			answersB2[0].action = 0;
			answersB2[0].buttonText = "But we were here from the start.";
			answersB2.Add (new Answer());
			answersB2[1].action = 0;
			answersB2[1].buttonText = "What is this sign on the right part?";
			answersB2.Add (new Answer());
			answersB2[2].action = 99;
			answersB2[2].buttonText = "<Take a prize>";
			break;
			#endregion
			#region Lvl9
		case 9:
			break;
			#endregion
			#region Lvl10
		case 10:
			break;
			#endregion
		default:
			break;
		}
	}
}
