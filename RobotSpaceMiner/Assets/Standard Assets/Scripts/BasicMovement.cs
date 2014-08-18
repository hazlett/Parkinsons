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
        Gravity();
        if (StateManager.Instance.CurrentState == StateManager.State.PLAYING)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Pumped();
            }

        }

        if(cartDistance.Distance() >= 300) {
            downhill.enabled = true;
            mainCamera.camera.enabled = false;
            chase.enabled = false;
            car.enabled = true;
        }

        CheckVelocity();
        CameraFollow();

    }

    public void Pumped() {

        if (cartDistance.Distance() < 300)
        {
            if (rigidbody.velocity.x <= maxVelocity && IsGrounded())
            {
                rigidbody.AddForce(new Vector3(7500, 0, 0));
            }
        }
        

    }

    void CheckVelocity()
    {
        // Cap the velocity
        if (rigidbody.velocity.x > maxVelocity)
        {
            rigidbody.velocity = new Vector3(maxVelocity, rigidbody.velocity.y, rigidbody.velocity.z);
        }
    }

    void Gravity() {
        
        moveNormal = -this.transform.up;

        /* Gravity always in carts -y direction */
        //Physics.gravity = moveNormal * 100;
        //Physics.gravity = new Vector3(0, -27, 0);
    }

    void CameraFollow() {

        mainCamera.transform.position = new Vector3(this.transform.position.x + offsetX, this.transform.position.y + offsetY, -20);
    }

    internal bool IsGrounded() {
        return !Physics.Raycast(transform.position, -Vector3.up, distanceToGround - 0.5f);
    }

    internal float Velocity()
    {
        return rigidbody.velocity.x;
    }
}
