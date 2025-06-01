using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;


public class Factory_Enemies : MonoBehaviour
{
    public enum EnemyType
    {
        PolarBear,
        Penguin,
        Wolf,
        Owl,
        Seal
    }
    public static Factory_Enemies instance { private set; get; }
    public GameObject AnimalPrefab;
    public RuntimeAnimatorController[] AnimalAnimators;

    void Awake()
    {
        if ((instance != null) && (instance != this))
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public GameObject CreateAnimal(EnemyType _Type, Transform _Position)
    {
        // return created object
        GameObject Animal = Instantiate(AnimalPrefab, _Position);
        AttachAnimalScript(_Type, Animal);
        IEnemies EnemyInterface = Animal.GetComponent<IEnemies>();
        Enemy EnemyComponent = Animal.GetComponent<Enemy>();
        if (EnemyInterface != null)
        {
            //Getting the flyweight instance
            EnemyFlyWeight Flyweight = GetFlyweight(_Type);
            EnemyComponent.Initialize(Flyweight);

            string RuntimePath = EnemyInterface.RunTimeController;
            SetAnimalAnimator(_Type, Animal, RuntimePath);
        }
        else
        {
            Debug.Log("Enemy component using IEnemies not found");
        }
        return Animal;
    }
    private IEnemies AttachAnimalScript(EnemyType _Type, GameObject _AnimalPrefab)
    {
        switch (_Type)
        {
            case EnemyType.PolarBear: 
                return _AnimalPrefab.AddComponent<EnemyPolarBear>();
            case EnemyType.Penguin:
                return _AnimalPrefab.AddComponent<EnemyPenguin>();
            case EnemyType.Wolf: 
                return _AnimalPrefab.AddComponent<EnemyWolf>();
            case EnemyType.Owl:
                return _AnimalPrefab.AddComponent<EnemyOwl>();
            case EnemyType.Seal: 
                return _AnimalPrefab.AddComponent<EnemySeal>();
            default: 
                return _AnimalPrefab.AddComponent<EnemySeal>();
        }
    }

    private void SetAnimalAnimator(EnemyType _Type, GameObject _AnimalPrefab, string _RuntimePath)
    {
        RuntimeAnimatorController Controller = Resources.Load<RuntimeAnimatorController>(_RuntimePath);
        if (Controller != null)
        {
            Animator Animator = _AnimalPrefab.GetComponentInChildren<Animator>();
            if (Animator != null)
            {
                Animator.runtimeAnimatorController = Controller;
            }
            else
            {
                Debug.Log("Animator component missing on the prefab");
            }
        }
        else
        {
            Debug.Log("Whoopsie, no RuntimeController on this fkn prefab ahh");
        }
    }

    private EnemyFlyWeight GetFlyweight(EnemyType _Type)
    {
        GameObject Temp = new GameObject("TempEnemyData");
        //Making a temp gameobject so that the compiler doesn't complain
        //Can't use "new" on Monobehaviours
        IEnemies EnemyData = AttachAnimalScript(_Type, Temp);
        RuntimeAnimatorController Controller = Resources.Load<RuntimeAnimatorController>(EnemyData.RunTimeController);

        if (Controller == null)
        {
            Debug.Log($"Animator controller not found at path: {EnemyData.RunTimeController}");
        }

        EnemyFlyWeight Flyweight = new EnemyFlyWeight(Controller, EnemyData.MoveSpeed, EnemyData.HitPoints, EnemyData.Cost);
        //Destroying our temp object
        Destroy(Temp);
        return Flyweight;
    }

    public int GetCost(Factory_Enemies.EnemyType _Type)
    {
        GameObject Temp = new GameObject("TempEnemyCost");
        IEnemies EnemyData = AttachAnimalScript(_Type, Temp);
        int Cost = EnemyData.Cost;
        Destroy(Temp);
        return Cost;
    }
}
