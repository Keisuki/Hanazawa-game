using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoss : MonoBehaviour {
	public GameObject win;
	public GameObject lose;
	public float delay;
	private float showWinTime = float.MaxValue;
	private float showLossTime = float.MaxValue;
	void Update()
	{
		if (Time.time > showWinTime) {
			win.SetActive (true);
		}
		if (Time.time > showLossTime) {
			lose.SetActive (true);
		}
	}
	public void showWin()
	{
		showWinTime = Time.time + delay;
	}
	public void showLoss()
	{
		showLossTime = Time.time + delay;
	}
	public static void ShowWin()
	{
		GameObject.Find ("WinLoss").GetComponent<WinLoss> ().showWin ();
	}
	public static void ShowLoss()
	{
		GameObject.Find ("WinLoss").GetComponent<WinLoss> ().showLoss ();
	}
}
