using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomButton : Tappable {
	public int step;
	public override void OnTap ()
	{

		GameController.Zoom (step);
	}
}
