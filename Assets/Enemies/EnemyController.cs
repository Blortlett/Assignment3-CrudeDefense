using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyFlyWeight mpFlyweight = null;

    private Vector2 mMovementDirection;

    void Start()
    {
        Vector2 currentPosition = transform.position;
        mMovementDirection = new Vector2(-currentPosition.x, 0).normalized;
    }

    void Update()
    {
        transform.Translate(mMovementDirection * mpFlyweight.mfMoveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FactoryComponent"))
        {
            Debug.Log("Animal is attacking component!");
        }
    }
    
    public void Initialize(EnemyFlyWeight flyweight)
    {
        mpFlyweight = flyweight;
    }
}

public class EnemyFlyWeight
{
    public RuntimeAnimatorController pAnmatorController;
    public float mfMoveSpeed;
    public float mfHitPoints;

    public EnemyFlyWeight(RuntimeAnimatorController _AnimatorController, float _MoveSpeed, float _HitPoints)
    {
        this.pAnmatorController = _AnimatorController;
        this.mfMoveSpeed = _MoveSpeed;
        this.mfHitPoints = _HitPoints;
    }
}

public static class EnemyFlyweightFactory
{
    public static EnemyFlyWeight PolarBear = new EnemyFlyWeight(Resources.Load<RuntimeAnimatorController>("AnimController_Animal_PolarBear"), 2f, 70f);
    public static EnemyFlyWeight Penguin = new EnemyFlyWeight(Resources.Load<RuntimeAnimatorController>("AnimController_Animal_Penguin"), 2f, 10f);
    public static EnemyFlyWeight Wolf = new EnemyFlyWeight(Resources.Load<RuntimeAnimatorController>("AnimController_Animal_Owl"), 2f, 40f);
    public static EnemyFlyWeight Owl = new EnemyFlyWeight(Resources.Load<RuntimeAnimatorController>("AnimController_Animal_Wolf"), 5f, 10f);
}