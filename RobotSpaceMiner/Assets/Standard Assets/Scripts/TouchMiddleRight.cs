using UnityEngine;
using System.Collections;
using OhioState.Libraries.Gesture;
using System.Collections.Generic;

class TouchMiddleRight : IGestureAction
{
	CubeGridCollider collider;
	public TouchMiddleRight(CubeGridCollider collider)
	{
		this.collider = collider;
	}
	public void Trigger(object data)
	{
		collider.DebugCheckCollision ();
	}
}

