using UnityEngine;
using System.Collections;

public class RoadblockSpawner : MonoBehaviour
{
    private int successfulDodgeCount, totalDodges, collisionCount;
    private float startSpawn, previousSpawn, spawnDistance, spawnRate, baseRate;
    private BasicMovement cart;

    private enum trackNumber
    {
        LEFT,
        CENTER,
        RIGHT
    }

    private trackNumber currentTrack;

    void Start()
    {

        // Set variables to objects
        cart = GameObject.Find("Cart").GetComponent<BasicMovement>();

        StartSpawnRate();
        previousSpawn = successfulDodgeCount = totalDodges = 0;
        currentTrack = trackNumber.CENTER;
        startSpawn = 500;
    }

    void Update()
    {

        CheckCart();
        //DodgeCount();
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

        // Instantiate obstacle (Fireball)
        GameObject roadblock;
        RoadblockBehavior roadblockBehavior;

        roadblock = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Roadblock"));
        roadblockBehavior = roadblock.GetComponent<RoadblockBehavior>();

        roadblock.transform.position = new Vector3(cart.transform.position.x + spawnDistance, cart.transform.position.y - cart.transform.localScale.y, cart.transform.position.z);

       /* switch (currentTrack)
        {
            case trackNumber.LEFT: roadblock.transform.position = new Vector3(cart.transform.position.x + spawnDistance, cart.transform.position.y - cart.transform.localScale.y, cart.transform.position.z);
                break;
            case trackNumber.CENTER: roadblock.transform.position = new Vector3(cart.transform.position.x + spawnDistance, cart.transform.position.y - cart.transform.localScale.y, cart.transform.position.z);
                break;
            case trackNumber.RIGHT: roadblock.transform.position = new Vector3(cart.transform.position.x + spawnDistance, cart.transform.position.y - cart.transform.localScale.y, cart.transform.position.z);
                break;
            default:
                break;
        }*/

    }

    void StartSpawnRate()
    {
        spawnDistance = 150;
        switch (PlayerSettings.Instance.Age)
        {
            case 65: baseRate = spawnRate = 150;
                break;
            case 70: baseRate = spawnRate = 200;
                break;
            case 75: baseRate = spawnRate = 250;
                break;
            case 80: baseRate = spawnRate = 300;
                break;
            case 85: baseRate = spawnRate = 350;
                break;
            case 90: baseRate = spawnRate = 400;
                break;
            default: baseRate = spawnRate = 150;
                break;
        }
    }

   /* void DodgeCount()
    {
        if (successfulDodgeCount - LevelSystem.Instance.LevelUpRequirement >= 0)
        {
            successfulDodgeCount = 0;
            LevelSystem.Instance.LevelUpRequirement += 2;
            SetSpawnRate();
        }
    }

    void SetSpawnRate()
    {
        spawnRate = baseRate - (LevelSystem.Instance.Level * 35);
        if (spawnRate < 15)
        {
            spawnRate = 15;
        }
        LevelSystem.Instance.LevelIncrease();
    }*/

    public void IncreaseDodgeCount()
    {
        successfulDodgeCount++;
        totalDodges++;
    }

    public void IncreaseCollisionCount()
    {
        collisionCount++;
    }

}
