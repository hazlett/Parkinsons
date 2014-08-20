using UnityEngine;
using System.Collections;

public class CameraSwitch : MonoBehaviour {

    public Camera sideView, firstView;

    private enum cameraOn
    {
        side,
        chase,
        firstPerson
    }

    private cameraOn cameraActive;

	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.C))
        {
            cameraActive++;
            SwitchCameras();
        }

	}

    public void SwitchCameras()
    {

        switch (cameraActive)
        {
            case cameraOn.side: this.camera.enabled = false;
                firstView.camera.enabled = false;
                sideView.camera.enabled = true;
                break;
            case cameraOn.chase: firstView.camera.enabled = false;
                this.camera.enabled = true;
                sideView.camera.enabled = false;
                break;
            case cameraOn.firstPerson: firstView.camera.enabled = true;
                this.camera.enabled = false;
                sideView.camera.enabled = false;
                break;
            default: cameraActive = 0;
                break;
        }
    }
}
