using UnityEngine;
using System.Collections;
using System.IO;

public class BasicMovement : MonoBehaviour {

    public float maxVelocity; 
    public Camera mainCamera;
    public DistanceTraveled cartDistance;
    public ConstantForce downhill;

    private Vector3 moveNormal;
    private float offsetX = -4f, offsetY = 6, offsetZ = -18f, forceOnCart;
    private Camera chase, car;

    void Start()
    {
        SetForceOnCart();
        downhill.force = new Vector3(100, 0, 0);
        downhill.enabled = false;

        chase = GameObject.Find("Chase Camera").camera;
        car = GameObject.Find("First Person Camera").camera;
    }

    void Update()
    {
        DownhillCheck();

        CameraFollow();

    }

    public void Move() {

        if (cartDistance.Distance() <= 400 || this.transform.rotation.z > 0.001f)
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
        switch (PlayerSettings.Instance.Age)
        {
            case 65: forceOnCart = 10000;
                break;
            case 70: forceOnCart = 12500;
                break;
            case 75: forceOnCart = 15000;
                break;
            case 80: forceOnCart = 17500;
                break;
            case 85: forceOnCart = 20000;
                break;
            case 90: forceOnCart = 22500;
                break;
            default: forceOnCart = 7500;
                break;
        }
    }

    void DownhillCheck()
    {
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
