using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Chest : MonoBehaviour 
{
	public bool isUsed = false;
	public AudioClip chestSound;
	public Sprite openedChest;
	 
	/// <summary>
	/// Opens the chest.
	/// </summary>
	public void OpenChest()
	{
		isUsed = true;
		gameObject.GetComponent<SpriteRenderer>().sprite = openedChest;
		SoundManager.instance.PlayPlayersSingle(chestSound);

		switch (GameManager.instance.chestOpened)
		{
			case 0:
				GameManager.instance.ShowSelectDialogPanel ();
				break;
			case 1:
				GameManager.instance.ShowModalDialogPanel ("You've got your prize already, you, greedy savage...", "mmm...Ok");
				break;
			case 2:
				GameManager.instance.ShowModalDialogPanel ("Still looking for something? There is nothing for you here.", "mmm...Ok");
				break;
			case 3:
				GameManager.instance.ShowModalDialogPanel ("You've found a shiny tiny golden key among the old rags and dirt.", "mmm...Ok");
				PlayerManager.instance.hasKey = true;
				GameObject.Find("/Canvas/ModalDialog/KeyImage").SetActive(true);
				GameObject.Find("/Canvas/HUD/KeyImage").SetActive(true);
				break;
			//LVL 4 items:
			case 4:
				GameManager.instance.ShowModalDialogPanel ("What a nice wooden stake. You pick it up, but it breaks in your hand. You notice note: Made in China on the hammer.", "Uhggrrr");
				GameObject.Find("/Canvas/ModalDialog/stakeImage").SetActive(true);
				break;
			case 5:
				GameManager.instance.ShowModalDialogPanel ("What a nice shiny thing. Unfortunately it will work against vampires only in 200 000 years.", "Uhggrrr");
				GameObject.Find("/Canvas/ModalDialog/stakeImage").SetActive(false);	
				GameObject.Find("/Canvas/ModalDialog/crusifixImage").SetActive(true);
				break;
			case 6:
				GameManager.instance.ShowModalDialogPanel ("What a smelly chest. May be this will help", "Yummy!");
				GameObject.Find("/Canvas/ModalDialog/crusifixImage").SetActive(false);	
				GameObject.Find("/Canvas/ModalDialog/garlicImage").SetActive(true);
				GameObject.Find("/Canvas/HUD/garlicImage").SetActive(true);
				PlayerManager.instance.hasGarlic = true;
				break;
			default: break;
		}
		GameManager.instance.chestOpened++;
		Debug.Log (GameManager.instance.chestOpened);
	}
}