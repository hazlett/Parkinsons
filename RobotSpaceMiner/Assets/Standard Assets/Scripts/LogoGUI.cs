using UnityEngine;
using System.Collections;

public class LogoGUI : MonoBehaviour {
    public Texture2D logo;
    private float nativeVerticalResolution = 1080.0f, scaledResolutionWidth, updateGUI = 0.5f;

    void Update()
    {
        TimedScreenResize();
    }
    void OnGUI()
    {
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(Screen.height / nativeVerticalResolution, Screen.height / nativeVerticalResolution, 1)); 
		
        GUI.DrawTexture(new Rect((scaledResolutionWidth - (3 * logo.width) - 15f), (nativeVerticalResolution - (3 * logo.height) - 15f), 3 * logo.width, 3 * logo.height), logo);
    }
    private void TimedScreenResize()
    {
        if (Time.time > updateGUI)
        {
            scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
        }
    }
}
