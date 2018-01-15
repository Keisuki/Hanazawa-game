using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour {
	public GameObject topMenu;
	public GameObject difficultyMenu;
	public GameObject tutorialMenu;

	public void OnResumeButton()
	{
		if (PlayerPrefs.GetString ("CurrentSave") != null) {
			GameController.loadGame (PlayerPrefs.GetString ("CurrentSave"));
		}
	}

	public void OnStartButton()
	{
		difficultyMenu.SetActive (true);
		tutorialMenu.SetActive (false);
		topMenu.SetActive (false);
	}

	public void OnBackButton()
	{
		difficultyMenu.SetActive (false);
		tutorialMenu.SetActive (false);
		topMenu.SetActive (true);
	}

	public void OnStartButtonWithGenerator(int id)
	{
		GameController.startGame (id);
	}
}
