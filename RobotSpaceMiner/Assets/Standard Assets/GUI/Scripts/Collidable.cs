using UnityEngine;
using System.Collections;

public class Collidable : MonoBehaviour {

    void Awake()
    {
        //this.gameObject.AddComponent<Rigidbody>();
        //rigidbody.useGravity = false;
        //rigidbody.isKinematic = false;
    }
    void OnCollisionEnter(Collision collision)
    {
        collision.rigidbody.velocity = Vector3.zero;
        collision.rigidbody.angularVelocity = Vector3.zero;
    }
}
