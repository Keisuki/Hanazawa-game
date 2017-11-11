using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceResize : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 bottomleft = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 10));
		Vector3 topright = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, 10));
		float xwidth = Mathf.Abs (topright.x - bottomleft.x);
		float ywidth = Mathf.Abs (topright.y - bottomleft.y);
		float newscale = xwidth / 6.66f;
		transform.Find ("Interface").localScale = new Vector3 (newscale, newscale, 1);
		transform.Find ("MenuButtonInterface").localPosition = new Vector3 (xwidth / 2.0f, ywidth / 2.0f, 9);
	}
}
