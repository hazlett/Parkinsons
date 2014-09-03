using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour {

    public Texture2D logo;
    private float timer;
    private float nativeVerticalResolution = 1080.0f, scaledResolutionWidth, updateGUI = 0.5f;

    void Start()
    {
        timer = 0.0f;
    }

    void Update()
    {
        timer += Time.deltaTime;
        TimedScreenResize();
        if (timer > 3.0)
        {
            Application.LoadLevel("MenuAutoBackground");
        }
    }
    void OnGUI()
    {
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(Screen.height / nativeVerticalResolution, Screen.height / nativeVerticalResolution, 1));

        GUI.DrawTexture(new Rect(scaledResolutionWidth * 0.5f - 2.5f * logo.width, nativeVerticalResolution * 0.5f - 2.5f * logo.height, 5 * logo.width, 5 * logo.height), logo);
    }
    private void TimedScreenResize()
    {
        scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
    }
}
