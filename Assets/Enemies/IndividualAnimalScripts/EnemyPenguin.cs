using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPenguin : MonoBehaviour, IEnemies
{
    public string Name { get; set; } = "Penguin";
    public float MoveSpeed { get; set; } = 2.0f;
    public float HitPoints { get; set; } = 10.0f;
    public int Cost { get; set; } = 4;
    public string RunTimeController { get; set; } = "Penguin/AnimController_Animal_Penguin";
}
