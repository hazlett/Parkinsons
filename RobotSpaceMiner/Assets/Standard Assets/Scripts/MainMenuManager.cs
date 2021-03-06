﻿using UnityEngine;
using System.Collections;

public class MainMenuManager : MonoBehaviour {

	public AudioClip buttonClick;
	public SoundFXManager soundFXManager;
    public GUISkin skin;
    public GameObject settings;
	private float nativeVerticalResolution = 1080.0f, scaledResolutionWidth, updateGUI = 0.5f;
    private XmlSettings playerSettings;

    void Start () {
        StateManager.Instance.CurrentState = StateManager.State.MENU;
		StateManager.Instance.IsPlaying = false;
	}
	
	void Update()
	{
        scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;

	}

    void OnGUI()
    {
        
        GUI.skin = skin;
        
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(Screen.height / nativeVerticalResolution, Screen.height / nativeVerticalResolution, 1));

        GUI.Label(new Rect(
            scaledResolutionWidth * 0.5f - (GUI.skin.label.fixedWidth / 2),
            nativeVerticalResolution * 0.60f,
            skin.label.fixedWidth,
            nativeVerticalResolution / 5),
            ("AGE: " + PlayerSettings.Instance.Age +
            "\nGENDER: " + PlayerSettings.Instance.GetSettings().GetGenderString() + 
            "\nTIMER: " + PlayerSettings.Instance.Timer));

        if (GUI.Button(new Rect(
            scaledResolutionWidth * 0.5f - (GUI.skin.button.fixedWidth / 2),
            nativeVerticalResolution * 0.25f,
            skin.button.fixedWidth,
            nativeVerticalResolution / 10),
            "START GAME"))
        {
			PlayFX();
			StateManager.Instance.IsPlaying = true;
            StateManager.Instance.CurrentState = StateManager.State.PLAYING;
            Application.LoadLevel("MainLevel");
        }

		if (GUI.Button(new Rect(
			scaledResolutionWidth * 0.5f - (GUI.skin.button.fixedWidth / 2),
			nativeVerticalResolution * 0.36f,
			skin.button.fixedWidth,
			nativeVerticalResolution / 10),
		    "TUTORIAL"))
		{
			PlayFX();
			Application.LoadLevel("Tutorial");
		}

        if (GUI.Button(new Rect(
            scaledResolutionWidth * 0.5f - (GUI.skin.button.fixedWidth / 2),
            nativeVerticalResolution * 0.47f,
            skin.button.fixedWidth,
            nativeVerticalResolution / 10),
            "SETTINGS"))
        {
			PlayFX();
            settings.SetActive(true);
            this.gameObject.SetActive(false);
        }
        
    }

	private void PlayFX()
 	{
		soundFXManager.PlayClip (buttonClick);
	}
}
