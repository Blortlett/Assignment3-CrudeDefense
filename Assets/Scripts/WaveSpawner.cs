using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner: MonoBehaviour
{
    [SerializeField] private List<Wave> Waves; //Wave Configurations
    //[SerializeField] private List<EnemyFlyWeight> EnemyTypes; //Storing all enemies
    [SerializeField] private List<Factory_Enemies.EnemyType> EnemyTypes;
    [SerializeField] private GameObject enemyPrefab; //Prefab for enemies
    [SerializeField] private Transform[] SpawnPoints; //Spawn locations

    private int CurrentWaveIndex = 0;

    private void Start()
    {
        foreach (Factory_Enemies.EnemyType enemyType in Enum.GetValues(typeof(Factory_Enemies.EnemyType)))
        {
            EnemyTypes.Add(enemyType);
        }
        ////Populating Flyweights
        //EnemyTypes = new List<EnemyFlyWeight>
        //{
        //    EnemyFlyweightFactory.PolarBear,
        //    EnemyFlyweightFactory.Penguin,
        //    EnemyFlyweightFactory.Wolf,
        //    EnemyFlyweightFactory.Owl,
        //    EnemyFlyweightFactory.Seal
        //};

        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        while (CurrentWaveIndex < Waves.Count)
        {
            Debug.Log("Entered spawner");
            Wave Wave = Waves[CurrentWaveIndex];

            yield return new WaitForSeconds(Wave.InitialDelay);

            int CurrentCostSum = 0;

            while (CurrentCostSum < Wave.MaxEnemyCost)
            {
                Debug.Log("Entered CurrentCostSum < Wave.MaxEnemyCost");
                List<Factory_Enemies.EnemyType> AffordableEnemies = new List<Factory_Enemies.EnemyType>();
                Debug.Log(AffordableEnemies);

                if (EnemyTypes == null || EnemyTypes.Count == 0)
                {
                    Debug.LogError("EnemyTypes list is empty!  Contents: " + (EnemyTypes == null ? "null" : EnemyTypes.Count));
                    yield break;
                }

                foreach (var Enemy in EnemyTypes)
                {
                    Debug.Log("Entered foreach Enemy in EnemyTypes");
                    int EnemyCost = Factory_Enemies.instance.GetCost(Enemy);
                    if (EnemyCost <= Wave.MaxEnemyCost - CurrentCostSum)
                    {
                        AffordableEnemies.Add(Enemy);
                    }
                }
                //If Polar Bear already exists, don't allow one to spawn
                bool PolarBearExists = EnemyTracker.instance.GetCount(Factory_Enemies.EnemyType.PolarBear) > 0;
                if (PolarBearExists)
                {
                    AffordableEnemies.Remove(Factory_Enemies.EnemyType.PolarBear);
                    if (AffordableEnemies.Count == 0)
                    {
                        Debug.Log("Entered AffordableEnemies.Count == 0");
                        break;
                    }
                }
                    

                Factory_Enemies.EnemyType ChosenType = AffordableEnemies[UnityEngine.Random.Range(0, AffordableEnemies.Count)];

                Transform SpawnPoint = SpawnPoints[UnityEngine.Random.Range(0, SpawnPoints.Length)];
                GameObject EnemyObject = Factory_Enemies.instance.CreateAnimal(ChosenType, SpawnPoint);

                //Update CurrentCostSum
                int ChosenCost = Factory_Enemies.instance.GetCost(ChosenType);
                CurrentCostSum += ChosenCost;
                Debug.Log($"Spawned{ChosenType} with cost {ChosenCost}. Total Cost: {CurrentCostSum / Wave.MaxEnemyCost}");

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
