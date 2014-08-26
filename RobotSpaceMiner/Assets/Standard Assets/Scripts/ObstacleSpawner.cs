using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour {

    private int totalHits, missCount;
    private float startSpawn, previousSpawn, spawnDistance, spawnRate, baseRate;
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
        previousSpawn = totalHits = 0;
        startSpawn = 1000;
	}
	
	void Update () {

        CheckCart();
        HitCount();
	}

    void CheckCart(){

        // Check if the cart is in the right position to spawn a new hazard
        if(StateManager.Instance.FireHazards && cart.transform.position.x > startSpawn && (cart.transform.position.x - previousSpawn) > spawnRate) {

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
        spawnDistance = 150;
        switch (PlayerSettings.Instance.Age)
        {
            case 65: baseRate = spawnRate = 350;
                break;
            case 70: baseRate = spawnRate = 400;
                break;
            case 75: baseRate = spawnRate = 450;
                break;
            case 80: baseRate = spawnRate = 500;
                break;
            case 85: baseRate = spawnRate = 550;
                break;
            case 90: baseRate = spawnRate = 600;
                break;
            default: baseRate = spawnRate = 350;
                break;
        }
    }

    void HitCount()
    {
        if(LevelSystem.Instance.SuccessfulHits - LevelSystem.Instance.LevelUpRequirement >= 0)
        {
            LevelSystem.Instance.SuccessfulHits = 0;
            LevelSystem.Instance.LevelUpRequirement += 2;
            SetSpawnRate();
        }
    }

    void SetSpawnRate()
    {
        spawnRate = baseRate - (LevelSystem.Instance.Level * 45);
        if (spawnRate < 15)
        {
            spawnRate = 15;
        }
        LevelSystem.Instance.LevelIncrease();
    }

    public void IncreaseHitCount()
    {
        LevelSystem.Instance.SuccessfulHits++;
        totalHits++;
    }

    public void IncreaseMissCount()
    {
        missCount++;
    }

}
