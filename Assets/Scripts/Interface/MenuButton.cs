using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : Tappable {

	public override void OnTap ()
	{
		Debug.Log ("Menu button was tapped");
		PlayerPrefs.SetString ("CurrentSave", GameController.SaveGame ());
		SceneManager.LoadScene (0);

	}
}
