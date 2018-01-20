using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Advertising : MonoBehaviour {
	public bool adsAreOn = true;
	public int secondsBetweenAds;
	public int timeUntilFirstAd;
	private float previousSecond;
	// Use this for initialization
	void Start () {
		int timeLeft = PlayerPrefs.GetInt ("SecondsUntilNextAd");
		if (timeLeft == 0) {
			PlayerPrefs.SetInt ("SecondsUntilNextAd", timeUntilFirstAd);
		}
	}
	
	// Update is called once per frame
	void Update () {
		float t = Time.time;
		if ((t - previousSecond) > 1) {
			previousSecond = t;
			int timeLeft = PlayerPrefs.GetInt ("SecondsUntilNextAd");
			timeLeft--;
			if (timeLeft <= 0) {
				timeLeft = secondsBetweenAds;
				if (adsAreOn) {
					Debug.Log ("Play an Ad at time " + t.ToString ());
					//Advertisement.Show ();
				}
			}
			PlayerPrefs.SetInt ("SecondsUntilNextAd", timeLeft);
		}
	}
}
