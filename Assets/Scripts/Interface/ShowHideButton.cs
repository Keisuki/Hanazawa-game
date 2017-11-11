using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideButton : Tappable {
	public GameObject toShowHide;
	public bool newActiveState;
	// Use this for initialization
	public override void OnTap ()
	{
		toShowHide.SetActive (newActiveState);
	}
}
