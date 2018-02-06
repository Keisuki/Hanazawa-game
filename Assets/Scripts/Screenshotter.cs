using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Screenshotter : MonoBehaviour {
	public int i;
	public bool s = false;
	// Use this for initialization
	void Start () {
		
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.A)) {
			s = true;
			Debug.Log ("s pressed");
		}
	}

	// Update is called once per frame
	void OnPostRender() {
		if (Application.platform == RuntimePlatform.WindowsEditor) {
			if (s) {
				Debug.Log (Application.persistentDataPath);
				Texture2D screenImage = new Texture2D (Screen.width, Screen.height);
				screenImage.ReadPixels (new Rect (0, 0, Screen.width, Screen.height), 0, 0);
				screenImage.Apply ();
				byte[] rawData = screenImage.EncodeToPNG ();
				File.WriteAllBytes ("E:\\scrn" + i.ToString () + ".png", rawData);
				i++;
				s = false;
			}
		}
	}
}
