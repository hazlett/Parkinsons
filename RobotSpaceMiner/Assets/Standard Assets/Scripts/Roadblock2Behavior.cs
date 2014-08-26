using UnityEngine;
using System.Collections;

public class Roadblock2Behavior : MonoBehaviour
{
    ParticleSystem decrease;
    private float explosionDistance = 2.5f;
    private GameObject cart;
    private RoadblockSpawner spawner;

    void Start()
    {
        decrease = GameObject.Find("Decrease_Score").GetComponent<ParticleSystem>();

        cart = GameObject.Find("Cart");
        spawner = GameObject.Find("Roadblock Spawner").GetComponent<RoadblockSpawner>();
    }

    // Update is called once per frame
    void Update()
    {

        if (StateManager.Instance.CurrentState == StateManager.State.GAMEOVER || StateManager.Instance.Cave)
        {

            GameObject explosion = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/TNTExplosionNoScore"));
            explosion.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 5, this.transform.position.z);
            GameObject.Destroy(this.gameObject);
        }

        if (cart.transform.position.x > this.transform.position.x)
        {
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

            decrease.Stop();
            decrease.enableEmission = true;
            decrease.Play();

            // Force of the explosion
            cart.rigidbody.AddForce(new Vector3(-30000, 0, 0));
            cart.GetComponent<GameStats>().AddScore(-100);

            GameObject.Destroy(this.gameObject);
        }
    }
}