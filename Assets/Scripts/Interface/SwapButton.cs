using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapButton : Tappable {

	public override void OnTap ()
	{

		GameController.SwapPieces ();
	}
}
