using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour{

	public Camera playerCamera;
    
	public float speed = 30.0f;
	public float jumpSpeed = 35.0f;
	private float gravity = 20.0f;
	private Vector3 moveDirection = Vector3.zero;
    private float pushDistance = 50.0f;

	private float maxDistance = 1.0f;
	private float currentX;
	private float initialX;
    public float DistanceTraveled { get { return (currentX - initialX); } }
    public float ScaledDistance { get { return Mathf.Round(DistanceTraveled / 10); } }
	void Start () {
		initialX = transform.position.x;
		playerCamera.transparencySortMode = TransparencySortMode.Orthographic;
	}

	void Update () 
	{
        if (StateManager.Instance.Paused)
            return;
		CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(1, 0, 0);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
            if (Input.touchCount > 0)
            {
                moveDirection.y = jumpSpeed;
            }
        }
        moveDirection.y -= gravity * Time.smoothDeltaTime;
		if (DistanceTraveled < maxDistance)
		{
			controller.Move(moveDirection * Time.smoothDeltaTime);
		}
		currentX = transform.position.x;
		playerCamera.transform.position = new Vector3(transform.position.x, transform.position.y + 10, playerCamera.transform.position.z);

	}
	public void Push(float distance)
	{
		maxDistance += distance;
	}
	public void Push()
	{
        maxDistance += pushDistance;
	}
}
