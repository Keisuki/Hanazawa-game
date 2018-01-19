using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyMenuStats : MonoBehaviour {
	public Text easy;
	public Text medium;
	public Text hard;
	public Text veryHard;
	public Text statsText;
	private bool statsShown = false;
	public void toggleStats()
	{
		statsShown = !statsShown;
		updateStats (statsShown);
	}
	public void updateStats(bool show)
	{
		if (show) {
			easy.text = getStatsString (1);
			medium.text = getStatsString (2);
			hard.text = getStatsString (3);
			veryHard.text = getStatsString (4);
			statsText.text = "Hide";
		} else {
			easy.text = "Easy";
			medium.text = "Medium";
			hard.text = "Hard";
			veryHard.text = "Very Hard";
			statsText.text = "Stats";
		}
	}
	private string getStatsString(int difficulty)
	{
		int wins = Stats.getWins (difficulty);
		int losses = Stats.getAttempts (difficulty) - wins;
		return "W: " + wins.ToString () + " - L: " + losses.ToString ();
	}
}
