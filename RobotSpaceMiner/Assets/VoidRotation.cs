﻿using UnityEngine;
using System.Collections;

public class VoidRotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.gameObject.transform.eulerAngles = Vector3.zero;
	}
}
