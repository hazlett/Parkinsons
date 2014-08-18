using UnityEngine;
using System.Collections;

public class IgnoreRobot : MonoBehaviour {

    private GameObject robot, leftHand, rightHand;

	void Start () {
        robot = GameObject.Find("Robot2");
        leftHand = GameObject.Find("Left_Wrist_Joint_01");
        rightHand = GameObject.Find("Right_Wrist_Joint_01");
	}
	
	// Update is called once per frame
	void Update () {

        Physics.IgnoreCollision(this.collider, robot.collider);
        Physics.IgnoreCollision(this.collider, leftHand.collider);
        Physics.IgnoreCollision(this.collider, rightHand.collider);

	}
}
