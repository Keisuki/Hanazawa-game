using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBackground : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 bottomleft = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 10));
		Vector3 topright = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, 10));
		float xwidth = Mathf.Abs (topright.x - bottomleft.x);
		float ywidth = Mathf.Abs (topright.y - bottomleft.y);
		float xsat = xwidth / 4.0f;
		float ysat = ywidth / 6.0f;
		float sat = xsat;
		if (ysat > sat) {
			sat = ysat;
		}
		float newScale = sat;
		transform.localScale = new Vector3 (newScale, newScale, 1);

	}
}
