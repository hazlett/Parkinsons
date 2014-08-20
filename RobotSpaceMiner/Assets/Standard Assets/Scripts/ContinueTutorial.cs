using UnityEngine;
using System.Collections;
using OhioState.Libraries.Gesture;

public class ContinueTutorial : IGestureAction {

	private SitStandTutor tutor;
	public ContinueTutorial(SitStandTutor tutor)
	{
		this.tutor = tutor;
	}
	public void Trigger(object data)
	{
		tutor.Continue ();
	}

}
