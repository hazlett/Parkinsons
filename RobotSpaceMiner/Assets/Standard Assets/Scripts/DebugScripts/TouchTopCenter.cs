using UnityEngine;
using System.Collections;
using OhioState.Libraries.Gesture;

public class TouchTopCenter : IGestureAction {

	CubeGridCollider collider;
	public TouchTopCenter(CubeGridCollider collider)
	{
		this.collider = collider;
	}
	public void Trigger(object data)
	{
		collider.DebugCheckCollision ();
	}
}
