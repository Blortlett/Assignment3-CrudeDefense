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