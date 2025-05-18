using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WaveSpawner;

public class WaveSpawner : MonoBehaviour
{
    public GameObject mEnemyPrefab; // Enemy Factory
    public int mEnemyCount; //Enemies per wave

    public float mTimeBetweenWaves;
    private int CurrentWave = 0;

    private float mEnemySpawnTimer;
    public float mEnemySpawnInterval = 3f; // Time interval between enemy spawns
    private bool isSpawning = false;

    public enum eEnemyTypes
    {
        PolarBear,
        Penguin,
        Wolf,
        Owl
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Handle Enemy Spawning
        mEnemySpawnTimer -= Time.deltaTime;
        if (mEnemySpawnTimer <= 0f)
        {
            SpawnEnemy();
            mEnemySpawnTimer = mEnemySpawnInterval;  // Reset the enemy spawn timer
        }

        /*if (!isSpawning && CountDown <= 0.0f)
        {
            //SpawnWave();
            CountDown = TimeBetweenWaves;
        }

        CountDown -= Time.deltaTime;*/
    }

    private void SpawnEnemy()
    {
        // Choose a random enemy type
        eEnemyTypes randomEnemyType = (eEnemyTypes)Random.Range(0, System.Enum.GetValues(typeof(eEnemyTypes)).Length); // Get random enemy from enemy types

        // Spawn the enemy either left or right of screen
        Vector3 spawnPosition;
        float randomX = Random.Range(0, 2);
        if (randomX == 1)
            spawnPosition = new Vector3(10f, -3.247f, -1); // spawn right of screen
        else
            spawnPosition = new Vector3(-10f, -3.247f, -1); // spawn left of screen

        // Instantiate enemy
        GameObject enemy = Instantiate(mEnemyPrefab, spawnPosition, Quaternion.identity);
        Enemy enemyScript = enemy.GetComponent<Enemy>();

        // Initialize the enemy with the correct flyweight data
        if (enemyScript != null)
        {
            switch (randomEnemyType)
            {
                case eEnemyTypes.PolarBear:
                    enemyScript.Initialize(EnemyFlyweightFactory.PolarBear);
                    break;
                case eEnemyTypes.Penguin:
                    enemyScript.Initialize(EnemyFlyweightFactory.Penguin);
                    break;
                case eEnemyTypes.Wolf:
                    enemyScript.Initialize(EnemyFlyweightFactory.Wolf);
                    break;
                case eEnemyTypes.Owl:
                    enemyScript.Initialize(EnemyFlyweightFactory.Owl);
                    break;
            }
        }
    }
}
