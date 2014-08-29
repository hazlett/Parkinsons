using UnityEngine;
using System.Collections;

public class HandbarRotation : MonoBehaviour {

    public Rigidbody cart;
    private float scale = 2.0f;
    private bool switchDirections = false;
    private Vector3 direction = Vector3.right;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "cart")
        {
            SwitchDirections();
        }
    }
    void Update()
    {
        gameObject.transform.Rotate(direction * Time.deltaTime * cart.velocity.x * scale, Space.Self);
    }

    void SwitchDirections()
    {
        if (switchDirections)
        {
            switchDirections = false;
            direction = Vector3.right;
        }
        else
        {
            switchDirections = true;
            direction = Vector3.left;
        }
    }
}
