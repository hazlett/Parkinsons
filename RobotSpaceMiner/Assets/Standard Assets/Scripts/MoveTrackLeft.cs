using UnityEngine;
using System.Collections;
using OhioState.Libraries.Gesture;

public class MoveTrackLeft : IGestureAction {

	private BasicMovement cart;
	public MoveTrackLeft(BasicMovement cart)
	{
		this.cart = cart;
	}
	public void Trigger(object data)
	{
		cart.TriggerHopLeft ();
	}
}
