using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public class Wave
    {
        public GameObject[] EnemyPrefab; //Array of enemies to pick from
        public int EnemyCount; //Enemies per wave
        public float SpawnRate; //Enemies per second
        public float TimeBetweenWaves;

        private int CurrentWave = 0;
        private float CountDown = 2.0f;
        private bool isSpawning = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSpawning && CountDown <= 0.0f)
        {
            SpawnWave();
            CountDown = TimeBetweenWaves;
        }

        CountDown -= Time.deltaTime;
    }

    
}
