using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour {

    public DistanceTraveled cart;
    public BasicMovement movement;
	public GUISkin Skin;
    public bool timerPause;
	public GameStats stats;
    public Texture2D logo, screenFlash;
    private GUIStyle logoStyle = new GUIStyle();

	private Color color;
	private float maxTime = 180.0f, timer, minutes, seconds, warningTime = 5.0f;
    private float nativeVerticalResolution = 1080.0f, scaledResolutionWidth, updateGUI = 0.5f;

    private bool fadingIn = true;

	private float timerAlpha = 1.0f, hitAlpha = 0.0f;

    void Start() {
        timerPause = true;
    }

	void Update()
	{
        
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
        if (!timerPause) {

            timer += Time.deltaTime;
        }

        if (timer > maxTime)
        {
            minutes = 0.0f;
            seconds = 0.0f;

            timerAlpha = timer % 0.5f + 0.2f;

            StateManager.Instance.CurrentState = StateManager.State.EPILOGUE;
        }
        else if (timer >= maxTime - warningTime)
        {
            minutes = Mathf.Floor((maxTime - timer) / 60.0f);
            seconds = (maxTime - timer) % 60;

            timerAlpha = timer % 1.0f;
        }
        else
        {
            minutes = Mathf.Floor((maxTime - timer) / 60.0f);
            seconds = (maxTime - timer) % 60;
        }

        if (!fadingIn)
        {
            DamageFlash();
        }
            
        TimedScreenResize();
	}

	void OnGUI() 
	{  
		GUI.skin = Skin;

        // Scale the GUI to any resolution based on 1920 x 1080 base resolution
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(Screen.height / nativeVerticalResolution, Screen.height / nativeVerticalResolution, 1)); 

        if (StateManager.Instance.Paused)
        {
            GUI.Box(new Rect (scaledResolutionWidth * 0.5f - (scaledResolutionWidth / 10), nativeVerticalResolution * 0.5f - (nativeVerticalResolution / 20), scaledResolutionWidth / 5,  nativeVerticalResolution / 10), 
                                "GAME PAUSED");
        }
        GUI.color = new Color(1, 1, 1, hitAlpha);
        GUI.DrawTexture(new Rect(0, 0, scaledResolutionWidth, nativeVerticalResolution), screenFlash);
        color.a = timerAlpha;
        GUI.color = color;
        GUI.Label(new Rect (scaledResolutionWidth / 2 - (scaledResolutionWidth / 10f), nativeVerticalResolution / 2 - (nativeVerticalResolution / 2f) + 20, scaledResolutionWidth / 5,
                            nativeVerticalResolution / 10), string.Format("{0:00}:{1:00}", minutes, seconds));
		ShowGestures ();
        if (GUI.Button(new Rect (scaledResolutionWidth - (scaledResolutionWidth / 10) - 20, 20, scaledResolutionWidth / 10, nativeVerticalResolution / 20), 
                                    "TOGGLE PAUSE"))
		{
            StateManager.Instance.TogglePause();
			//goto main menu
		}
	}

	private void ShowGestures()
	{
		color.a = 1.0f;
		GUI.color = color;
        GUI.DrawTexture(new Rect(scaledResolutionWidth - logo.width - 15f, nativeVerticalResolution - logo.height - 15f, logo.width, logo.height), logo);
		GUILayout.Box ("Score: " + stats.Score);
		GUILayout.Box("Distance: " + cart.Distance() + " meters");
        GUILayout.Box("Velocity: " + Mathf.Floor(movement.Velocity()) + " m/s");
	}

    private void TimedScreenResize()
    {
        if (Time.time > updateGUI)
        {
            scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
        }
    }

    internal void DamageFlash()
    {
        if (fadingIn)
        {
            hitAlpha = Mathf.Lerp(hitAlpha, 1, 1f);
            if (hitAlpha == 1)
            {
                fadingIn = false;
            }
        }
        else
        {
            hitAlpha = Mathf.Lerp(hitAlpha, 0, 0.1f);
            if (hitAlpha < 0.01f)
            {
                fadingIn = true;
            }
        }
    }
}

