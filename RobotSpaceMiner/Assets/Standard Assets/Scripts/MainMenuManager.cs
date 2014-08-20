using UnityEngine;
using System.Collections;

public class MainMenuManager : MonoBehaviour {

    public GUISkin skin;
    public Font font;
    public GameObject settings;
	// Use this for initialization
	void Start () {
        StateManager.Instance.CurrentState = StateManager.State.MENU;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnGUI()
    {
        GUI.skin = skin;
        GUI.skin.button.font = font;
        GUI.skin.textField.font = font;
        if (GUILayout.Button("START GAME"))
        {
            StateManager.Instance.CurrentState = StateManager.State.PLAYING;
            Application.LoadLevel("PerspectiveCamera");
        }
        if (GUILayout.Button("SETTINGS"))
        {
            settings.SetActive(true);
            this.gameObject.SetActive(false);
        }

    }
}
