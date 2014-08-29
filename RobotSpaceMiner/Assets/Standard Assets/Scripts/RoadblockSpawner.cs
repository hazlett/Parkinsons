using UnityEngine;
using System.Collections;

public class RoadblockSpawner : MonoBehaviour
{
    private int totalDodges, collisionCount;
    private float startSpawn, previousSpawn, spawnDistance, spawnRate, baseRate, offsetY;
    private BasicMovement cart;

    private enum trackNumber
    {
        LEFT,
        CENTER,
        RIGHT,
        LEFTCENTER,
        LEFTRIGHT,
        CENTERRIGHT
    }

    private trackNumber currentTrack;

    void Start()
    {

        // Set variables to objects
        cart = GameObject.Find("Cart").GetComponent<BasicMovement>();

        StartSpawnRate();
        previousSpawn = totalDodges = 0;
        currentTrack = trackNumber.CENTER;
        startSpawn = 500;
        offsetY = 1.5f;
    }

    void Update()
    {

        CheckCart();
        DodgeCount();
    }

    void CheckCart()
    {

        // Check if the cart is in the right position to spawn a new hazard
        if (StateManager.Instance.Roadblocks && cart.transform.position.x > startSpawn && (cart.transform.position.x - previousSpawn) > spawnRate)
        {

            previousSpawn = (int)cart.transform.position.x;
            if (StateManager.Instance.CurrentState == StateManager.State.PLAYING)
            {
                Spawn();
            }
        }
    }

    void Spawn()
    {
        currentTrack = (trackNumber)Random.Range(0, 6);

        // Instantiate obstacle (Roadblock)
        GameObject roadblock, roadblock2;
        Roadblock2Behavior roadblock2Behavior;
        RoadblockBehavior roadblockBehavior;

        roadblock = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Roadblock"));

        switch (currentTrack)
        {
            case trackNumber.LEFT:
            switch ((int)cart.currentTrack)
                {
                    case 2: roadblock.transform.position = new Vector3(cart.transform.position.x + spawnDistance, cart.minY - offsetY, cart.transform.position.z);
                        break;
                    case 1: roadblock.transform.position = new Vector3(cart.transform.position.x + spawnDistance, cart.minY - offsetY, cart.transform.position.z + 10);
                        break;
                    case 0: roadblock.transform.position = new Vector3(cart.transform.position.x + spawnDistance, cart.minY - offsetY, cart.transform.position.z + 20);
                        break;
                }
                break;
            case trackNumber.CENTER: switch ((int)cart.currentTrack)
                {
                    case 2: roadblock.transform.position = new Vector3(cart.transform.position.x + spawnDistance, cart.minY - offsetY, cart.transform.position.z - 10);
                        break;
                    case 1: roadblock.transform.position = new Vector3(cart.transform.position.x + spawnDistance, cart.minY - offsetY, cart.transform.position.z);
                        break;
                    case 0: roadblock.transform.position = new Vector3(cart.transform.position.x + spawnDistance, cart.minY - offsetY, cart.transform.position.z + 10);
                        break;
                }
                break;
            case trackNumber.RIGHT: switch ((int)cart.currentTrack)
                {
                    case 2: roadblock.transform.position = new Vector3(cart.transform.position.x + spawnDistance, cart.minY - offsetY, cart.transform.position.z - 20);
                        break;
                    case 1: roadblock.transform.position = new Vector3(cart.transform.position.x + spawnDistance, cart.minY - offsetY, cart.transform.position.z - 10);
                        break;
                    case 0: roadblock.transform.position = new Vector3(cart.transform.position.x + spawnDistance, cart.minY - offsetY, cart.transform.position.z);
                        break;
                }
                break;
            case trackNumber.LEFTCENTER: roadblock2 = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Roadblock2"));
                roadblockBehavior = roadblock.GetComponent<RoadblockBehavior>();
                roadblockBehavior.roadblockPair = roadblock2;
                roadblock2Behavior = roadblock2.GetComponent<Roadblock2Behavior>();
                roadblock2Behavior.roadBlockPair = roadblock;
            switch ((int)cart.currentTrack)
                {
                    case 2: roadblock.transform.position = new Vector3(cart.transform.position.x + spawnDistance, cart.minY - offsetY, cart.transform.position.z);
                        roadblock2.transform.position = new Vector3(cart.transform.position.x + spawnDistance, cart.minY - offsetY, cart.transform.position.z - 10);
                        break;
                    case 1: roadblock.transform.position = new Vector3(cart.transform.position.x + spawnDistance, cart.minY - offsetY, cart.transform.position.z + 10);
                        roadblock2.transform.position = new Vector3(cart.transform.position.x + spawnDistance, cart.minY - offsetY, cart.transform.position.z);
                        break;
                    case 0: roadblock.transform.position = new Vector3(cart.transform.position.x + spawnDistance, cart.minY - offsetY, cart.transform.position.z + 20);
                        roadblock2.transform.position = new Vector3(cart.transform.position.x + spawnDistance, cart.minY - offsetY, cart.transform.position.z + 10);
                        break;
                }
                break;
            case trackNumber.LEFTRIGHT: roadblock2 = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Roadblock2"));
                roadblockBehavior = roadblock.GetComponent<RoadblockBehavior>();
                roadblockBehavior.roadblockPair = roadblock2;
                roadblock2Behavior = roadblock2.GetComponent<Roadblock2Behavior>();
                roadblock2Behavior.roadBlockPair = roadblock;
                switch ((int)cart.currentTrack)
                {
                    case 2: roadblock.transform.position = new Vector3(cart.transform.position.x + spawnDistance, cart.minY - offsetY, cart.transform.position.z);
                        roadblock2.transform.position = new Vector3(cart.transform.position.x + spawnDistance, cart.minY - offsetY, cart.transform.position.z - 20);
                        break;
                    case 1: roadblock.transform.position = new Vector3(cart.transform.position.x + spawnDistance, cart.minY - offsetY, cart.transform.position.z + 10);
                        roadblock2.transform.position = new Vector3(cart.transform.position.x + spawnDistance, cart.minY - offsetY, cart.transform.position.z - 10);
                        break;
                    case 0: roadblock.transform.position = new Vector3(cart.transform.position.x + spawnDistance, cart.minY - offsetY, cart.transform.position.z + 20);
                        roadblock2.transform.position = new Vector3(cart.transform.position.x + spawnDistance, cart.minY - offsetY, cart.transform.position.z);
                        break;
                }
                break;
            case trackNumber.CENTERRIGHT: roadblock2 = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Roadblock2"));
                roadblockBehavior = roadblock.GetComponent<RoadblockBehavior>();
                roadblockBehavior.roadblockPair = roadblock2;
                roadblock2Behavior = roadblock2.GetComponent<Roadblock2Behavior>();
                roadblock2Behavior.roadBlockPair = roadblock;
                switch ((int)cart.currentTrack)
                {
                    case 2: roadblock.transform.position = new Vector3(cart.transform.position.x + spawnDistance, cart.minY - offsetY, cart.transform.position.z - 10);
                        roadblock2.transform.position = new Vector3(cart.transform.position.x + spawnDistance, cart.minY - offsetY, cart.transform.position.z - 20);
                        break;
                    case 1: roadblock.transform.position = new Vector3(cart.transform.position.x + spawnDistance, cart.minY - offsetY, cart.transform.position.z);
                        roadblock2.transform.position = new Vector3(cart.transform.position.x + spawnDistance, cart.minY - offsetY, cart.transform.position.z - 10);
                        break;
                    case 0: roadblock.transform.position = new Vector3(cart.transform.position.x + spawnDistance, cart.minY - offsetY, cart.transform.position.z + 10);
                        roadblock2.transform.position = new Vector3(cart.transform.position.x + spawnDistance, cart.minY - offsetY, cart.transform.position.z);
                        break;
                }
                break;
            default:
                break;
        }

    }

