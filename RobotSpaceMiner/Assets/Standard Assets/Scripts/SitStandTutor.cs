using UnityEngine;
using System.Collections;

public class SitStandTutor : MonoBehaviour {

	public GameObject hip, rightKnee, leftKnee, position, rightThigh, leftThigh, rightArm;
	public Transform hipSit, hipStand, rightThighSit, rightKneeSit, rightKneeStand, leftThighSit, leftKneeSit, leftKneeStand, sitPosition, standPosition, rightAnkle, leftAnkle;
	private float runAngle = 30.0f;
	private float runTime = 1.0f, armTime = 2.0f;
	private Transform leftAnkleConstant, rightAnkleConstant;
	private int redos, successes;
    private float timer;

	private int stateChanges;
	private bool CanContinue { get { return (stateChanges > 5); } }
	public int state;
	public enum TutorStates {
		Intro,
		Idle,
		TransitionToSit,
		Sitting,
		SittingRedo,
		TransitionToStand,
		Standing,
		StandingRedo,
        SittingRun,
		TransitionToSittingRun,
		ContinueSittingRun,
		TransitionToContinue,
		Continue
	};
	public void Start()
	{
		rightAnkleConstant = rightAnkle;
		leftAnkleConstant = leftAnkle;
		SnapToStand ();
	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		switch (state){ 
		case (int)TutorStates.Intro:
			Intro();
			break;
	    case (int)TutorStates.Standing:
		    Stand ();
		    break;
	    case (int)TutorStates.Sitting:
		    Sit();
		    break;
		case (int)TutorStates.ContinueSittingRun:
			if (timer > 3.0f)
			{
				ChangeState((int)TutorStates.SittingRun);
				
			}
			if (successes > 8)
			{
				ChangeState((int)TutorStates.TransitionToContinue);
			}
			SittingRun();
			break;
	    case (int)TutorStates.SittingRun:
	        SittingRun();
			break;
		case (int)TutorStates.SittingRedo:
			Stand (3.0f);
			if (timer > 4.0f)
			{
				ChangeState((int)TutorStates.Sitting);
				timer = 0;
			}
			break;
		case (int)TutorStates.StandingRedo:
			Sit (3.0f);
			if (timer > 4.0f)
			{
				ChangeState((int)TutorStates.Standing);
				timer = 0;
			}
			break;
		case (int)TutorStates.TransitionToSit:
			if (successes >= 4){
				ChangeState((int)TutorStates.TransitionToSittingRun);
				return;
			}
			Stand (3.0f);
			if (timer > 3.0f)
			{
				ChangeState((int)TutorStates.Sitting);
				timer = 0;
			}
			break;
		case (int)TutorStates.TransitionToStand:
			if (successes >= 4){
				ChangeState((int)TutorStates.TransitionToSittingRun);
				return;
			}
			Sit(3.0f);
			if (timer > 3.0f)
			{
				ChangeState((int)TutorStates.Standing);
				timer = 0;
			}
			break;
		case (int)TutorStates.TransitionToSittingRun:
			if (successes >= 8){
				ChangeState((int)TutorStates.TransitionToSittingRun);
				return;
			}
			if (timer < 3.0f - runTime)
			{
				Sit (3.0f);
			}
			else{
				InitialSittingRun();
			}
			if (timer > 3.0f)
			{
				ChangeState((int)TutorStates.SittingRun);
			}
			break;
		case (int)TutorStates.TransitionToContinue:
			Stand (3.0f);
			if (timer > 1.0f)
			{
				ChangeState((int)TutorStates.Continue);
			}
			break;
		case (int)TutorStates.Continue:
			RaiseLowerHand();
			break;
		default:
			break;
		}
	}

	private void Intro()
	{	
		if (timer > 5)
		{
			timer = 0;
			ChangeState((int)TutorStates.Sitting);
		}
	}
	public string GetMessage()
	{
		if (CanContinue)
		{
			return ("TUTOR:\nRaise your hand\nto continue!");
		}
		switch (state){ 
		case (int)TutorStates.Intro:
			return ("TUTOR:\nI will show you\na gesture and\nyou need to mimic me");
	    case (int)TutorStates.Standing:
		    return ("TUTOR:\nWill you please\nstand with me!");
	    case (int)TutorStates.Sitting:
		    return ("TUTOR:\nWill you please\nsit with me!");
	    case (int)TutorStates.SittingRun:
	        return ("TUTOR:\nWill you please\nrun with me\nwhilst sitting!");
		case (int)TutorStates.ContinueSittingRun:
			return ("TUTOR:\nGreat job!\nLet's keep running!");
		case (int)TutorStates.SittingRedo:
			return("TUTOR:\nLet's try again.\nRemember to sit\nfrom stand!");
		case (int)TutorStates.StandingRedo:
			return("TUTOR:\nLet's try again.\nRemember to stand\nfrom sit!");
		case (int)TutorStates.TransitionToSit:
			return("TUTOR:\nGreat job!\nWe are going to try\nsitting next!");
		case (int)TutorStates.TransitionToStand:
			return("TUTOR:\nGreat job!\nWe are going to try\nstanding next!");
		case (int)TutorStates.TransitionToContinue:
		case (int)TutorStates.Continue:
			return("TUTOR:\nRaise your hand\nto continue");
		case (int)TutorStates.TransitionToSittingRun:
			return("TUTOR:\nGreat job!\nWe are going to try\nrunning whilst sitting!");
	    default:
		    return ("TUTOR:\nI'm confused!");
		}
	}

