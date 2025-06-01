using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour, IEnemy
{
    [SerializeField] private Animator mAnimator;
    [SerializeField] private SpriteRenderer mSpriteRenderer;

    private EnemyFlyWeight mpFlyweight = null;

    private Vector2 mMovementDirection;

    private Vector3 mFaceLeftScale = new Vector3(1,1,1);
    private Vector3 mFaceRightScale = new Vector3(-1,1,1);



    bool mIsTrapped = false;

    //void Start()
    //{
    //    Vector2 currentPosition = transform.position;
    //    mMovementDirection = new Vector2(-currentPosition.x, 0).normalized;

    //    mAnimator.runtimeAnimatorController = mpFlyweight.pAnmatorController;

    //    SetFacingDirection();
    //}

    void Start()
    {
        Vector2 currentPosition = transform.position;
        mMovementDirection = new Vector2(-currentPosition.x, 0).normalized;

        if (mAnimator == null)
        {
            Debug.LogError("mAnimator is null in EnemyController.Start()");
        }
        if (mpFlyweight == null)
        {
            Debug.LogError("mpFlyweight is null in EnemyController.Start()");
        }
        else if (mpFlyweight.pAnmatorController == null)
        {
            Debug.LogError("mpFlyweight.pAnmatorController is null");
        }
        else
        {
            mAnimator.runtimeAnimatorController = mpFlyweight.pAnmatorController;
        }

        SetFacingDirection();
    }


    void Update()
    {
        if (!mIsTrapped)
        {
            transform.Translate(mMovementDirection * mpFlyweight.mfMoveSpeed * Time.deltaTime);
        }
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

        if (mpFlyweight.pAnmatorController != null)
        {
            mAnimator.runtimeAnimatorController = mpFlyweight.pAnmatorController;
        }
        else
        {
            Debug.Log("No animator controller!");
        }
    }

    private void SetFacingDirection()
    {
        if (mMovementDirection.x > 0f)
        {
            mSpriteRenderer.transform.localScale = mFaceRightScale;
        }
        else
        {
            mSpriteRenderer.transform.localScale = mFaceLeftScale;
        }
    }

    public void Trap()
    {
        mIsTrapped = true;
    }

    public void BlowUp()
    {
        this.GetComponentInChildren<Explosion>().gameObject.SetActive(true);
        this.GetComponentInChildren<Explosion>().Explode();
        this.GetComponent<Explosion>().Explode();
    }
}

public class EnemyFlyWeight
{
    public RuntimeAnimatorController pAnmatorController;
    public float mfMoveSpeed;
    public float mfHitPoints;
    public int miCost;

    public EnemyFlyWeight(RuntimeAnimatorController _AnimatorController, float _MoveSpeed, float _HitPoints, int _Cost)
    {
        this.pAnmatorController = _AnimatorController;
        this.mfMoveSpeed = _MoveSpeed;
        this.mfHitPoints = _HitPoints;
        this.miCost = _Cost;
    }
}

public static class EnemyFlyweightFactory
{
    public static EnemyFlyWeight PolarBear = new EnemyFlyWeight(Resources.Load<RuntimeAnimatorController>("Polarbear/AnimController_Animal_PolarBear"), 1.5f, 70f, 5);
    public static EnemyFlyWeight Penguin = new EnemyFlyWeight(Resources.Load<RuntimeAnimatorController>("Penguin/AnimController_Animal_Penguin"), 2f, 10f, 4);
    public static EnemyFlyWeight Wolf = new EnemyFlyWeight(Resources.Load<RuntimeAnimatorController>("Wolf/AnimController_Animal_Wolf"), 3f, 10f, 3);
    public static EnemyFlyWeight Owl = new EnemyFlyWeight(Resources.Load<RuntimeAnimatorController>("Owl/AnimController_Animal_Owl"), 5f, 40f, 2);
    public static EnemyFlyWeight Seal = new EnemyFlyWeight(Resources.Load<RuntimeAnimatorController>("Seal/AnimController_Animal_Seal"), .7f, 40f, 1);
}