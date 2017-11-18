using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour {

	public void OnResumeButton()
	{
		if (PlayerPrefs.GetString ("CurrentSave") != null) {
			GameController.loadGame (PlayerPrefs.GetString ("CurrentSave"));
		}
	}

	public void OnStartButton()
	{
		GameController.startGame ();
	}
}
