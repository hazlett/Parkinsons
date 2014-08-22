using UnityEngine;
using System.Collections;

public class SitStandTutor : MonoBehaviour {

	public GameObject hip, rightKnee, leftKnee, position, rightThigh, leftThigh;
	public Transform hipSit, hipStand, rightThighSit, rightKneeSit, rightKneeStand, leftThighSit, leftKneeSit, leftKneeStand, sitPosition, standPosition, rightAnkle, leftAnkle;
	private float speed = 1.0f;
	private Transform leftAnkleConstant, rightAnkleConstant;

    private float timer;

	private int stateChanges;
	public bool CanContinue { get { return (stateChanges > 5); } }
	public int state;
	public enum TutorStates {
		Idle,
		Sit,
		Stand,
        SittingRun
	};
	public void Start()
	{
		rightAnkleConstant = rightAnkle;
		leftAnkleConstant = leftAnkle;
        state = (int)TutorStates.SittingRun;
        SnapToSit();
	}

	// Update is called once per frame
	void Update () {
        if (CanContinue)
        {

        }
		switch (state){ 
		    case (int)TutorStates.Stand:
			    Stand ();
			    break;
		    case (int)TutorStates.Sit:
			    Sit();
			    break;
            case (int)TutorStates.SittingRun:
                SittingRun();
                break;
		    default:
			    Sit ();
			    break;
		}
	}

    public void RanInPlace()
    {
        if ((int)TutorStates.SittingRun == this.state)
        {
            stateChanges++;
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
		    case (int)TutorStates.Sit:
			    return ("TUTOR:\nWill you please\nsit with me!");
            case (int)TutorStates.SittingRun:
                return ("TUTOR:\nWill you please\nrun with me\nwhilst sitting!");
		    default:
			    return ("TUTOR:\nWill you please\nsit with me!");
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


    private void SittingRun()
    {
        float z = Mathf.Lerp(rightThigh.transform.localEulerAngles.z, rightThighSit.localEulerAngles.z - 20.0f, Time.deltaTime * speed);
        rightThigh.transform.localEulerAngles = new Vector3(
            rightThigh.transform.localEulerAngles.x,
            rightThigh.transform.localEulerAngles.y,
            z);

    }

    private void SnapToSit()
    {
        hip.transform.localRotation = hipSit.localRotation;
        rightKnee.transform.localRotation = rightKneeSit.localRotation;
        leftKnee.transform.localRotation = leftKneeSit.localRotation;
        position.transform.localPosition = sitPosition.localPosition;
        leftAnkle = leftAnkleConstant;
        rightAnkle = rightAnkleConstant;
    }
    private void SnapToStand()
    {
        hip.transform.localRotation = hipStand.localRotation;
        rightKnee.transform.localRotation = rightKneeStand.localRotation;
        leftKnee.transform.localRotation = leftKneeStand.localRotation;
        position.transform.localPosition = standPosition.localPosition;
        leftAnkle = leftAnkleConstant;
        rightAnkle = rightAnkleConstant;
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
