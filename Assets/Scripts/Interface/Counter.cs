using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour {
	private int count;
	private int remaining;
	public void setRemaining(int i)
	{
		remaining = i;
		updateVisual ();
	}
	public void setCount(int i)
	{
		count = i;
		updateVisual ();
	}
	public int getRemaining()
	{
		return remaining;
	}
	public int getCount()
	{
		return count;
	}
	private void updateVisual()
	{
		string text = "Tiles Placed: " + count.ToString () + "\nArrows Remaining: " + remaining.ToString ();
		GetComponent<TextMesh> ().text = text;
	}

	public static void UpdateValue(int count, int remaining)
	{
		Counter c = Camera.main.transform.Find ("Counters").Find ("Text").GetComponent<Counter> ();
		c.setCount (count);
		c.setRemaining (remaining);
	}
}
