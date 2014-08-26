using UnityEngine;
using System.Collections;

public class WheelBehavior : MonoBehaviour {

	public Rigidbody cart;
	private float scale = 10.0f;

	void Update () {
		gameObject.transform.Rotate (Vector3.right * Time.deltaTime * cart.velocity.x * scale, Space.Self);
	}
}
