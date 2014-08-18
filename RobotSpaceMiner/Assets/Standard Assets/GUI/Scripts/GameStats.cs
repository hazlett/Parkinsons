using UnityEngine;
using System.Collections;

public class GameStats : MonoBehaviour {

	private int upDowns;
	private int upDownsFailed;

    public Move mover;

    public float DistanceTraveled {
        get { return mover.DistanceTraveled; }
    }
	public int UpDownsCompleted {
		get { return upDowns; }
	}
	public int UpDownsFailed {
		get { return upDownsFailed; }
	}

	public void UpDown(bool completed)
	{
		if (completed)
			upDowns++;
		else
			upDownsFailed++;
	}

	public void LogStats()
	{
        DataLogger.Instance.Log("Distance traveled: " + DistanceTraveled);
	}

}
