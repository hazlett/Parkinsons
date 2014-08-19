using UnityEngine;
using System.Collections;
using OhioState.Libraries.Gesture;

public class TouchTopLeft : IGestureAction {

	CubeGridCollider collider;
	public TouchTopLeft(CubeGridCollider collider)
	{
		this.collider = collider;
	}
	public void Trigger(object data)
	{
		collider.DebugCheckCollision ();
	}
}
