using UnityEngine;
using System.Collections;
using OhioState.Libraries.Gesture;

public class MoveTrackRight : IGestureAction {

	private BasicMovement cart;
	public MoveTrackRight(BasicMovement cart)
	{
		this.cart = cart;
	}
	public void Trigger(object data)
	{
		cart.TriggerHopRight ();
	}
}
