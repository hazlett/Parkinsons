﻿using UnityEngine;
using System.Collections;
using OhioState.Libraries.Gesture;
using OhioState.Kinect;
using System.Collections.Generic;
using System.Threading;
using System;

public class TutorialManager : MonoBehaviour {
	
	public GestureManager gestureManager;
	public KinectGameData currentKinectGameData;
	public SitStandTutor tutor;
	private KinectSensor GameKinectSensor;

	private IGestureAction stand, sit;
	// Use this for initialization
	
	void Awake () {
		stand = new StandTutorial (tutor);
		sit = new SitTutorial (tutor);
		
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
		
		DontDestroyOnLoad(this.gameObject);
		int size = 5 * 60;
		gestureManager = new GestureManager(size, true);
		
		gestureManager.Level = 1;
		gestureManager.ReadFiles();
		//if (gestureManager.ErrorMessage.Length > 0)
		//    Debug.LogError(gestureManager.ErrorMessage);
		SetGesturesForSection("tutorial");
		Debug.Log("Kinect Complete");
		
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
	private IGestureAction CreateGestureAction(string name)
	{
		IGestureAction rv = null;
		
		if (name == "standTutorial")
		{
			rv = new StandTutorial(tutor);
		}
		if (name == "sitTutorial")
		{
			rv = new SitTutorial(tutor);
		}
		
		return rv;
	}

	void Update()
	{
		//debugging only
		if (Input.GetKeyUp(KeyCode.W))
		{
			stand.Trigger(null);
		}
		if (Input.GetKeyUp(KeyCode.S))
		{
			sit.Trigger(null);
		}
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
