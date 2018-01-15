using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MenuButton : Tappable {

	public override void OnTap ()
	{
		Debug.Log ("Menu button was tapped");
		try {
			PlayerPrefs.SetString ("CurrentSave", GameController.SaveGame ());
		} catch (NullReferenceException e) {
			Debug.Log (e);
		}
		SceneManager.LoadScene (0);

	}
}
