using UnityEngine;
using System.Collections;
using OhioState.Libraries.Gesture;
using OhioState.Kinect;
using System.Collections.Generic;
using System.Threading;
using System;

public class SitTutorial : IGestureAction
{
	private SitStandTutor tutor;
	public SitTutorial(SitStandTutor tutor)
	{
		this.tutor = tutor;
	}
	public void Trigger (object data)
	{
		tutor.SitTriggered ();
	}
}

