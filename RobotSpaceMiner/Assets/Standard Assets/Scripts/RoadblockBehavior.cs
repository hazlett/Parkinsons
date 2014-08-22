using UnityEngine;
using System.Collections;

public class RoadblockBehavior : MonoBehaviour
{

    private float explosionDistance = 2.5f;
    private GameObject cart;
    private RoadblockSpawner spawner;

    void Start()
    {

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
            spawner.IncreaseDodgeCount();
            GameObject.Destroy(this.gameObject);
        }

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "cart")
        {
            Debug.Log("Cart Collision!!");
            spawner.IncreaseCollisionCount();
            GameObject explosion = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/TNTExplosion"));
            explosion.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 5, this.transform.position.z);

            // Force of the explosion
            cart.rigidbody.AddForce(new Vector3(-30000, 0, 0));
            cart.GetComponent<GameStats>().AddScore(-200);

            GameObject.Destroy(this.gameObject);
        }
    }
}