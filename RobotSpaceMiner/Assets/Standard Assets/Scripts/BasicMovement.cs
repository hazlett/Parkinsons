using UnityEngine;
using System.Collections;
using System.IO;

public class BasicMovement : MonoBehaviour {

    public float maxVelocity; 
    public Camera mainCamera;
    public DistanceTraveled cartDistance;
    public ConstantForce downhill;
	public AutoRunState autoRunState;
    public WheelCollider wheel;
    public float minY;

    private Vector3 moveNormal;
    private float offsetX = -6f, offsetY = 6, offsetZ = -18f, forceOnCart;
    private Camera car;

    internal enum trackNumber
    {
        LEFT,
        CENTER,
        RIGHT
    }

    internal trackNumber currentTrack;

    void Start()
    {
        SetForceOnCart();
        downhill.force = new Vector3(100, 0, 0);
        downhill.enabled = false;
		autoRunState.Initialize (this);
        currentTrack = trackNumber.CENTER;

        car = GameObject.Find("First Person Camera").camera;
    }

    void Update()
    {
        DownhillCheck();

        CameraFollow();

        SetGround();

        if (wheel.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.rigidbody.AddForce(new Vector3(0, 11000, 0));
            }
        }

    }

	public void TriggerHopLeft()
	{
		HopLeft ();
	}
	public void TriggerHopRight()
	{
		HopRight ();
	}

    public void Move() {

        if (cartDistance.Distance() <= 400 || this.transform.rotation.x < -0.550001f)
        {
			if (StateManager.Instance.TimerPause)
			{
				StateManager.Instance.StartTimer();
			}
            if (rigidbody.velocity.x <= maxVelocity)
            {
                rigidbody.AddForce(new Vector3(forceOnCart, 0, 0));
            }
        }


        CheckVelocity();
        

    }

    void CheckVelocity()
    {
        // Cap the velocity
        if (rigidbody.velocity.x > maxVelocity)
        {
            rigidbody.velocity = new Vector3(maxVelocity, rigidbody.velocity.y, rigidbody.velocity.z);
        }
    }

    void CameraFollow() {

        mainCamera.transform.position = new Vector3(this.transform.position.x + offsetX, this.transform.position.y + offsetY, this.transform.position.z + offsetZ);
    }

    internal float Velocity()
    {
        return rigidbody.velocity.x;
    }

    void SetForceOnCart()
    {
        if (PlayerSettings.Instance.Age >= 65 && PlayerSettings.Instance.Age < 70)
        {
            forceOnCart = 10000;
        }
        else if (PlayerSettings.Instance.Age >= 70 && PlayerSettings.Instance.Age < 75)
        {
            forceOnCart = 11000;
        }
        else if (PlayerSettings.Instance.Age >= 75 && PlayerSettings.Instance.Age < 80)
        {
            forceOnCart = 12000;
        }
        else if (PlayerSettings.Instance.Age >= 80 && PlayerSettings.Instance.Age < 85)
        {
            forceOnCart = 13000;
        }
        else if (PlayerSettings.Instance.Age >= 85 && PlayerSettings.Instance.Age < 90)
        {
            forceOnCart = 14000;
        }
        else
        {
            forceOnCart = 10000;
        }
        
    }

    void HopLeft()
    {
        if (currentTrack != trackNumber.LEFT)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 10);
            if(currentTrack == trackNumber.CENTER) {
                currentTrack = trackNumber.LEFT;
            }
            else {
                currentTrack = trackNumber.CENTER;
            }
        }
    }

    void HopRight()
    {
        if (currentTrack != trackNumber.RIGHT)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 10);
            if (currentTrack == trackNumber.CENTER)
            {
                currentTrack = trackNumber.RIGHT;
            }
            else
            {
                currentTrack = trackNumber.CENTER;
            }
        }
    }

    void DownhillCheck()
    {

        if (cartDistance.Distance() >= 400 && this.transform.rotation.x > -0.550001f)
        {
            downhill.enabled = true;

            if (!StateManager.Instance.Cave)
            {
                if (!StateManager.Instance.Roadblocks)
                {
                    StateManager.Instance.Roadblocks = true;
                    StateManager.Instance.FireHazards = false;
                }
            }
            else
            {
                StateManager.Instance.FireHazards = true;
                CenterCart();
                StateManager.Instance.Roadblocks = false;
            }

            mainCamera.camera.enabled = false;
            car.enabled = true;
        }
        else
        {
            StateManager.Instance.Roadblocks = false;
            StateManager.Instance.FireHazards = false;
            downhill.enabled = false;
            mainCamera.camera.enabled = true;
            car.enabled = false;
        }
    }

    void CenterCart()
    {
        if (currentTrack == trackNumber.RIGHT)
        {
            HopLeft();
        }
        else if (currentTrack == trackNumber.LEFT)
        {
            HopRight();
        }
    }

    void SetGround()
    {

        if (wheel.isGrounded)
        {
            minY = this.transform.position.y;
        }
    }
}
