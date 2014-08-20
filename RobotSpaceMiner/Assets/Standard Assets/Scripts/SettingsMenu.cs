using UnityEngine;
using System.Collections;
using System;

public class SettingsMenu : MonoBehaviour {
    public GUISkin skin;
    public Font font;

    public string age = "AGE";
    private bool ageCleared;
    public string timer = "TIMER";
    private bool timerCleared;
    public GameObject mainMenu;
    private int gender = 0;
    private XmlSettings settings;

	void Start () {
		settings = SettingsSerializer.Instance.ReadSettings ();
        ageCleared = false;
        timerCleared = false;
		age = PlayerSettings.Instance.Age.ToString();
		timer = PlayerSettings.Instance.Timer.ToString();
	}
	
    void OnGUI()
    {
        GUI.skin = skin;
        GUI.skin.button.font = font;
        GUI.skin.textField.font = font;
        GUI.SetNextControlName("age");
        age = GUILayout.TextField(age, 5);
        if (GUI.GetNameOfFocusedControl() == "age")
        {
            if (!ageCleared)
            {
                age = "";
                ageCleared = true;
            }
        }
        GUI.SetNextControlName("timer");
        timer = GUILayout.TextField(timer, 10);
        if (GUI.GetNameOfFocusedControl() == "timer")
        {
            if (!timerCleared)
            {
                timer = "";
                timerCleared = true;
            }
        }

        if (GUILayout.Button("Male"))
        {
            gender = 0;

        }
        if (GUILayout.Button("Female"))
        {
            gender = 1;
        }
        GUILayout.Space(50.0f);
        if (GUILayout.Button("Apply"))
        {
            Apply();
        }
        if (GUILayout.Button("BACK"))
        {
            mainMenu.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    private void Apply()
    {
        try
        {
            settings = new XmlSettings(int.Parse(age), int.Parse(timer), gender);
			PlayerSettings.Instance.SetSettings(settings);
        }
        catch (Exception e) {
			Debug.LogError(e.Message);
		}
    }
}
