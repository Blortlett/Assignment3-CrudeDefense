using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemies
{
    public string Name { get; set; }
    public float MoveSpeed { get; set; }
    public float HitPoints { get; set; }
    public int Cost { get; set; }
    public string RunTimeController { get; set; }
}
public class EnemyPolarBear : MonoBehaviour, IEnemies
{
    public string Name { get; set; } = "Polar Bear";
    public float MoveSpeed { get; set; } = 1.5f;
    public float HitPoints { get; set; } = 70.0f;
    public int Cost { get; set; } = 5;
    public string RunTimeController { get; set; } = "Polarbear/AnimController_Animal_PolarBear";
}
public class EnemyPenguin : MonoBehaviour, IEnemies
{
    public string Name { get; set; } = "Penguin";
    public float MoveSpeed { get; set; } = 2.0f;
    public float HitPoints { get; set; } = 10.0f;
    public int Cost { get; set; } = 4;
    public string RunTimeController { get; set; } = "Penguin/AnimController_Animal_Penguin";
}
public class EnemyWolf : MonoBehaviour, IEnemies
{
    public string Name { get; set; } = "Wolf";
    public float MoveSpeed { get; set; } = 3.0f;
    public float HitPoints { get; set; } = 10.0f;
    public int Cost { get; set; } = 3;
    public string RunTimeController { get; set; } = "Wolf/AnimController_Animal_Wolf";
}
public class EnemyOwl : MonoBehaviour, IEnemies
{
    public string Name { get; set; } = "Owl";
    public float MoveSpeed { get; set; } = 5.0f;
    public float HitPoints { get; set; } = 40.0f;
    public int Cost { get; set; } = 2;
    public string RunTimeController { get; set; } = "Owl/AnimController_Animal_Owl";
}
public class EnemySeal : MonoBehaviour, IEnemies
{
    public string Name { get; set; } = "Seal";
    public float MoveSpeed { get; set; } = 0.7f;
    public float HitPoints { get; set; } = 40.0f;
    public int Cost { get; set; } = 1;
    public string RunTimeController { get; set; } = "Seal/AnimController_Animal_Seal";
}