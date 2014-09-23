using UnityEngine;
using System.Collections;

public class FireballBehavior : MonoBehaviour {

    private float explosionDistance = 2.5f;
    private int cubeNumber;
    public int CubeNumber { set {cubeNumber = value;} }
    private GameObject cart;
    private CubeGridCollider cubeGrid;
    private ObstacleSpawner spawner;

	void Start () {

        cart = GameObject.Find("Cart");


        setCube();
        spawner = GameObject.Find("Obstacle Spawner").GetComponent<ObstacleSpawner>();
	}

    void OnCollisionEnter(Collider collider)
    {
        if (collider.transform.tag == "CubeGrid")
        {
            DeactivateCube();
            spawner.IncreaseMissCount();
            GameObject explosion = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Explosion"));
            explosion.transform.position = this.transform.position;

            // Force of the explosion
            cart.rigidbody.AddForce(new Vector3(-25000, 0, 0));
            cart.GetComponent<GameStats>().AddScore(-100);

            GameObject.Destroy(this.gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (StateManager.Instance.CurrentState == StateManager.State.GAMEOVER)
        {
            DeactivateCube();
            GameObject.Destroy(this.gameObject);
        }

        // If the cart is past the fireball, but off to the side, destroy the object
        if (this.transform.position.x < cart.transform.position.x)
        {
            GameObject.Destroy(this.gameObject);
        }
	}

    void setCube()
    {
        switch (cubeNumber)
        {
            case 0: cubeGrid = GameObject.Find("TopLeft").GetComponent<CubeGridCollider>();
                break;
            case 1: cubeGrid = GameObject.Find("TopCenter").GetComponent<CubeGridCollider>();
                break;
            case 2: cubeGrid = GameObject.Find("TopRight").GetComponent<CubeGridCollider>();
                break;
            case 3: cubeGrid = GameObject.Find("MiddleLeft").GetComponent<CubeGridCollider>();
                break;
            case 4: cubeGrid = GameObject.Find("MiddleCenter").GetComponent<CubeGridCollider>();
                break;
            case 5: cubeGrid = GameObject.Find("MiddleRight").GetComponent<CubeGridCollider>();
                break;
        }
    }

    void DeactivateCube()
    {
        cubeGrid.DeactivateHit();
    }

}
