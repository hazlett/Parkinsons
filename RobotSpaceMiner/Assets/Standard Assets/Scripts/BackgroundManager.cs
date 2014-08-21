using UnityEngine;
using System.Collections;

public class BackgroundManager : MonoBehaviour {

    public Texture2D backgroundImage;
	void Start () {
	
	}
	
	void Update () {
        
	}

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundImage);
    }
}
