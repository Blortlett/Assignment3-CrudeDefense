using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Factory_Enemies;

public class EnemyTracker : MonoBehaviour
{
    public static EnemyTracker instance;

    private Dictionary<EnemyType, List<GameObject>> ActiveEnemies = new();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            //Initialising dictionary ActiveEnemies to track each enemy type (e.g PolarBear, Penguin)
            //This creates a list for each EnemyType
            //ActiveEnemies[EnemyType.PolarBear] = new List<GameObject>(); for example
            foreach (EnemyType Type in System.Enum.GetValues(typeof(EnemyType)))
            {
                ActiveEnemies[Type] = new List<GameObject>();
            }
        }
        //Making sure only one tracker exists
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterEnemy(EnemyType Type, GameObject Enemy)
    {
        //If the dictionary doesn't have a list for the EnemyType,
        //create one. Otherwise, add it to the associated list.
        if (!ActiveEnemies.ContainsKey(Type))
        {
            ActiveEnemies[Type] = new List<GameObject>();
        }

        ActiveEnemies[Type].Add(Enemy);
    }

    //Call to remove enemies from the list when they die/are destroyed
    public void UnregisterEnemy(EnemyType Type, GameObject Enemy)
    {
        if (ActiveEnemies.ContainsKey(Type))
        {
            ActiveEnemies[Type].Remove(Enemy);
        }
    }

    public int GetCount(EnemyType Type)
    {
        //Checks if ActiveEnemies contains the Enemy Type
        //If it does, return the amount of enemies in that list
        //If it doesn't, return 0
        if (ActiveEnemies.TryGetValue(Type, out var List))
        {
            return List.Count;
        }
        else
        {
            return 0;
        }
    }
}
