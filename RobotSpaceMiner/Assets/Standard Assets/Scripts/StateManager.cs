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
        currentState = State.PLAYING;
    }
    public static StateManager Instance
    {
        get
        {
            return instance;
        }
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
