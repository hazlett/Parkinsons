using UnityEngine;
using System.Collections;

public class GameStats : MonoBehaviour {
	
	private int score;
	private int distance;
	private float time;
	public int Score {
		get { return score; }
	}
	public int Distance {
		get { return distance; }
	}

	public void SetDistance(int value)
	{
		if (StateManager.Instance.CurrentState == StateManager.State.PLAYING)
			this.distance = value;
	}
	public void AddScore(int value)
	{
		if (StateManager.Instance.CurrentState == StateManager.State.PLAYING)
			this.score += value;
	}
	public void SetTime(float value)
	{
		this.time = value;
	}
	
	public void LogStats()
	{
		DataLogger.Instance.Log("Time: " + time +
		                        " Score: " + Score +
		                        " Distance: " + Distance);
	}

}
