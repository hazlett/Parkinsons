using UnityEngine;
using System.Collections;
using OhioState.Libraries.Gesture;

public class TouchBottomLeft : IGestureAction {
	CubeGridCollider collider;
	public TouchBottomLeft(CubeGridCollider collider)
	{
		this.collider = collider;
	}
	public void Trigger(object data)
	{
		collider.DebugCheckCollision ();
	}
}
