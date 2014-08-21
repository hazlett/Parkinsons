﻿using UnityEngine;
using System.Collections;

public class CubeGridCollider : MonoBehaviour {

    private Color transparent = new Color(1, 1, 1, 0);
    private Color indicatorOn = new Color(0, 0.59f, 1, 0.4f);
    private int onCount;

	// Use this for initialization
	void Start () {

        this.renderer.material.color = transparent;
	}

    // If the cube collides with the hand...
    void OnTriggerEnter(Collider collider)
    {
		if (collider.tag == "Hand")
		{
			CheckCollision();
		}
    }

    // Turn off the cube indicator...
	private void CheckCollision()
	{
		if (this.renderer.material.color == indicatorOn)
		{
			Deactivate();
		}
	}
	internal void DebugCheckCollision()
	{
		CheckCollision ();
	}

    // Turn cube indicator on
    internal void Activate()
    {
        onCount++;
        indicatorOn.a +=  0.125f;
        this.renderer.material.color = indicatorOn;
    }

    // And shoot an iceball out of the cube
    void Deactivate()
    {
        onCount--;
        if (onCount >= 0)
        {
            indicatorOn.a -= 0.125f;
            this.renderer.material.color = indicatorOn;
        }

        if (onCount == 0)
        {
            this.renderer.material.color = transparent;
        }

        GameObject iceball;
        iceball = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Iceball"));
        iceball.transform.position = this.transform.position;
    }

    internal void DeactivateHit()
    {
        onCount--;
        if (onCount >= 0)
        {
            indicatorOn.a -= 0.125f;
            this.renderer.material.color = indicatorOn;
        }
        if (onCount == 0)
        {
            this.renderer.material.color = transparent;
        }
    }
}
