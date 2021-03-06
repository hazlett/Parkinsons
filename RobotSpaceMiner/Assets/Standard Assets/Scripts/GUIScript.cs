﻿using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour {

    public DistanceTraveled cart;
    public BasicMovement movement;
	public GUISkin Skin;
	public GameStats stats;
    public Texture2D logo;
    private GUIStyle logoStyle = new GUIStyle();
	private string message = "Please stand and raise your right hand to begin";
    public bool timeOn;
	private Color color;
	private float maxTime, timer, minutes, seconds, warningTime = 5.0f;
    private float nativeVerticalResolution = 1080.0f, scaledResolutionWidth, updateGUI = 0.5f;

    private float timerAlpha = 1.0f;

	void Start()
	{
		maxTime = PlayerSettings.Instance.Timer;
		timer = 0.0f;
		StateManager.Instance.ResetTimer ();
        scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
	}

	void Update()
	{
        message = StateManager.Instance.GetMessage();
		if (StateManager.Instance.CurrentState == StateManager.State.GAMEOVER) {
			return;
		}

        if (StateManager.Instance.Paused)
        {
            Time.timeScale = 0;
            AudioListener.pause = true;
            return;
        }
        else
        {
            Time.timeScale = 1;
            AudioListener.pause = false;
        }
        color = GUI.color;
        if (!StateManager.Instance.TimerPause) {

            timer += Time.deltaTime;
        }
        if (!timeOn)
        {
            timer = 0;
        }

        if (timer > maxTime)
        {
            minutes = 0.0f;
            seconds = 0.0f;

            timerAlpha = timer % 0.5f + 0.2f;
			stats.SetTime(maxTime);
			stats.SetDistance(cart.Distance());
			stats.LogStats();
            StateManager.Instance.CurrentState = StateManager.State.GAMEOVER;
        }
        else if (timer >= maxTime - warningTime)
        {
            minutes = Mathf.FloorToInt((maxTime - timer) / 60.0f);
            seconds = Mathf.FloorToInt(maxTime - timer) % 60;

            timerAlpha = timer % 1.0f;
        }
        else
        {
            minutes = Mathf.FloorToInt((maxTime - timer) / 60.0f);
            seconds = Mathf.FloorToInt(maxTime - timer) % 60;
        }
            
        TimedScreenResize();
	}

	void OnGUI() 
	{  
		GUI.skin = Skin;

        // Scale the GUI to any resolution based on 1920 x 1080 base resolution
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(Screen.height / nativeVerticalResolution, Screen.height / nativeVerticalResolution, 1)); 
		if (StateManager.Instance.IsPlaying)
		{
			if (StateManager.Instance.CurrentState == StateManager.State.GAMEOVER)
			{
				GUILayout.Box ("STATS");
				GUI.Box (new Rect(scaledResolutionWidth - 325, 25, 400, 100),"DISTANCE TRAVELED: " + stats.Distance);
				GUILayout.Box ("SCORE: " + stats.Score);
				GUI.Box(new Rect (scaledResolutionWidth * 0.5f - (scaledResolutionWidth / 10), nativeVerticalResolution * 0.3f - (nativeVerticalResolution / 20), scaledResolutionWidth / 5,  nativeVerticalResolution / 10), 
				        "GAME\nOVER");
				
				if (GUI.Button(new Rect (scaledResolutionWidth * 0.5f - (scaledResolutionWidth / 10), nativeVerticalResolution * 0.5f - (nativeVerticalResolution / 20), scaledResolutionWidth / 5,  nativeVerticalResolution / 10), 
				                "PRESS TO\nPLAY AGAIN"))
				{
					Application.LoadLevel("MainLevel");
				}
				if (GUI.Button(new Rect (scaledResolutionWidth * 0.5f - (scaledResolutionWidth / 10), nativeVerticalResolution * 0.7f - (nativeVerticalResolution / 20), scaledResolutionWidth / 5,  nativeVerticalResolution / 10), 
				               "PRESS TO\nEXIT"))
				{
					Application.Quit();
				}
				return;
			}
		}

        GUI.Box(new Rect(scaledResolutionWidth / 2 - 480,  nativeVerticalResolution - 150, 960, 125), message, "Message");

        if (StateManager.Instance.Paused)
        {
            GUI.Box(new Rect (scaledResolutionWidth * 0.5f - (scaledResolutionWidth / 10), nativeVerticalResolution * 0.5f - (nativeVerticalResolution / 20), scaledResolutionWidth / 5,  nativeVerticalResolution / 10), 
                                "GAME PAUSED");
        }
        color.a = timerAlpha;
        GUI.color = color;
        if (timeOn)
        {
            GUI.Label(new Rect(scaledResolutionWidth / 2 - (scaledResolutionWidth / 10f), nativeVerticalResolution / 2 - (nativeVerticalResolution / 2f) + 20, scaledResolutionWidth / 5,
                                nativeVerticalResolution / 10), string.Format("{0:00}:{1:00}", minutes, seconds));
        }
		ShowGestures ();
	}

	private void ShowGestures()
	{
		color.a = 1.0f;
		GUI.color = color;
        GUI.DrawTexture(new Rect(scaledResolutionWidth - logo.width - 15f, nativeVerticalResolution - logo.height - 15f, logo.width, logo.height), logo);
        GUI.Box(new Rect(scaledResolutionWidth - 525, 0, 500, 75), "Score: " + stats.Score);
        GUI.Box(new Rect(scaledResolutionWidth - 525, 75, 500, 75), "DISTANCE TRAVELED: " + stats.Distance);
        GUILayout.Box("Velocity: " + Mathf.Floor(movement.Velocity()) + " m/s");
	}

    private void TimedScreenResize()
    {
        if (Time.time > updateGUI)
        {
            scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
        }
    }
}

