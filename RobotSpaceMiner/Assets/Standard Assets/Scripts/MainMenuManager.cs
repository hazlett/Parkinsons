using UnityEngine;
using System.Collections;

public class MainMenuManager : MonoBehaviour {
    public Texture2D backgroundImage;
    public GUISkin skin;
    public GameObject settings;
	private float nativeVerticalResolution = 1080.0f, scaledResolutionWidth, updateGUI = 0.5f;
	// Use this for initialization
	void Start () {
        StateManager.Instance.CurrentState = StateManager.State.MENU;
	}
	
	void Update()
	{
        scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;

	}

    void OnGUI()
    {
        GUI.skin = skin;
        
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(Screen.height / nativeVerticalResolution, Screen.height / nativeVerticalResolution, 1));
        GUI.DrawTexture(new Rect(0, 0, scaledResolutionWidth, nativeVerticalResolution), backgroundImage);
 
        if (GUI.Button(new Rect(
            scaledResolutionWidth * 0.5f - (GUI.skin.button.fixedWidth / 2),
            nativeVerticalResolution * 0.3f,
            skin.button.fixedWidth,
            nativeVerticalResolution / 10),
            "START GAME"))
        {
            StateManager.Instance.CurrentState = StateManager.State.PLAYING;
            Application.LoadLevel("MainLevel");
        }
        if (GUI.Button(new Rect(
            scaledResolutionWidth * 0.5f - (GUI.skin.button.fixedWidth / 2),
            nativeVerticalResolution * 0.45f,
            skin.button.fixedWidth,
            nativeVerticalResolution / 10),
            "SETTINGS"))
        {
            settings.SetActive(true);
            this.gameObject.SetActive(false);
        }
        
    }
}
