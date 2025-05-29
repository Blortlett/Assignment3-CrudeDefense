using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner: MonoBehaviour
{
    [SerializeField] private List<Wave> Waves; //Wave Configurations
    [SerializeField] private List<EnemyFlyWeight> EnemyTypes; //Storing all enemies
    [SerializeField] private GameObject enemyPrefab; //Prefab for enemies
    [SerializeField] private Transform[] SpawnPoints; //Spawn locations

    private int CurrentWaveIndex = 0;

    private void Start()
    {
        //Populating Flyweights
        EnemyTypes = new List<EnemyFlyWeight>
        {
            EnemyFlyweightFactory.PolarBear,
            EnemyFlyweightFactory.Penguin,
            EnemyFlyweightFactory.Wolf,
            EnemyFlyweightFactory.Owl,
            EnemyFlyweightFactory.Seal
        };

        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        while (CurrentWaveIndex < Waves.Count)
        {
            Wave Wave = Waves[CurrentWaveIndex];

            yield return new WaitForSeconds(Wave.InitialDelay);

            int CurrentCostSum = 0;

            while (CurrentCostSum < Wave.MaxEnemyCost)
            {
                List<EnemyFlyWeight> AffordableEnemies = new List<EnemyFlyWeight>();
                //While there's budget to add enemies to the list to spawn,
                //add enemies.
                foreach (EnemyFlyWeight ET in EnemyTypes)
                {
                    if (ET.miCost <= (Wave.MaxEnemyCost - CurrentCostSum))
                    {
                        AffordableEnemies.Add(ET);
                    }
                }

                //If we run out of AffordableEnemies, stop spawning
                if (AffordableEnemies.Count == 0)
                {
                    break;
                }

                //Pick a random enemy FlyWeight
                EnemyFlyWeight ChosenFlyWeight = AffordableEnemies[Random.Range(0, AffordableEnemies.Count)];
                //Pick a spawn point
                Transform SpawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
                //Instantiate the enemy
                GameObject EnemyObject = Instantiate(enemyPrefab, SpawnPoint.position, Quaternion.identity);

                //Initialise the enemy
                Enemy EnemyComp = EnemyObject.GetComponent<Enemy>();
                if (EnemyComp != null)
                {
                    EnemyComp.Initialize(ChosenFlyWeight);
                }

                //Update CurrentCostSum
                CurrentCostSum += ChosenFlyWeight.miCost;

                //Wait for spawn interval before spawning new enemy
                yield return new WaitForSeconds(Wave.SpawnInterval);
            }

            //Wait before starting the next wave
            yield return new WaitForSeconds(Wave.WaveDelay);
            CurrentWaveIndex++;
        }
    }
}

[System.Serializable]
public class Wave
{
    //public int EnemyCount; //Amount of enemies per wave
    public float SpawnInterval; //Delay between enemy spawns
    public float InitialDelay; //Delay before initial wave spawns
    public float WaveDelay; //Delay between waves spawning
    public int MaxEnemyCost; //Maximum "value" of a wave
}

//using static WaveSpawner;

//public class WaveSpawner : MonoBehaviour
//{
//    public GameObject mEnemyPrefab; // Enemy Factory
//    public int mEnemyCount; //Enemies per wave

//    public float mTimeBetweenWaves;

//    private float mEnemySpawnTimer;
//    public float mEnemySpawnInterval = 3f; // Time interval between enemy spawns

//    public enum eEnemyTypes
//    {
//        PolarBear,
//        Penguin,
//        Wolf,
//        Owl
//    }

//    // Start is called before the first frame update
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        // Handle Enemy Spawning
//        mEnemySpawnTimer -= Time.deltaTime;
//        if (mEnemySpawnTimer <= 0f)
//        {
//            SpawnEnemy();
//            mEnemySpawnTimer = mEnemySpawnInterval;  // Reset the enemy spawn timer
//        }
//    }

//    private void SpawnEnemy()
//    {
//        // Choose a random enemy type
//        eEnemyTypes randomEnemyType = (eEnemyTypes)Random.Range(0, System.Enum.GetValues(typeof(eEnemyTypes)).Length); // Get random enemy from enemy types

//        // Spawn the enemy either left or right of screen
//        Vector3 spawnPosition;
//        float randomX = Random.Range(0, 2);
//        if (randomX == 1)
//            spawnPosition = new Vector3(10f, -3.247f, -1); // spawn right of screen
//        else
//            spawnPosition = new Vector3(-10f, -3.247f, -1); // spawn left of screen

//        // Instantiate enemy
//        GameObject enemy = Instantiate(mEnemyPrefab, spawnPosition, Quaternion.identity);
//        Enemy enemyScript = enemy.GetComponent<Enemy>();

//        // Initialize the enemy with the correct flyweight data
//        if (enemyScript != null)
//        {
//            switch (randomEnemyType)
//            {
//                case eEnemyTypes.PolarBear:
//                    enemyScript.Initialize(EnemyFlyweightFactory.PolarBear);
//                    break;
//                case eEnemyTypes.Penguin:
//                    enemyScript.Initialize(EnemyFlyweightFactory.Penguin);
//                    break;
//                case eEnemyTypes.Wolf:
//                    enemyScript.Initialize(EnemyFlyweightFactory.Wolf);
//                    break;
//                case eEnemyTypes.Owl:
//                    enemyScript.Initialize(EnemyFlyweightFactory.Owl);
//                    break;
//            }
//        }
//    }
//}
