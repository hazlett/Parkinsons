using UnityEngine;
using System.Collections;
using System;

public class SettingsMenu : MonoBehaviour {
    public GUISkin skin;
    public Texture2D background;
    private string message;
    private bool errorMessage;
    private string messageEnd;
    public string age = "";
    private bool ageCleared;
    public string timer = "";
    private bool timerCleared;
    public GameObject mainMenu;
	private bool male;
    private XmlSettings settings;
    private float genderButton = 150.0f;
    private float applyAndExitButton = 500.0f;
    private float ageHeight = 0.25f,
        timerHeight = 0.35f,
        genderHeight = 0.45f,
        applyHeight = 0.6f,
        exitHeight = 0.7f,
        genderWidth;

	private float nativeVerticalResolution = 1080.0f, scaledResolutionWidth, updateGUI = 0.5f;

	void Start () {
        genderWidth = skin.label.fixedWidth +
            (2 * skin.toggle.fixedWidth) +
            (2 * genderButton) + 
            15.0f;
		settings = SettingsSerializer.Instance.ReadSettings ();
		male = (PlayerSettings.Instance.Gender == (int)XmlSettings.Genders.Male);
        messageEnd = "\n-PLEASE TRY AGAIN-";
	}

    void OnEnable()
    {
        age = PlayerSettings.Instance.Age.ToString();
        timer = PlayerSettings.Instance.Timer.ToString();
        timerCleared = false;
        ageCleared = false;
        errorMessage = false;
        GUI.FocusControl(null);
    }

    void Update()
    {
        scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
    }

    void OnGUI()
    {
        GUI.skin = skin;
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(Screen.height / nativeVerticalResolution, Screen.height / nativeVerticalResolution, 1));
        GUI.DrawTexture(new Rect(0, 0, scaledResolutionWidth, nativeVerticalResolution), background);
 
		//age        
        GUI.Label(new Rect(
            scaledResolutionWidth * 0.5f - (skin.label.fixedWidth + (skin.textField.fixedWidth / 2)),
            nativeVerticalResolution * ageHeight,
            scaledResolutionWidth / 5,
            nativeVerticalResolution / 10),
            "YOUR AGE:");
        GUI.SetNextControlName("age");
        age = GUI.TextField(new Rect(
            scaledResolutionWidth * 0.5f - (skin.textField.fixedWidth / 2),
            nativeVerticalResolution * ageHeight,
            scaledResolutionWidth / 5,
            nativeVerticalResolution / 10),
            age, 5);
        if (GUI.GetNameOfFocusedControl() == "age")
        {
            if (!ageCleared)
            {
                age = "";
                ageCleared = true;
            }
        }
        else
            ageCleared = false;
		GUI.Label (new Rect(
            scaledResolutionWidth * 0.5f + (skin.textField.fixedWidth / 2),
            nativeVerticalResolution * ageHeight,
            scaledResolutionWidth / 5,
            nativeVerticalResolution / 10),
            "YEARS");

		//game time
		GUI.Label (new Rect(
            scaledResolutionWidth * 0.5f - (skin.label.fixedWidth + (skin.textField.fixedWidth / 2)),
            nativeVerticalResolution * timerHeight,
            scaledResolutionWidth / 5,
            nativeVerticalResolution / 10),
            "GAME TIME:");
        GUI.SetNextControlName("timer");
        timer = GUI.TextField(new Rect(
            scaledResolutionWidth * 0.5f - (skin.textField.fixedWidth / 2),
            nativeVerticalResolution * timerHeight,
            scaledResolutionWidth / 5,
            nativeVerticalResolution / 10),
            timer, 10);
        if (GUI.GetNameOfFocusedControl() == "timer")
        {
            if (!timerCleared)
            {
                timer = "";
                timerCleared = true;
            }
        }
        else
            timerCleared = false;
		GUI.Label (new Rect(
            scaledResolutionWidth * 0.5f + (skin.textField.fixedWidth / 2),
            nativeVerticalResolution * timerHeight,
            scaledResolutionWidth / 5,
            nativeVerticalResolution / 10),
            "SECONDS");
		

		//gender	
        GUI.skin.button.fixedWidth = genderButton;
		GUI.Label (new Rect(
            scaledResolutionWidth * 0.5f - (genderWidth / 2),
            nativeVerticalResolution * genderHeight,
            scaledResolutionWidth / 5,
            nativeVerticalResolution / 10),
            "YOUR GENDER:");

