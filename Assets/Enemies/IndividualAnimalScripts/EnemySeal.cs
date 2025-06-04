using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySeal : MonoBehaviour, IEnemies
{
    public string Name { get; set; } = "Seal";
    public float MoveSpeed { get; set; } = 0.7f;
    public float HitPoints { get; set; } = 40.0f;
    public int Cost { get; set; } = 1;
    public string RunTimeController { get; set; } = "Seal/AnimController_Animal_Seal";
}