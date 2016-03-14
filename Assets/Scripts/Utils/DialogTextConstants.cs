using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class DialogTextConstants : MonoBehaviour {
	
	public int Level;
	[HideInInspector]
	public int PhraseId;
	//List of Arrays for each available branch in conversation
	[HideInInspector]
	public ArrayList phrasesB1;
	[HideInInspector]
	public ArrayList answersB1;
	[HideInInspector]
	public ArrayList phrasesB2;
	[HideInInspector]
	public ArrayList answersB2;
	[HideInInspector]
	public ArrayList phrasesB2_1;
	[HideInInspector]
	public ArrayList answersB2_1;
	[HideInInspector]
	public ArrayList phrasesB2_2;
	[HideInInspector]
	public ArrayList answersB2_2;
	[HideInInspector]
	public ArrayList phrasesB3;
	[HideInInspector]
	public ArrayList answersB3;
	[HideInInspector]
	public ArrayList phrasesB4;
	[HideInInspector]
	public ArrayList answersB4;
	[HideInInspector]
	public ArrayList phrasesB4_1;
	[HideInInspector]
	public ArrayList answersB4_1;
	[HideInInspector]
	public ArrayList phrasesB4_2;
	[HideInInspector]
	public ArrayList answersB4_2;
	[HideInInspector]
	public ArrayList phrasesB5;
	[HideInInspector]
	public ArrayList answersB5;

	public void SetPhraseListForModalDialog() {
		switch (Level) {
		case 1:
			phrasesB1 = new ArrayList ();
			phrasesB1.Add ("Hello there, young stranger! Are you tough in face of danger?");
			phrasesB1.Add ("Then play my GAME if you are clever.");
			phrasesB1.Add ("You won't forget this experience never. As you are now the First Player Ever...");
			phrasesB1.Add ("Ever!");

			answersB1 = new ArrayList ();
			answersB1.Add ("Me Boobaraka, tough as ever.");
			answersB1.Add ("...");
			answersB1.Add ("mmm...");
			answersB1.Add ("As Boobaraka did not eat a lot of berries he was not very smart, so he got himself dragged into this \"Game\" as a First Player Ever...mmm...Ever");
			break;
		case 2:
			//BRANCH 1
			phrasesB1 = new ArrayList ();
			phrasesB1.Add ("Wow, I see you made it through. How about quest for you?");
			phrasesB1.Add ("There's no need in being rude. Wife is definitely good. Do you want the girl you met, or you never saw her yet?");
			phrasesB1.Add ("Don't you be upset, my friend. You will find her at the end.");
			phrasesB1.Add ("You will have to set her free.");
			phrasesB1.Add ("Then you go without rest. Take all flowers, you can see. Bring them afterwards to me.");

			answersB1 = new ArrayList ();
			answersB1.Add ("Me need wife and no more game, life without wife is lame.");
			answersB1.Add ("Boobaraka did not see. He is lonely, let him be.");
			answersB1.Add ("Will you bring a wife for me?");
			answersB1.Add ("I am is ready for the quest.");
			answersB1.Add ("...");
			//BRANCH 2
			phrasesB2.Add ("Not all flowers found yet! ");
			phrasesB2.Add ("Flowers don't grow in the cave...");
			phrasesB2.Add ("Try to take this quest for granted. The toilet is where they are planted.");

			answersB2.Add ("Flowers there smell very bad!");
			answersB2.Add ("...That room makes a throw up wave.");
			answersB2.Add ("...");
			//BRANCH 3
			phrasesB3.Add ("All flowers are here indeed. You completed your great deed.");
			phrasesB3.Add ("I smell stink and so will she. You may pick your prize for now:");

			answersB3.Add ("Did you bring a wife to me?");
			answersB3.Add ("...");
			break;

		case 3:
			break;
		case 4:
			//BRANCH 1
			phrasesB1.Add ("How dare you touching my wife?!");

			answersB1.Add ("I never saw her in my life!");

			//BRANCH 2
			phrasesB2.Add ("AGGGHHH What's that smell?!");

			answersB2.Add ("...");

			//BRANCH 3
			phrasesB3.Add ("You're so brave, my hero.");
			phrasesB3.Add ("Eat a mushroom");

			answersB3.Add ("...");
			answersB3.Add ("Ugh what's that taste");
			break;
		case 5:

			break;

		case 6:
			//BRANCH 1
			phrasesB1 = new ArrayList ();
			phrasesB1.Add ("And here we meet again, young lad.");
			phrasesB1.Add ("Tell what happened, don't be mad!");

			answersB1 = new ArrayList ();
			answersB1.Add ("I don't like this game - it's bad.");
			answersB1.Add ("I can't find a rhyme for that."); //EXITS THE DIALOG
			answersB1.Add ("I got bitten by a bat.");
			answersB1.Add ("I am tired! I am sad!");
			answersB1.Add ("Where is wife I should have met?");
			answersB1.Add ("Looks like soon you will be dead.");

			//BRANCH 2
			phrasesB2 = new ArrayList ();
			phrasesB2.Add ("Oh I see. Estel and Fred.");
			phrasesB2.Add ("Don't be sad.");
			phrasesB2.Add ("I am their good old friend.");
			phrasesB2.Add ("They are nice when they are fed.");
			phrasesB2.Add ("They drink blood if it is red.");

			answersB2 = new ArrayList ();
			answersB2.Add ("So, you know them?!");
			answersB2.Add ("...");
			answersB2.Add ("...");
			answersB2.Add ("How they eat to not get fat?");
			answersB2.Add ("Sounds like it is a threat!"); //branch 2_1
			answersB2.Add ("I was hurt and almost dead."); //branch 2_2

			//BRANCH 2_1
			phrasesB2_1 = new ArrayList ();
			phrasesB2_1.Add ("Nice old tomb is their bed.");
			phrasesB2_1.Add ("Look for it in next room, lad.");
			phrasesB2_1.Add ("");

			answersB2_1 = new ArrayList ();
			answersB2_1.Add ("...");
			answersB2_1.Add ("Hope in sleep they won't get mad.");

			//BRANCH 2_2
			phrasesB2_2 = new ArrayList ();
			phrasesB2_2.Add ("There's a lot of ways to that. \nYou can eat in next cave, lad.");
			phrasesB2_2.Add ("Little potion with the red,\nMakes your feeling better, lad.");

			answersB2_2 = new ArrayList ();
			answersB2_2.Add ("..?!");
			answersB2_2.Add ("I will look to find that");

			//BRANCH 3
			phrasesB3 = new ArrayList ();
			phrasesB3.Add ("Is there something you don't get?");
			phrasesB3.Add ("Shield is what you need, I bet!");

			answersB3 = new ArrayList ();
			answersB3.Add ("Spear is flying to my head!");
			answersB3.Add ("Interesting, The shield, you said?");

			//BRANCH 4
			phrasesB4 = new ArrayList ();
			phrasesB4.Add ("You are halfway through, young lad.");

			answersB4 = new ArrayList ();
			answersB4.Add ("I wonder if she'd like a pet.");//GOES TO 4_1
			answersB4.Add ("I want something now I meant?");//GOES TO 4_2

			//BRANCH 4_1			
			phrasesB4_1 = new ArrayList ();
			phrasesB4_1.Add ("It reminds me of my cat");

			answersB4_1 = new ArrayList ();
			answersB4_1.Add ("I will look under the bed.");

			//BRANCH 4_2
			phrasesB4_2 = new ArrayList ();
			phrasesB4_2.Add ("Look in next cave under bed.");

			answersB4_2 = new ArrayList ();
			answersB4_2.Add ("I will do like you have said.");

			//BRANCH 5
			phrasesB5 = new ArrayList ();
			phrasesB5.Add ("You are pulling the wrong thread!");

			answersB5 = new ArrayList ();
			answersB5.Add ("Easy, oldman, don't get wet."); //If no cat
			answersB5.Add ("Tell this to the cat I met."); //If there is a cat
			break;
		case 7: 
			//BRANCH 1
			phrasesB1 = new ArrayList ();
			phrasesB1.Add ("Hello, Boobaraka!");
			phrasesB1.Add ("This game lacks someone called Draka. Once you find him, let him know, that the oldmans cat has grown.");
			phrasesB1.Add ("Draka leaves in deepest caves, he keeps sleeping couple days. If you bring his tooth to me, I am setting yourself free.");
			phrasesB1.Add ("Draka's sleeping and sees cow. He will sleep the day or more, in the cave behind the door. You go now and get his tooth, I will sit and eat lettuce.");

			answersB1 = new ArrayList ();
			answersB1.Add ("...");
			answersB1.Add ("What?");
			answersB1.Add ("Where is Draka? Tell me now.");
			answersB1.Add ("...");

			//BRANCH 2
			phrasesB2 = new ArrayList ();
			phrasesB2.Add ("Who is making me awake? I was dreaming about stake!");
			phrasesB2.Add ("Why the cat is not here now! I don't see him");

			answersB2 = new ArrayList ();
			answersB2.Add ("Oldman said to wake you up, as his wild cat has grown up.");
			answersB2.Add ("Cat says M-E-O-W!");

			//BRANCH 3
			phrasesB3 = new ArrayList ();
			phrasesB3.Add ("Ohh I see you brought a tooth (sets a tooth in his mouth), now I can eat more lettuce!");
			phrasesB3.Add ("Sure, my friend, there's no problém.");

			answersB3 = new ArrayList ();
			answersB3.Add ("Let me out you, damn old man.");
			answersB3.Add ("...");
			break;

		case 8:
			//BRANCH 1
			phrasesB1 = new ArrayList ();
			phrasesB1.Add ("My bones hurt much on rainy weather.");
			phrasesB1.Add ("Escort me to my tomb. I'll pay you further.");

			answersB1 = new ArrayList ();
			answersB1.Add ("I am the one who brought you up together.");
			answersB1.Add ("I do need clothes from the leather.");

			//BRANCH 2
			phrasesB2 = new ArrayList ();
			phrasesB2.Add ("Looks like I totally forgot.");
			phrasesB2.Add ("YES! You are young but not too smart.");
			phrasesB2.Add ("\"My tomb is here!\" Here's where I rot!");

			answersB2 = new ArrayList ();
			answersB2.Add ("But we were here from the start.");
			answersB2.Add ("What is this sign on the right part?");
			answersB2.Add ("<Take a prize>");
			break;
		default:
			break;
		}
	}
}
