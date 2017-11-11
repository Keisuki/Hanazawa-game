using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateButton : Tappable {

	public override void OnTap ()
	{

		GameController.RotatePiece ();
	}
}
