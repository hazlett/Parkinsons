using UnityEngine;
using System.Collections;
using System.IO;

public class BasicMovement : MonoBehaviour {

    public float maxVelocity; 
    public Camera mainCamera;
    public DistanceTraveled cartDistance;
    public ConstantForce downhill;

    private Vector3 moveNormal;
    private float offsetX = -6f, offsetY = 7, distanceToGround;
    private Camera chase, car;

    void Start()
    {
        distanceToGround = this.collider.bounds.extents.y;
        downhill.force = new Vector3(100, 0, 0);
        downhill.enabled = false;

        chase = GameObject.Find("Chase Camera").camera;
        car = GameObject.Find("First Person Camera").camera;
    }

    void Update()
    {
        if (StateManager.Instance.CurrentState == StateManager.State.PLAYING)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Move();
            }

        }

        DownhillCheck();

        CameraFollow();

    }

    public void Move() {

        // User based movement if the cart is going uphill.
        if (cartDistance.Distance() <= 400 || this.transform.rotation.z > 0.001f)
        {
			if (StateManager.Instance.TimerPause)
			{
				StateManager.Instance.StartTimer();
			}
            if (rigidbody.velocity.x <= maxVelocity)
            {
                rigidbody.AddForce(new Vector3(7500, 0, 0));
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

        mainCamera.transform.position = new Vector3(this.transform.position.x + offsetX, this.transform.position.y + offsetY, -20);
    }


    internal float Velocity()
    {
        return rigidbody.velocity.x;
    }

    void DownhillCheck()
    {
        // If the cart has no rotation, enable obstacle spawning
        // Also add a constant force to the cart
        if (cartDistance.Distance() >= 400 && this.transform.rotation.z < 0.001f)
        {
            downhill.enabled = true;
            StateManager.Instance.Downhill = true;
            mainCamera.camera.enabled = false;
            chase.enabled = false;
            car.enabled = true;
        }
        else
        {
            downhill.enabled = false;
            StateManager.Instance.Downhill = false;
            mainCamera.camera.enabled = true;
            car.enabled = false;
        }
    }
}