    void StartSpawnRate()
    {
        spawnDistance = 150;
        if (PlayerSettings.Instance.Age >= 60 && PlayerSettings.Instance.Age < 70)
        {
            baseRate = spawnRate = 150;
        }
        else if (PlayerSettings.Instance.Age >= 70 && PlayerSettings.Instance.Age < 75)
        {
            baseRate = spawnRate = 200;
        }
        else if (PlayerSettings.Instance.Age >= 75 && PlayerSettings.Instance.Age < 80)
        {
            baseRate = spawnRate = 250;
        }
        else if (PlayerSettings.Instance.Age >= 80 && PlayerSettings.Instance.Age < 85)
        {
            baseRate = spawnRate = 300;
        }
        else if (PlayerSettings.Instance.Age >= 85 && PlayerSettings.Instance.Age < 90)
        {
            baseRate = spawnRate = 350;
        }
        else if (PlayerSettings.Instance.Age >= 90)
        {
            baseRate = spawnRate = 400;
        }
        else
        {
            baseRate = spawnRate = 150;
        }
    }

    void DodgeCount()
    {
        if (LevelSystem.Instance.SuccessfulHits - LevelSystem.Instance.LevelUpRequirement >= 0)
        {
            LevelSystem.Instance.SuccessfulHits = 0;
            LevelSystem.Instance.LevelUpRequirement += 2;
            SetSpawnRate();
        }
    }

    void SetSpawnRate()
    {
        spawnRate = baseRate - (LevelSystem.Instance.Level * 25);
        if (spawnRate < 40)
        {
            spawnRate = 40;
        }
        LevelSystem.Instance.LevelIncrease();
    }

    public void IncreaseDodgeCount()
    {
        LevelSystem.Instance.SuccessfulHits++;
        totalDodges++;
    }

    public void IncreaseCollisionCount()
    {
        collisionCount++;
    }

}
