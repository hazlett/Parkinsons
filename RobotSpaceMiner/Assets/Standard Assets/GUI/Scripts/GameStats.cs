using UnityEngine;
using System.Collections;

public class GameStats : MonoBehaviour {
	
	private int score;

	public int Score {
		get { return score;}
	}

	public void AddScore(int value)
	{
		this.score += value;
	}
	
	public void LogStats()
	{
        DataLogger.Instance.Log("Total Score: " + Score);
	}

}
