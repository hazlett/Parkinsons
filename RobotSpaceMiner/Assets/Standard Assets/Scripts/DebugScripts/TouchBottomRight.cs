using UnityEngine;
using System.Collections;
using OhioState.Libraries.Gesture;

public class TouchBottomRight : IGestureAction {

	CubeGridCollider collider;
	public TouchBottomRight(CubeGridCollider collider)
	{
		this.collider = collider;
	}
	public void Trigger(object data)
	{
		collider.DebugCheckCollision ();
	}

}
