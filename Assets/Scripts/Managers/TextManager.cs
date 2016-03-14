using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class TextManager : MonoBehaviour {
	public static TextManager instance = null;

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

		 DontDestroyOnLoad(gameObject);

	}


	void Start()
	{
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		switch (currentSceneIndex) {
		case 1:
			
			break;
		default: 
			break;
		}
	}

	public void ReturnText(int id, int branch){
//		DialogTextConstants ();
	}

}
