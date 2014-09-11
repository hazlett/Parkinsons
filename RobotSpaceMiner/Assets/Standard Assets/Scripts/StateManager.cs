using UnityEngine;
using System.Collections;

public class StateManager {
    
    public enum State
    {
        INTRO,
        PREGAME,
        PLAYING,
        EPILOGUE,
        GAMEOVER,
        MENU
    };

	public bool AutoRun;

	private bool playing;
	public bool IsPlaying { get { return playing; } set { playing = value; } }

	private bool fireHazards = false;
	public bool FireHazards { get { return fireHazards; } set { fireHazards = value; } }

    private bool roadblocks = false;
    public bool Roadblocks { get { return roadblocks; } set { roadblocks = value; } }

    private bool cave = false;
    public bool Cave { get { return cave; } set { cave = value; } } 

	private bool timerPause = true;
	public bool TimerPause { get { return timerPause; } }
    private static StateManager instance = new StateManager();
    private State currentState;
    public State CurrentState { get { return currentState; } set { currentState = value; } }
    private bool paused;
    public bool Paused { get { return paused; } }
    private StateManager()
    {
        paused = false;
        currentState = State.PREGAME;
		playing = false;
    }
    public static StateManager Instance
    {
        get
        {
            return instance;
        }
    }
    public string GetMessage()
    {
        string message = "";
        if (CurrentState == State.GAMEOVER)
        {
            message = "";
        }
        else if (AutoRun)
        {
            message = "AUTO RUN ENABLED";
        }
        else if (currentState == State.PREGAME)
        {
            message = "Please stand and raise your right hand to begin";
        }
        else if (FireHazards)
        {
            message = "Push blue squares with your hand as they appear";
        }
        else if (Roadblocks)
        {
            message = "Swipe left and right to avoid obstacles";
        }
        else
        {
            message = "Follow the lever\nStand and sit to move up the hill";
        }
        return message;
    }
	public void StartTimer()
	{
		timerPause = false;
	}
	public void ResetTimer()
	{
		timerPause = true;
	}
    public void TogglePause()
    {
        paused = !paused;
    }
    public void GameOver()
    {
        currentState = State.GAMEOVER;
    }

}
