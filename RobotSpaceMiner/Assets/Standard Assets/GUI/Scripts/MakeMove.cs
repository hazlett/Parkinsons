using UnityEngine;
using System.Collections;

public class MakeMove : MonoBehaviour
{
	public Camera playerCamera;

	public float XDistance = 0.0f;
	private float xMoved = 0.0f;
	private Vector3 velocity = new Vector3(0.1f, 0, 0);

	public float speed = 12.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	private Vector3 moveDirection = Vector3.zero;

	// Use this for initialization
	void Start () {
		XDistance += this.transform.position.x;
		xMoved += this.transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (xMoved < XDistance)
		{
			CharacterController controller = GetComponent<CharacterController>();
			if (controller.isGrounded)
			{
				moveDirection = new Vector3(1, 0, Input.GetAxis("Vertical"));
				moveDirection = transform.TransformDirection(moveDirection);
				moveDirection *= speed;				
			}
			moveDirection.y -= gravity * Time.smoothDeltaTime;
			controller.Move(moveDirection * Time.smoothDeltaTime);

			xMoved = transform.position.x;

		}
		playerCamera.transform.position = new Vector3(transform.position.x, transform.position.y + 10, playerCamera.transform.position.z);
	}

	public void AddDistance(float distance)
	{
		XDistance += distance;
	}
}
