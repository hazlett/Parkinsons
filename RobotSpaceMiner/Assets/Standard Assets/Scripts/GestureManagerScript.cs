﻿using UnityEngine;
using System.Collections;
using OhioState.Libraries.Gesture;
using OhioState.Kinect;
using System.Collections.Generic;
using System.Threading;
using System;

public class GestureManagerScript : MonoBehaviour {

    public GestureManager gestureManager;
    public BasicMovement move;
	public SitStandTutor tutor;
    public KinectGameData currentKinectGameData;
    public string sectionName;

    private KinectSensor GameKinectSensor;

	// Use this for initialization

	void Awake () {
            
        Debug.Log("Initializing Kinect");
        try
        {
            GameKinectSensor = new OhioState.Kinect.KinectSensor();
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }

        currentKinectGameData = new KinectGameData();

        try
        {
            GameKinectSensor.SkeletonHandler += kinectSensor_SkeletonFrameReady;
            GameKinectSensor.Initialize();
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }

        int size = 5 * 60;
        gestureManager = new GestureManager(size, true);
      
        gestureManager.Level = 1;
        gestureManager.ReadFiles();
        //if (gestureManager.ErrorMessage.Length > 0)
        //    Debug.LogError(gestureManager.ErrorMessage);
        SetGesturesForSection(sectionName);
		Debug.Log("Kinect: " + GameKinectSensor.Status);
		
	}

    public void SetGesturesForSection(string sectionName)
    {
        gestureManager.Clear();
        List<string> gameActions = gestureManager.GetActionsForSection(sectionName);
        foreach (string gameAction in gameActions)
        {
            IGestureAction action = CreateGestureAction(gameAction);

            if (action != null)
            {
                gestureManager.AttachAction(sectionName, gameAction, action);
            }
        }
    }

    void OnDestroy()
    {
        GameKinectSensor.Deactivate();
    }

    private IGestureAction CreateGestureAction(string name)
    {
       IGestureAction rv = null;

       if (sectionName == "demo")
       {
           if (name == "stand")
           {
               rv = new MoveCart(move);
           }
           if (name == "sit")
           {
               rv = new MoveCart(move);
           }
       }
       if (sectionName == "tutorial")
       {
           if (name == "standTutorial")
           {
               rv = new StandTutorial(tutor);
           }
           if (name == "sitTutorial")
           {
               rv = new SitTutorial(tutor);
           }
			if (name == "continueTutorial")
			{
				rv = new ContinueTutorial(tutor);
			}
       }
       
        return rv;
    }

	void FixedUpdate () {

        GameKinectSensor.Update();
        gestureManager.HandleNewSkeleton(currentKinectGameData, (double)Time.fixedTime * 1000);
	}

    void kinectSensor_SkeletonFrameReady(object sender, SkeletonHandlerArgs skeletonHandler)
    {
        currentKinectGameData.TrackedSkeleton = new Skeleton(skeletonHandler.GetSkeleton());
    }

}