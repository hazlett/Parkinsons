using UnityEngine;
using System.Collections;
using OhioState.Libraries.Gesture;

public class AutoRun : IGestureAction {

	private BasicMovement cart;
	public AutoRun(BasicMovement cart)
	{
		this.cart = cart;
	}
	public void Trigger(object data)
	{
		cart.autoRunState.enabled = !cart.autoRunState.enabled;
		StateManager.Instance.AutoRun = cart.autoRunState.enabled;
	}
}
