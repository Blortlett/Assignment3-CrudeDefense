using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyOwl : MonoBehaviour, IEnemies
{
    public string Name { get; set; } = "Owl";
    public float MoveSpeed { get; set; } = 5.0f;
    public float HitPoints { get; set; } = 40.0f;
    public int Cost { get; set; } = 2;
    public string RunTimeController { get; set; } = "Owl/AnimController_Animal_Owl";

    private void Start()
    {
        // move owl up
        transform.position += new Vector3(0, 3.1f, 0);
    }

}

