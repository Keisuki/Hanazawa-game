using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Stats {

	public static void addWin(int difficulty)
	{
		string name = getDifficultyName (difficulty) + "Wins";
		PlayerPrefs.SetInt (name, PlayerPrefs.GetInt (name) + 1);
	}
	public static void addAttempt(int difficulty)
	{
		string name = getDifficultyName (difficulty) + "Attempts";
		PlayerPrefs.SetInt (name, PlayerPrefs.GetInt (name) + 1);
	}
	public static int getWins(int difficulty)
	{
		string name = getDifficultyName (difficulty) + "Wins";
		return PlayerPrefs.GetInt (name);
	}
	public static int getAttempts(int difficulty)
	{
		string name = getDifficultyName (difficulty) + "Attempts";
		return PlayerPrefs.GetInt (name);
	}
	private static string getDifficultyName(int difficulty)
	{
		switch (difficulty) {
		case (1):
			{
				return "Easy";
			}
		case (2):
			{
				return "Medium";
			}
		case (3):
			{
				return "Hard";
			}
		case (4):
			{
				return "VeryHard";
			}
		}
		throw new UnityException ("GetDifficultyName with parameter " + difficulty.ToString () + " could not return a value");
	}
}
