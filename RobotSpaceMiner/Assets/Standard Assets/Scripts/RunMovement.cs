using UnityEngine;
using System.Collections;
using OhioState.Libraries.Gesture;
using OhioState.Kinect;
using System.Collections.Generic;
using System.Threading;
using System;

public class RunMovement : IGestureAction
{

    private BasicMovement cart;

    public RunMovement(BasicMovement cart)
    {
        this.cart = cart;
    }

    public void Trigger(object data)
    {
        cart.Move();
    }
}
