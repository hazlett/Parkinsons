using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameStats stats;
    public float TotalTime;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        switch (StateManager.Instance.CurrentState)
        {
            case StateManager.State.EPILOGUE:
                // Finish out moving then transition to GAMEOVER
                break;
            case StateManager.State.PLAYING:
                // Push is possible
                break;
            case StateManager.State.GAMEOVER:
                stats.LogStats();
                break;
            default:
                Debug.LogError("Unknown State");
                break;
        }
    }
}
