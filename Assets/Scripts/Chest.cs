using UnityEngine;
using System.Collections;

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
			default: break;
		}

		GameManager.instance.chestOpened++;
	}
}