using UnityEngine;
using System.Collections;
using OhioState.Libraries.Gesture;
using System.Collections.Generic;

class TouchMiddleCenter : IGestureAction
{
	CubeGridCollider collider;
	public TouchMiddleCenter(CubeGridCollider collider)
	{
		this.collider = collider;
	}
	public void Trigger(object data)
	{
		collider.DebugCheckCollision ();
	}
}

