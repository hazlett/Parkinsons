using UnityEngine;
using Windows.Kinect;
using System.Collections;
using System;


public class SimpleGestureListener : MonoBehaviour, KinectGestures.GestureListenerInterface
{
	// GUI Text to display the gesture messages.
	public GUIText GestureInfo;
	
	// private bool to track if progress message has been displayed
	private bool progressDisplayed;
	
	
	public void UserDetected(long userId, int userIndex)
	{
		// as an example - detect these user specific gestures
		KinectManager manager = KinectManager.Instance;
		manager.DetectGesture(userId, KinectGestures.Gestures.RaiseRightHand);
	}
	
	public void UserLost(long userId, int userIndex)
	{
		if(GestureInfo != null)
		{
			GestureInfo.guiText.text = string.Empty;
		}
	}

	public bool GestureCompleted (long userId, int userIndex, KinectGestures.Gestures gesture, 
	    JointType joint, Vector3 screenPos)
	{
        if (gesture == KinectGestures.Gestures.RaiseRightHand)
        {
            StateManager.Instance.IsPlaying = true;
            StateManager.Instance.CurrentState = StateManager.State.PLAYING;
        }
		
		return true;
	}

	public bool GestureCancelled (long userId, int userIndex, KinectGestures.Gestures gesture, 
	    JointType joint)
	{
		if(progressDisplayed)
		{
			// clear the progress info
			if(GestureInfo != null)
				GestureInfo.guiText.text = String.Empty;
			
			progressDisplayed = false;
		}
		
		return true;
	}

    public void GestureInProgress(long userId, int userIndex, KinectGestures.Gestures gesture, float progress, JointType joint, Vector3 screenPos)
    {
    }
}
