using UnityEngine;
using System.Collections;
using System;

public class RoadblockBehavior : MonoBehaviour
{
    public GameObject roadblockPair;

    private ParticleSystem dodge, increase, decrease;
    private float explosionDistance = 2.5f;
    private GameObject cart;
    private RoadblockSpawner spawner;

    void Start()
    {
        dodge = GameObject.Find("Dodge_Popup").GetComponent<ParticleSystem>();
        increase = GameObject.Find("Increase_Score").GetComponent<ParticleSystem>();
        decrease = GameObject.Find("Decrease_Score").GetComponent<ParticleSystem>();

        cart = GameObject.Find("Cart");
        spawner = GameObject.Find("Roadblock Spawner").GetComponent<RoadblockSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (StateManager.Instance.CurrentState == StateManager.State.GAMEOVER || StateManager.Instance.Cave || cart.transform.rotation.x < -0.550001f)
        {

            GameObject explosion = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/TNTExplosionNoScore"));
            explosion.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 5, this.transform.position.z);
            GameObject.Destroy(this.gameObject);
        }

        if (cart.transform.position.x > this.transform.position.x)
        {
            increase.Stop();
            increase.enableEmission = true;
            increase.Play();

            dodge.Stop();
            dodge.enableEmission = true;
            dodge.Play();

            GameObject.Find("Cart").GetComponent<GameStats>().AddScore(150);
            spawner.IncreaseDodgeCount();
            GameObject.Destroy(this.gameObject);
        }

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "cart")
        {
            spawner.IncreaseCollisionCount();
            GameObject explosion = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/TNTExplosion"));
            explosion.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 5, this.transform.position.z);

            if (roadblockPair != null)
            {
                GameObject explosionPair = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/TNTExplosion"));
                explosionPair.transform.position = new Vector3(roadblockPair.transform.position.x, roadblockPair.transform.position.y + 5, roadblockPair.transform.position.z);
            }

            decrease.Stop();
            decrease.enableEmission = true;
            decrease.Play();

            // Force of the explosion
            cart.rigidbody.AddForce(new Vector3(-30000, 0, 0));
            cart.GetComponent<GameStats>().AddScore(-100);
			try {
            GameObject.Destroy(roadblockPair.gameObject);
			}
			catch(Exception)
			{

				}
            GameObject.Destroy(this.gameObject);
        }
    }
}