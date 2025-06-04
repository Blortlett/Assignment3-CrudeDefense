using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPolarBear : MonoBehaviour, IEnemies
{
    public string Name { get; set; } = "Polar Bear";
    public float MoveSpeed { get; set; } = 1.5f;
    public float HitPoints { get; set; } = 70.0f;
    public int Cost { get; set; } = 5;
    public string RunTimeController { get; set; } = "Polarbear/AnimController_Animal_PolarBear";

    private void Start()
    {

    }

    private void Update()
    {

    }
}