﻿using UnityEngine;
using System.Collections;
using OhioState.Libraries.Gesture;
using System.Collections.Generic;

public class KeyboardController : MonoBehaviour {

	public CubeGridCollider topRight, topCenter, topLeft, middleRight, middleCenter, middleLeft, bottomRight, bottomCenter, bottomLeft;
	private IDictionary<KeyCode, IGestureAction> keyMapping;

	void Start () {
		keyMapping = new Dictionary<KeyCode, IGestureAction> ();
		keyMapping.Add (KeyCode.Keypad1, new TouchBottomLeft (bottomLeft));
		keyMapping.Add (KeyCode.Keypad2, new TouchBottomCenter (bottomCenter));
		keyMapping.Add (KeyCode.Keypad3, new TouchBottomRight (bottomRight));
		keyMapping.Add (KeyCode.Keypad4, new TouchMiddleLeft (middleLeft));
		keyMapping.Add (KeyCode.Keypad5, new TouchMiddleCenter (middleCenter));
		keyMapping.Add (KeyCode.Keypad6, new TouchMiddleRight (middleRight));
		keyMapping.Add (KeyCode.Keypad7, new TouchTopLeft (topLeft));
		keyMapping.Add (KeyCode.Keypad8, new TouchTopLeft (topCenter));
		keyMapping.Add (KeyCode.Keypad9, new TouchTopLeft (topRight));

	}

	void Update () {

		foreach (KeyCode code in keyMapping.Keys)
		{
			if (Input.GetKeyDown(code))
			{
				keyMapping[code].Trigger(null);
			}
		}
		
	}
}