		if (GUI.Toggle (new Rect(
            scaledResolutionWidth * 0.5f - ((genderWidth / 2) - skin.label.fixedWidth),
            nativeVerticalResolution * genderHeight,
            skin.toggle.fixedWidth,
            nativeVerticalResolution / 20),
            male, ""))
			male = true;
        
		if (GUI.Button (new Rect(
            scaledResolutionWidth * 0.5f - 
            ((genderWidth / 2) - skin.label.fixedWidth - skin.toggle.fixedWidth),
            nativeVerticalResolution * genderHeight,
            genderButton,
            nativeVerticalResolution / 10),
            "MALE"))
			male = true;

		if (GUI.Toggle (new Rect(
            scaledResolutionWidth * 0.5f -
            ((genderWidth / 2) - skin.label.fixedWidth -
            skin.toggle.fixedWidth - skin.button.fixedWidth - 15.0f),
            nativeVerticalResolution * genderHeight,
            skin.toggle.fixedWidth,
            nativeVerticalResolution / 20),
            !male, ""))
			male = false;

		if (GUI.Button (new Rect(
            scaledResolutionWidth * 0.5f -
            ((genderWidth / 2) - skin.label.fixedWidth -
            skin.toggle.fixedWidth - skin.button.fixedWidth - skin.toggle.fixedWidth - 15.0f),
            nativeVerticalResolution * genderHeight,
            genderButton,
            nativeVerticalResolution / 10),
            "FEMALE"))
			male = false;

        //apply and exit
        GUI.skin.button.fixedWidth = applyAndExitButton;
        if (GUI.Button(new Rect(
            scaledResolutionWidth * 0.5f - (applyAndExitButton / 2),
            nativeVerticalResolution * applyHeight,
            applyAndExitButton,
            nativeVerticalResolution / 9.9f),
            "APPLY AND BACK"))
        {
            Apply();
        }
        if (GUI.Button(new Rect(
            scaledResolutionWidth * 0.5f - (applyAndExitButton / 2),
            nativeVerticalResolution * exitHeight,
            applyAndExitButton,
            nativeVerticalResolution / 9.9f),
            "BACK WITHOUT APPLYING"))
        {
            Back();
        }

        if (errorMessage)
        {
            GUI.Box(new Rect(
                scaledResolutionWidth * 0.5f - (skin.box.fixedWidth / 2),
                nativeVerticalResolution * 0.21f - (nativeVerticalResolution / 5),
                scaledResolutionWidth / 5,
                nativeVerticalResolution / 5),
                message + messageEnd);
        }
    }
	private void Back()
	{
		mainMenu.SetActive(true);
		this.gameObject.SetActive(false);
	}
    private void Apply()
    {
        int ageInt, timerInt;
        try
        {
            ageInt = int.Parse(age);
            if (ageInt <= 0)
            {
                message = "AGE MUST BE GREATER THAN 0";
                errorMessage = true;
                return;
            }
            else if (ageInt >= 150)
            {
                message = "AGE MUST BE LESS THAN 150";
                errorMessage = true;
                return;
            }
        }
        catch (Exception)
        {
            message = "AGE IS IN INCORRECT FORMAT\nPLEASE ENTER A WHOLE NUMBER";
            errorMessage = true;
            return;
        }
        try
        {
            timerInt = int.Parse(timer);
            if (timerInt <= 0)
            {
                message = "TIME MUST BE GREATER THAN 0 SECONDS";
                errorMessage = true;
                return;
            }
            else if (timerInt > 300)
            {
                message = "TIME MUST BE LESS THAN 300 SECONDS";
                errorMessage = true;
                return;
            }
        }
        catch (Exception)
        {
            message = "TIME IS IN INCORRECT FORMAT\nPLEASE ENTER A WHOLE NUMBER";
            errorMessage = true;
            return;
        }
      
        try
        {
            settings = new XmlSettings(ageInt, timerInt, Convert.ToInt32(!male));
			PlayerSettings.Instance.SetSettings(settings);
            Back();
        }
        catch (Exception e) {
			Debug.LogError(e.Message);
            message = "UNEXPECTED ERROR";
            messageEnd = "\nPLEASE CONTINUE WITHOUT SAVING";
            errorMessage = true;
		}
    }
}
