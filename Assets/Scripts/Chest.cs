using UnityEngine;
using System.Collections;

public class Chest : MonoBehaviour 
{
	public bool isUsed = false;
	public AudioClip chestSound;

	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	/// <summary>
	/// Opens the chest.
	/// </summary>
	public void OpenChest()
	{
		isUsed = true;
		SoundManager.instance.PlayPlayersSingle(chestSound);
		GameManager.instance.ShowSelectDialogPanel();
	}
}
