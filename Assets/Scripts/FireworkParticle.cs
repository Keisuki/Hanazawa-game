﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworkParticle : MonoBehaviour {
	float? timeLeft;
	public static float gravity = 2.0f;
	Vector3 velocity;
	bool isFirework;
	public void Initialise(Vector3 initial, float? expiryTime, bool isFirework)
	{
		timeLeft = expiryTime;
		velocity = initial;
		this.isFirework = isFirework;
		if (this.isFirework == false) {
			GetComponent<SpriteRenderer> ().color = Random.ColorHSV (0, 1, 1, 1, 1, 1);
		}
	}

	void Update()
	{
		float dt = Time.deltaTime;
		velocity += dt * new Vector3 (0, -gravity, 0);
		if (timeLeft != null) {
			timeLeft -= dt;
			if (timeLeft < 0) {
				if (isFirework) {
					FireworkExplode ();
				} else {
					Destroy (gameObject);
				}
			}
		}
		transform.localPosition += velocity * dt;
	}

	void FireworkExplode()
	{
		for (int i = 0; i < 8; i++) {
			Celebration.globalCreateParticle (transform.localPosition, new Vector3 (Random.Range (-4.0f, 4.0f), Random.Range (-4.0f, 4.0f), 0), 12.0f, false);
		}
		Destroy (gameObject);
	}

	void OnBecameInvisible()
	{
		if (isFirework) {
			FireworkExplode ();
		} else {
			Destroy (gameObject);
		}
	}

}
