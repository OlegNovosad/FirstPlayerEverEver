using UnityEngine;
using System.Collections;

public class Tablet : MonoBehaviour {

	public int tabletNum;



	public string setTabletMessage(){
		string tabletMessage = "";
		switch (tabletNum){
		case 1:
			tabletMessage = "Beware! The labyrinth of doomed ahead!!!";
			break;
		case 2:
			tabletMessage = "Watch out for poop. WC ahead.";
			break;
		case 3:
			tabletMessage = "Smash the bat with your head. Really, just hit it.";
			break;
		case 4:
			tabletMessage = "The beauty or the beast?";
			break;
		case 5:
			tabletMessage = "Talk to the old mans hand.";
			break;
		case 6:
			tabletMessage = "Spear throwing area.";
			break;
		default:
			break;
	}
		return tabletMessage;
}
}
