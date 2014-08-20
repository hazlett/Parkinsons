using UnityEngine;
using System.Collections;
using OhioState.Libraries.Gesture;
using OhioState.Kinect;
using System.Collections.Generic;
using System.Threading;
using System;

public class MoveCart : IGestureAction {

    private BasicMovement cart;

	public MoveCart(BasicMovement cart)
	{
		this.cart = cart;
	}

	public void Trigger (object data)
	{
        cart.Pumped();
	}
}
