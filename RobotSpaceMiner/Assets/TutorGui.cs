using UnityEngine;
using System.Collections;

public class TutorGui : MonoBehaviour {

	public SitStandTutor tutor;
	public GUISkin skin;
	private float nativeVerticalResolution = 1080.0f, scaledResolutionWidth, updateGUI = 0.5f;

	void OnGUI()
	{
		GUI.skin = skin;
		GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(Screen.height / nativeVerticalResolution, Screen.height / nativeVerticalResolution, 1)); 

		GUI.Box (new Rect (scaledResolutionWidth * 0.2f - (scaledResolutionWidth / 10f), nativeVerticalResolution / 2 - (nativeVerticalResolution / 2f) + 20, scaledResolutionWidth / 4,
		                  nativeVerticalResolution / 5), tutor.GetMessage ());

		GUI.Box (new Rect (scaledResolutionWidth * 0.8f - (scaledResolutionWidth / 5f), nativeVerticalResolution / 2 - (nativeVerticalResolution / 2f) + 20, scaledResolutionWidth / 4,
		                   nativeVerticalResolution / 10), "(This is you)");

		if (tutor.CanContinue)
		{
			if (GUI.Button(new Rect (scaledResolutionWidth * 0.5f - (scaledResolutionWidth / 10f), nativeVerticalResolution * 0.5f, scaledResolutionWidth / 4,
			                     nativeVerticalResolution / 5), "PRESS TO CONTINUE")) 
			{
				Application.LoadLevel("MainLevel");
			}
		}

	}

	void Update()
	{

		TimedScreenResize ();
	}

	private void TimedScreenResize()
	{
		if (Time.time > updateGUI)
		{
			scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
		}
	}
}
