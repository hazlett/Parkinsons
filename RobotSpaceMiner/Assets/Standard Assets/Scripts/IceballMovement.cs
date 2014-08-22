using UnityEngine;
using System.Collections;

public class IceballMovement : MonoBehaviour {

    private ObstacleSpawner spawner;
	void Start () {
        this.rigidbody.velocity = new Vector3(60, 0, 0);
        spawner = GameObject.Find("Obstacle Spawner").GetComponent<ObstacleSpawner>();
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Fireball")
        {
            // If the iceball collides with the hazard, spawn fireworks and increase score
            GameObject destroyedFireworks;
            destroyedFireworks = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Fireworks"));
            destroyedFireworks.transform.position = this.transform.position;
            spawner.IncreaseHitCount();

			GameObject.Find("Cart").GetComponent<GameStats>().AddScore(150);

            GameObject.Destroy(collider.gameObject);
            GameObject.Destroy(this.gameObject);          
        }
    }
}
