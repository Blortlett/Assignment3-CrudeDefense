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

    public void TakeDammage(float _Dammage)
    {
        Debug.Log("Take DAMMAGE");
        HitPoints -= _Dammage;

        if (HitPoints <= 0)
        {
            Debug.Log("DIE DIE DIE DIE DIE DIE DIE");
            Die();
        }
    }

    public void Die();
}