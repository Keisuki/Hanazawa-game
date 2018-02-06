using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceResize : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void OnPreRender () {
		Vector3 bottomleft = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 10));
		Vector3 topright = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, 10));
		float xwidth = Mathf.Abs (topright.x - bottomleft.x);
		float ywidth = Mathf.Abs (topright.y - bottomleft.y);
		float topRatio = xwidth / 6f;
		if (topRatio > 1) {
			topRatio = 1;
		} 
		transform.Find ("Counters").localScale = new Vector3 (topRatio, topRatio, topRatio);
		transform.Find ("MenuButtonInterface").localScale = new Vector3 (topRatio, topRatio, topRatio);
		if (xwidth > ywidth) {
			float newscale = xwidth / 13.32f;
			transform.Find ("Interface").localScale = new Vector3 (newscale, newscale, 1);
			transform.Find ("Interface").localPosition = new Vector3 (bottomleft.x + newscale * 3.34f - transform.position.x, bottomleft.y - transform.position.y  + newscale*1.12f, 9);
		} else {
			float newscale = xwidth / 6.66f;
			transform.Find ("Interface").localScale = new Vector3 (newscale, newscale, 1);
			transform.Find ("Interface").localPosition = new Vector3 (0, bottomleft.y - transform.position.y  + newscale*1.12f, 9);
		}

		transform.Find ("MenuButtonInterface").localPosition = new Vector3 (xwidth / 2.0f, ywidth / 2.0f, 9);
		transform.Find ("Counters").localPosition = new Vector3 (-xwidth / 2.0f, ywidth / 2.0f, 9);
		transform.Find ("WinLoss").localScale = new Vector3 (xwidth / 6.66f, xwidth / 6.66f, 1);

	}
}
