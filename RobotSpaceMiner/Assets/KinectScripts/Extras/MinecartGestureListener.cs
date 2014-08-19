using UnityEngine;
using Windows.Kinect;
using System.Collections;
using System;


public class MinecartGestureListener : MonoBehaviour, KinectGestures.GestureListenerInterface
{
	// GUI Text to display the gesture messages.
	public GUIText GestureInfo;
	
	// private bool to track if progress message has been displayed
	private bool progressDisplayed;
    public GUIScript mainGUI;
	
	
	public void UserDetected(long userId, int userIndex)
	{
		// as an example - detect these user specific gestures
		KinectManager manager = KinectManager.Instance;

        manager.DetectGesture(userId, KinectGestures.Gestures.StandUp);
        manager.DetectGesture(userId, KinectGestures.Gestures.Squat);

		StateManager.Instance.StartTimer ();

		if(GestureInfo != null)
		{
			GestureInfo.guiText.text = "Sit and stand to move the cart!";
		}
	}
	
	public void UserLost(long userId, int userIndex)
	{

		if(GestureInfo != null)
		{
			GestureInfo.guiText.text = ("User not found.  Raise hand to start again.");
		}
	
	}

	public void GestureInProgress(long userId, int userIndex, KinectGestures.Gestures gesture, 
		float progress, JointType joint, Vector3 screenPos)
	{
		//GestureInfo.guiText.text = string.Format("{0} Progress: {1:F1}%", gesture, (progress * 100));
//		if(gesture == KinectGestures.Gestures.Click && progress > 0.3f)
//		{
//			string sGestureText = string.Format ("{0} {1:F1}% complete", gesture, progress * 100);
//			if(GestureInfo != null)
//				GestureInfo.guiText.text = sGestureText;
//			
//			progressDisplayed = true;
//		}		
//		else 
		if((gesture == KinectGestures.Gestures.ZoomOut || gesture == KinectGestures.Gestures.ZoomIn) && progress > 0.5f)
		{
			string sGestureText = string.Format ("{0} detected, zoom={1:F1}%", gesture, screenPos.z * 100);
			if(GestureInfo != null)
				GestureInfo.guiText.text = sGestureText;
			
			progressDisplayed = true;
		}
		else if(gesture == KinectGestures.Gestures.Wheel && progress > 0.5f)
		{
			string sGestureText = string.Format ("{0} detected, angle={1:F1} deg", gesture, screenPos.z);
			if(GestureInfo != null)
				GestureInfo.guiText.text = sGestureText;
			
			progressDisplayed = true;
		}
	}

	public bool GestureCompleted (long userId, int userIndex, KinectGestures.Gestures gesture, 
	    JointType joint, Vector3 screenPos)
	{
		
//		if(gesture == KinectGestures.Gestures.Click)
//			sGestureText += string.Format(" at ({0:F1}, {1:F1})", screenPos.x, screenPos.y);
        if (StateManager.Instance.CurrentState == StateManager.State.PLAYING)
        {
            string sGestureText = gesture + " detected";

            if (GestureInfo != null)
                GestureInfo.guiText.text = sGestureText;
        }
		
		
		
		progressDisplayed = false;
		
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
	
}
