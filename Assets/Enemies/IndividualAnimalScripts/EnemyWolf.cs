using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWolf : MonoBehaviour, IEnemies
{
    public string Name { get; set; } = "Wolf";
    public float MoveSpeed { get; set; } = 3.0f;
    public float HitPoints { get; set; } = 10.0f;
    public int Cost { get; set; } = 3;
    public string RunTimeController { get; set; } = "Wolf/AnimController_Animal_Wolf";
}