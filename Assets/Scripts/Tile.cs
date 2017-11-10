using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : Tappable {
	public GameController controller;
	public int x;
	public int y;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetTexture(Sprite tex)
	{
		Debug.Log ("Texture set to " + tex.ToString ());
		GetComponent<SpriteRenderer> ().sprite = tex;
	}

	public override void OnTap()
	{
		if (controller != null) {
			controller.onTileTapped (x, y);
		}
	}
}
