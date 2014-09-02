using UnityEngine;
using System.Collections;
using OhioState.Libraries.Gesture;

public class QuitApplication : MonoBehaviour, IGestureAction {

    public void Trigger (object data)
    {
        Application.Quit();
    }
}
