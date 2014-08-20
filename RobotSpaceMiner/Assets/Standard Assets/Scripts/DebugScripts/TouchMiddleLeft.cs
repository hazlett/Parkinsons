using UnityEngine;
using System.Collections;
using OhioState.Libraries.Gesture;
using System.Collections.Generic;

class TouchMiddleLeft : IGestureAction
{
	CubeGridCollider collider;
	public TouchMiddleLeft(CubeGridCollider collider)
	{
		this.collider = collider;
	}
	public void Trigger(object data)
	{
		collider.DebugCheckCollision ();
	}
}

