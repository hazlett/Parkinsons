using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour {

    public float spawnDistance;
    public GameObject hazard;

    private float startSpawn, previousSpawn;
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

        startSpawn = 400;
        previousSpawn = 0;
	}
	
	// Update is called once per frame
	void Update () {

        CheckCart();
	}

    void CheckCart(){

        if(cart.transform.position.x > 400 && ((cart.transform.position.x - previousSpawn) > spawnDistance)) {

            previousSpawn = (int)cart.transform.position.x;
            Spawn();
        }
    }

    void Spawn()
    {

        activeCube = (cubeGrid)Random.Range(0, 5);

        Debug.Log("Cube Grid: " + (int)activeCube);


        switch (activeCube)
        {
            case cubeGrid.TOPLEFT: hazard.transform.position = new Vector3(topLeftCube.transform.position.x + spawnDistance, topLeftCube.transform.position.y, topLeftCube.transform.position.z);
                topLeft.Activate();
                break;
            case cubeGrid.TOPCENTER: hazard.transform.position = new Vector3(topCenterCube.transform.position.x + spawnDistance, topCenterCube.transform.position.y, topCenterCube.transform.position.z);
                topCenter.Activate();
                break;
            case cubeGrid.TOPRIGHT: hazard.transform.position = new Vector3(topRightCube.transform.position.x + spawnDistance, topRightCube.transform.position.y, topRightCube.transform.position.z);
                topRight.Activate();
                break;
            case cubeGrid.MIDDLELEFT: hazard.transform.position = new Vector3(middleLeftCube.transform.position.x + spawnDistance, middleLeftCube.transform.position.y, middleLeftCube.transform.position.z);
                middleLeft.Activate();
                break;
            case cubeGrid.MIDDLECENTER: hazard.transform.position = new Vector3(middleCenterCube.transform.position.x + spawnDistance, middleCenterCube.transform.position.y, middleCenterCube.transform.position.z);
                middleCenter.Activate();
                break;
            case cubeGrid.MIDDLERIGHT: hazard.transform.position = new Vector3(middleRightCube.transform.position.x + spawnDistance, middleRightCube.transform.position.y, middleRightCube.transform.position.z);
                middleRight.Activate();
                break;
            default: activeCube = 0;
                break;
        }

    }

}
