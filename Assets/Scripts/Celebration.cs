﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Celebration : MonoBehaviour {
	bool fireworks = false;
	int stepsUntilFirework = 20;
	public GameObject fireworkPrefab;
	public GameObject particlePrefab;
	// Use this for initialization
	void Start () {
		//fireworks = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (fireworks) {
			stepsUntilFirework--;
			if (stepsUntilFirework <= 0) {
				stepsUntilFirework = Random.Range (120, 240);
				createParticle (new Vector3 (Random.Range (-3, 3), -5, 0), new Vector3 (Random.Range (-2.0f, 2.0f), Random.Range (6.0f, 8.0f), 0), 1.25f, true);
			}
		}
	}

	void createParticle(Vector3 localPos, Vector3 velocity, float? expiryTime, bool isFirework)
	{
		GameObject q;
		if (isFirework) {
			q = Instantiate (fireworkPrefab, transform);
		} else {
			q = Instantiate (particlePrefab, transform);
		}
		q.transform.localPosition = localPos;
		q.GetComponent<FireworkParticle> ().Initialise (velocity, expiryTime, isFirework);
	}

	public void onWin()
	{
		fireworks = true;
	}

	public static void globalCreateParticle(Vector3 localPos, Vector3 velocity, float? expiryTime, bool isFirework)
	{
		GameObject.Find ("Celebration").GetComponent<Celebration> ().createParticle (localPos, velocity, expiryTime, isFirework);
	}

	public static void globalWin()
	{
		GameObject.Find ("Celebration").GetComponent<Celebration> ().onWin ();
	}
}
