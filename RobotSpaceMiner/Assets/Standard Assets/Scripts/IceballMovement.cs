using UnityEngine;
using System.Collections;

public class IceballMovement : MonoBehaviour {

	void Start () {
        this.rigidbody.velocity = new Vector3(60, 0, 0); 
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Fireball")
        {
            // If the iceball collides with the hazard, spawn fireworks and increase score
            GameObject destroyedFireworks;
            destroyedFireworks = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/GreenFireworks"));
            destroyedFireworks.transform.position = this.transform.position;

			GameObject.Find("Cart").GetComponent<GameStats>().AddScore(50);

            GameObject.Destroy(collider.gameObject);
            GameObject.Destroy(this.gameObject);          
        }
    }
}