	public void SitTriggered()
	{
		if (state == (int)TutorStates.Sitting)
		{
			successes++;
			ChangeState((int)TutorStates.TransitionToStand);
		}
		else if (state == (int)TutorStates.SittingRedo)
		{
			successes++;
			ChangeState((int)TutorStates.TransitionToStand);
		} 
	}
	public void StandTriggered()
	{
		if (state == (int)TutorStates.Standing)
		{
			successes++;
			ChangeState((int)TutorStates.TransitionToSit);
		}
		else if (state == (int)TutorStates.StandingRedo)
		{
			successes++;
			ChangeState((int)TutorStates.TransitionToSit);
		} 
	}
	public void SittingRunTriggered()
	{
		if (state == (int)TutorStates.SittingRun)
		{
			successes++;
			ChangeState((int)TutorStates.ContinueSittingRun, timer);
			
		}
		else if (state == (int)TutorStates.ContinueSittingRun)
		{
			successes++;
			ChangeState((int)TutorStates.SittingRun, timer);
			
		} 
	}
	public void Continue()
	{
		if (state == (int)TutorStates.Continue)
			Application.LoadLevel ("SettingsMenu");
		else if (state == (int)TutorStates.TransitionToContinue)
			Application.LoadLevel ("SettingsMenu");
	}
	private void ChangeState(int state, float timer)
	{
		this.state = state;
		this.timer = timer;
		redos = 0;
	}
	private void ChangeState(int state)
	{
		timer = 0;
		redos = 0;
		this.state = state;
	}
	private void InitialSittingRun()
	{
		float rightZ = rightThigh.transform.localEulerAngles.z,
		leftZ = leftThigh.transform.localEulerAngles.z;
		rightZ = Mathf.Lerp(rightThigh.transform.localEulerAngles.z, rightThighSit.localEulerAngles.z - runAngle, Time.deltaTime);

	}
    private void SittingRun()
    {		
		float rightZ = rightThigh.transform.localEulerAngles.z,
			leftZ = leftThigh.transform.localEulerAngles.z;
		if (timer < runTime) //raise left leg, lower right leg
		{
			rightZ = Mathf.Lerp(rightThigh.transform.localEulerAngles.z, rightThighSit.localEulerAngles.z + runAngle, Time.deltaTime);
			leftZ = Mathf.Lerp (leftThigh.transform.localEulerAngles.z, leftThighSit.localEulerAngles.z + runAngle, Time.deltaTime);
		}
		else if (timer < 2 * runTime) //raise right leg, lower left leg
		{
			rightZ = Mathf.Lerp(rightThigh.transform.localEulerAngles.z, rightThighSit.localEulerAngles.z - runAngle, Time.deltaTime);
			leftZ = Mathf.Lerp (leftThigh.transform.localEulerAngles.z, leftThighSit.localEulerAngles.z - runAngle, Time.deltaTime);
		}
		else
		{
			timer = 0.0f;
		}
        rightThigh.transform.localEulerAngles = new Vector3(
            rightThigh.transform.localEulerAngles.x,
            rightThigh.transform.localEulerAngles.y,
            rightZ);
		leftThigh.transform.localEulerAngles = new Vector3(
			leftThigh.transform.localEulerAngles.x,
			leftThigh.transform.localEulerAngles.y,
			leftZ);
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

	private void Sit() { Sit (1.0f);}
	private void Sit(float speed)
	{
		if (timer > 5)
		{
			ChangeState((int)TutorStates.SittingRedo);
			timer = 0;
		}
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

	private void Stand() { Stand (1.0f); }
	private void Stand(float speed)
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
	private void RaiseLowerHand() { RaiseLowerHand (1.0f); }
	private void RaiseLowerHand(float speed)
	{
		float z = rightArm.transform.localEulerAngles.z;
		if (timer < armTime) 
		{
			z = Mathf.Lerp(rightArm.transform.localEulerAngles.z, rightArm.transform.localEulerAngles.z + runAngle, Time.deltaTime);
		}
		else if (timer < 2 * armTime) 
		{
			z = Mathf.Lerp(rightArm.transform.localEulerAngles.z, rightArm.transform.localEulerAngles.z - 2 * runAngle, Time.deltaTime);
		}
		else if (timer < 3 * armTime) 
		{
			z = Mathf.Lerp(rightArm.transform.localEulerAngles.z, rightArm.transform.localEulerAngles.z + 2 * runAngle, Time.deltaTime);
		}
		else
		{
			timer = armTime;
		}
		rightArm.transform.localEulerAngles = new Vector3 (
			rightArm.transform.localEulerAngles.x,
			rightArm.transform.localEulerAngles.y,
			z);
	}
}
