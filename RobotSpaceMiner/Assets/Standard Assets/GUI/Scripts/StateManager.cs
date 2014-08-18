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

    public void TogglePause()
    {
        paused = !paused;
    }
    public void GameOver()
    {
        currentState = State.GAMEOVER;
    }

}
