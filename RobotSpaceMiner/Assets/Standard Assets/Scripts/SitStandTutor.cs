using UnityEngine;
using System.Collections;

public class SitStandTutor : MonoBehaviour {

	public GameObject hip, rightKnee, leftKnee, position;
	public Transform hipSit, hipStand, rightKneeSit, rightKneeStand, leftKneeSit, leftKneeStand, sitPosition, standPosition, rightAnkle, leftAnkle;
	private float speed = 1.0f;
	private Transform leftAnkleConstant, rightAnkleConstant;

	private int stateChanges;
	public bool CanContinue { get { return (stateChanges > 5); } }
	public int state;
	public enum TutorStates {
		Idle,
		Sit,
		Stand
	};
	public void Start()
	{
		rightAnkleConstant = rightAnkle;
		leftAnkleConstant = leftAnkle;
	}

	// Update is called once per frame
	void Update () {
		switch (state){ 
		case (int)TutorStates.Stand:
			Stand ();
			break;
		case (int)TutorStates.Sit:
			Sit();
			break;
		default:
			Sit ();
			break;
		}
	}
	

	public string GetMessage()
	{
		if (CanContinue)
		{
			return ("TUTOR:\nPress the button\nto continue!");
		}
		switch (state){ 
		case (int)TutorStates.Stand:
			return ("TUTOR:\nWill you please\nstand with me!");
			break;
		case (int)TutorStates.Sit:
			return ("TUTOR:\nWill you please\nsit with me!");
			break;
		default:
			return ("TUTOR:\nWill you please\nsit with me!");
			break;
		}
	}

	public void SetState(int state)
	{
		this.state = state;
		stateChanges++;
	}

	public void Continue()
	{
		if (CanContinue)
			Application.LoadLevel ("MainLevel");
	}
	private void Sit()
	{
		Quaternion hipRotation = Quaternion.Slerp (hip.transform.localRotation, hipSit.localRotation, Time.deltaTime * speed);
		Quaternion rightKneeRotation = Quaternion.Slerp (rightKnee.transform.localRotation, rightKneeSit.localRotation, Time.deltaTime * speed);
		Quaternion leftKneeRotation = Quaternion.Slerp (leftKnee.transform.localRotation, leftKneeSit.localRotation, Time.deltaTime * speed);
		Vector3 pos = Vector3.Slerp (position.transform.localPosition, sitPosition.localPosition, Time.deltaTime * speed * 0.5f);
		hip.transform.localRotation = hipRotation;
		rightKnee.transform.localRotation = rightKneeRotation;
		leftKnee.transform.localRotation = leftKneeRotation;
		position.transform.localPosition = pos;
		leftAnkle = leftAnkleConstant;
		rightAnkle = rightAnkleConstant;
	}
	private void Stand()
	{
		Quaternion hipRotation = Quaternion.Slerp (hip.transform.localRotation, hipStand.localRotation, Time.deltaTime * speed);
		Quaternion rightKneeRotation = Quaternion.Slerp (rightKnee.transform.localRotation, rightKneeStand.localRotation, Time.deltaTime * speed);
		Quaternion leftKneeRotation = Quaternion.Slerp (leftKnee.transform.localRotation, leftKneeStand.localRotation, Time.deltaTime * speed);
		Vector3 pos = Vector3.Slerp (position.transform.position, standPosition.position, Time.deltaTime * speed );
		hip.transform.localRotation = hipRotation;
		rightKnee.transform.localRotation = rightKneeRotation;
		leftKnee.transform.localRotation = leftKneeRotation;
		position.transform.position = pos;
		leftAnkle = leftAnkleConstant;
		rightAnkle = rightAnkleConstant;
	}
}
