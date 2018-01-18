using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialBrowser : MonoBehaviour {
	public GameObject[] panels;
	public Text PreviousText;
	public Text NextText;
	int currentPanel = 0;
	void Start()
	{
		reset ();
	}
	public void next()
	{
		currentPanel++;
		updatePanels ();
	}
	public void previous()
	{
		currentPanel--;
		updatePanels ();
	}
	private void fixRange()
	{
		if (currentPanel < 0) {
			currentPanel = 0;
		}
		if (currentPanel >= panels.Length) {
			currentPanel = panels.Length - 1;
		}
	}
	private void updatePanels()
	{
		fixRange ();
		for (int i = 0; i < panels.Length; i++) {
			panels [i].SetActive (false);
		}
		panels [currentPanel].SetActive (true);
		switch(currentPanel)
		{
		case(0):
			{
				PreviousText.text = "";
				NextText.text = "Next";
				break;
			}
		case(1):
			{
				PreviousText.text = "Previous";
				NextText.text = "Next";
				break;
			}
		case(2):
			{
				PreviousText.text = "Previous";
				NextText.text = "";
				break;
			}
		}
	}
	public void reset()
	{
		currentPanel = 0;
		updatePanels ();
	}
}
