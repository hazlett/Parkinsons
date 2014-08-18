using UnityEngine;
using System.Collections;
using OhioState.Libraries.Gesture;
using OhioState.Kinect;
using System.Collections.Generic;
using System.Threading;
using System;

class StandTutorial : IGestureAction
{
	private SitStandTutor tutor;
	public StandTutorial(SitStandTutor tutor)
	{
		this.tutor = tutor;
	}
	public void Trigger (object data)
	{
		tutor.SetState ((int)SitStandTutor.TutorStates.Sit);
		Debug.Log ("StandTrigger");
	}
}

