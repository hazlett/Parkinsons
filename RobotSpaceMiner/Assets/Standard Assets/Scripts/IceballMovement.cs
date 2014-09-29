using UnityEngine;
using System.Collections;

public class IceballMovement : MonoBehaviour {

    private ParticleSystem niceShot, increase;
    private ObstacleSpawner spawner;
    private float timer;

	void Start () {
        this.rigidbody.velocity = new Vector3(60, 0, 0);
        spawner = GameObject.Find("Obstacle Spawner").GetComponent<ObstacleSpawner>();
        niceShot = GameObject.Find("Nice_Shot_Popup").GetComponent<ParticleSystem>();
        increase = GameObject.Find("Increase_Score").GetComponent<ParticleSystem>();
	}

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 5.0f)
        {
            GameObject.Destroy(this.gameObject);
        }
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

            niceShot.Stop();
            niceShot.enableEmission = true;
            niceShot.Play();
            increase.Stop();
            increase.enableEmission = true;
            increase.Play();
			GameObject.Find("Cart").GetComponent<GameStats>().AddScore(150);

            GameObject.Destroy(collider.gameObject);
            GameObject.Destroy(this.gameObject);          
        }
    }
}
