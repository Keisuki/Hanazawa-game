﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {
	float lastClick = 0.0f;
	public float tapTimeSensitivity = 0.25f;
	public float dragCameraMultiplier;
	Vector2 lastHoldPosition;

	void Update () {
		Vector2 currentPosition = Input.mousePosition;
		lastClick += Time.deltaTime;
		if (Input.GetMouseButtonDown (0)) {
			lastClick = 0;
			lastHoldPosition = currentPosition;
		}
		if (Input.GetMouseButtonUp (0)) {
			if (lastClick < tapTimeSensitivity) {
				OnTap (lastHoldPosition);
			}
		}
		if (Input.GetMouseButton (0)) {
			if (lastClick >= tapTimeSensitivity) {
				OnDrag (currentPosition - lastHoldPosition); 
			}
		}

		lastHoldPosition = currentPosition;
	}

	void OnTap(Vector2 position)
	{
		Vector3 hitPos = GetComponent<Camera> ().ScreenToWorldPoint (position);
		Collider2D coll = Physics2D.OverlapPoint (hitPos);
		if (coll != null) {
			Tappable q = coll.GetComponent<Tappable> ();
			if (q != null) {
				q.OnTap ();
				return;
			}
		}
		OnBackgroundTap (hitPos);
	}

	void OnDrag(Vector2 delta)
	{
		Vector3 newPosition = transform.position + dragCameraMultiplier * (Vector3)delta;
		Vector3 topRight = GameController.GetTopRightPos ();
		if (newPosition.x < 0) {
			newPosition.x = 0;
		}
		if (newPosition.y < 0) {
			newPosition.y = 0;
		}
		if (newPosition.x > topRight.x) {
			newPosition.x = topRight.x;
		}
		if (newPosition.y > topRight.y) {
			newPosition.y = topRight.y;
		}
		transform.position = newPosition;

	}

	void OnBackgroundTap(Vector3 worldPos)
	{

	}

	public void OnScale(float factor)
	{
		Vector3 newPosition = transform.position;
		newPosition.x = newPosition.x * factor;
		newPosition.y = newPosition.y * factor;
		transform.position = newPosition;
	}
}

public abstract class Tappable : MonoBehaviour 
{
	public abstract void OnTap();
}
