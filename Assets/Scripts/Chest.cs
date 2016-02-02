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
				UIManager.instance.ShowSelectDialogPanel ();
				break;
			case 1:
				UIManager.instance.ShowModalDialogPanel ("You've got your prize already, you, greedy savage...", "mmm...Ok");
				break;
			case 2:
				UIManager.instance.ShowModalDialogPanel ("Still looking for something? There is nothing for you here.", "mmm...Ok");
				break;
			case 3:
				UIManager.instance.ShowModalDialogPanel ("You've found a shiny tiny golden key among the old rags and dirt.", "mmm...Ok");
				PlayerManager.instance.hasKey = true;
				GameObject.Find("/Canvas/ModalDialog/KeyImage").SetActive(true);
				GameObject.Find("/Canvas/HUD/KeyImage").SetActive(true);
				break;
			//LVL 4 items:
			case 4:
				UIManager.instance.ShowModalDialogPanel ("What a nice wooden stake. You pick it up, but it breaks in your hand. You notice a note \"Made in China\".", "Uhggrrr");
				UIManager.instance.ShowItem(Constants.Item.Stake);
				break;
			case 5:
				UIManager.instance.ShowModalDialogPanel ("What a nice shiny thing. Unfortunately it will work against vampires only in 200 000 years.", "Uhggrrr");
				UIManager.instance.ShowItem(Constants.Item.Crusifix);
				break;
			case 6:
				UIManager.instance.ShowModalDialogPanel ("What a smelly chest. May be this will help", "Yummy!");
				UIManager.instance.ShowItem(Constants.Item.Garlic);
				PlayerManager.instance.hasGarlic = true;
				break;
			default: break;
		}
		GameManager.instance.chestOpened++;
		Debug.Log (GameManager.instance.chestOpened);
	}
}