using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour {

    private float startSpawn, previousSpawn, spawnDistance, spawnRate;
    private BasicMovement cart;
    private GameObject topLeftCube, topCenterCube, topRightCube, middleLeftCube, middleCenterCube, middleRightCube;
    private CubeGridCollider topLeft, topCenter, topRight, middleLeft, middleCenter, middleRight;

    private enum cubeGrid
    {
        TOPLEFT,
        TOPCENTER,
        TOPRIGHT,
        MIDDLELEFT,
        MIDDLECENTER,
        MIDDLERIGHT
    }

    private cubeGrid activeCube;

	void Start () {

        // Set variables to objects
        cart = GameObject.Find("Cart").GetComponent<BasicMovement>();

        topLeftCube = GameObject.Find("TopLeft");
        topCenterCube = GameObject.Find("TopCenter");
        topRightCube = GameObject.Find("TopRight");
        middleLeftCube = GameObject.Find("MiddleLeft");
        middleCenterCube = GameObject.Find("MiddleCenter");
        middleRightCube = GameObject.Find("MiddleRight");

        topLeft = topLeftCube.GetComponent<CubeGridCollider>();
        topCenter = topCenterCube.GetComponent<CubeGridCollider>();
        topRight = topRightCube.GetComponent<CubeGridCollider>();
        middleLeft = middleLeftCube.GetComponent<CubeGridCollider>();
        middleCenter = middleCenterCube.GetComponent<CubeGridCollider>();
        middleRight = middleRightCube.GetComponent<CubeGridCollider>();

        StartSpawnRate();
        previousSpawn = 0;
	}
	
	void Update () {

        CheckCart();
	}

    void CheckCart(){

        // Check if the cart is in the right position to spawn a new hazard
        if(cart.transform.position.x > 400 && ((cart.transform.position.x - previousSpawn) > spawnRate) && (cart.transform.rotation.z < 0.01f)) {

            previousSpawn = (int)cart.transform.position.x;
            if (StateManager.Instance.CurrentState == StateManager.State.PLAYING)
            {
                Spawn();
            }
        }
    }

    void Spawn()
    {

        // Instantiate obstacle (Fireball)
        GameObject hazardousElement;
        FireballBehavior hazardBehavior;

        activeCube = (cubeGrid)Random.Range(0, 6);

        hazardousElement =  (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/RedFireball"));
        hazardBehavior = hazardousElement.GetComponent<FireballBehavior>();

        // Based on which grid cube is active, place the obstacle in the cube's path
        switch (activeCube)
        {
            case cubeGrid.TOPLEFT: hazardousElement.transform.position = new Vector3(topLeftCube.transform.position.x + spawnDistance, topLeftCube.transform.position.y, topLeftCube.transform.position.z);
                topLeft.Activate();
                hazardBehavior.CubeNumber = 0;
                break;
            case cubeGrid.TOPCENTER: hazardousElement.transform.position = new Vector3(topCenterCube.transform.position.x + spawnDistance, topCenterCube.transform.position.y, topCenterCube.transform.position.z);
                topCenter.Activate();
                hazardBehavior.CubeNumber = 1;
                break;
            case cubeGrid.TOPRIGHT: hazardousElement.transform.position = new Vector3(topRightCube.transform.position.x + spawnDistance, topRightCube.transform.position.y, topRightCube.transform.position.z);
                topRight.Activate();
                hazardBehavior.CubeNumber = 2;
                break;
            case cubeGrid.MIDDLELEFT: hazardousElement.transform.position = new Vector3(middleLeftCube.transform.position.x + spawnDistance, middleLeftCube.transform.position.y, middleLeftCube.transform.position.z);
                middleLeft.Activate();
                hazardBehavior.CubeNumber = 3;
                break;
            case cubeGrid.MIDDLECENTER: hazardousElement.transform.position = new Vector3(middleCenterCube.transform.position.x + spawnDistance, middleCenterCube.transform.position.y, middleCenterCube.transform.position.z);
                middleCenter.Activate();
                hazardBehavior.CubeNumber = 4;
                break;
            case cubeGrid.MIDDLERIGHT: hazardousElement.transform.position = new Vector3(middleRightCube.transform.position.x + spawnDistance, middleRightCube.transform.position.y, middleRightCube.transform.position.z);
                middleRight.Activate();
                hazardBehavior.CubeNumber = 5;
                break;
            default: activeCube = 0;
                break;
        }

    }

    void StartSpawnRate()
    {
        spawnDistance = 250;

        switch (PlayerSettings.Instance.Age)
        {
            case 65: spawnRate = 1000;
                break;
            case 70: spawnRate = 1100;
                break;
            case 75: spawnRate = 1200;
                break;
            case 80: spawnRate = 1300;
                break;
            case 85: spawnRate = 1400;
                break;
            case 90: spawnRate = 1500;
                break;
            default: spawnRate = 750;
                break;
        }
    }

}
