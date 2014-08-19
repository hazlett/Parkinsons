using UnityEngine;
using System.Collections;
using OhioState.Libraries.Gesture;

public class TouchBottomCenter : IGestureAction {

	CubeGridCollider collider;
	public TouchBottomCenter(CubeGridCollider collider)
	{
		this.collider = collider;
	}
	public void Trigger(object data)
	{
		collider.DebugCheckCollision ();
	}
}
