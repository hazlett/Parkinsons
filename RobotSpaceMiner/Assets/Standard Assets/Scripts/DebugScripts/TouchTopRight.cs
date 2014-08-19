using UnityEngine;
using System.Collections;
using OhioState.Libraries.Gesture;

public class TouchTopRight : IGestureAction {
	CubeGridCollider collider;
	public TouchTopRight(CubeGridCollider collider)
	{
		this.collider = collider;
	}
	public void Trigger(object data)
	{
		collider.DebugCheckCollision ();
	}
}
