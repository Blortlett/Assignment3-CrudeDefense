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

    void Start()
    {
        Vector2 currentPosition = transform.position;
        mMovementDirection = new Vector2(-currentPosition.x, 0).normalized;

        mAnimator.runtimeAnimatorController = mpFlyweight.pAnmatorController;
        mSpriteRenderer.sprite = mpFlyweight.pSpriteSheet;

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

        if (mpFlyweight.pSpriteSheet != null)
        {
            mSpriteRenderer.sprite = mpFlyweight.pSpriteSheet;
        }
        else
        {
            Debug.Log("No sprite sheet!");
        }

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
            transform.localScale = mFaceRightScale;
        }
        else
        {
            transform.localScale = mFaceLeftScale;
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
    public Sprite pSpriteSheet;
    public float mfMoveSpeed;
    public float mfHitPoints;

    public EnemyFlyWeight(RuntimeAnimatorController _AnimatorController, Sprite _SpriteSheet, float _MoveSpeed, float _HitPoints)
    {
        this.pAnmatorController = _AnimatorController;
        this.pSpriteSheet = _SpriteSheet;
        this.mfMoveSpeed = _MoveSpeed;
        this.mfHitPoints = _HitPoints;
    }
}

public static class EnemyFlyweightFactory
{
    public static EnemyFlyWeight PolarBear = new EnemyFlyWeight(Resources.Load<RuntimeAnimatorController>("Polarbear/AnimController_Animal_PolarBear"), Resources.Load<Sprite>("Polarbear/Polarbear-Sheet"), 1.5f, 70f);
    public static EnemyFlyWeight Penguin = new EnemyFlyWeight(Resources.Load<RuntimeAnimatorController>("Penguin/AnimController_Animal_Penguin"), Resources.Load<Sprite>("Penguin/Penguin-Sheet"), 2f, 10f);
    public static EnemyFlyWeight Wolf = new EnemyFlyWeight(Resources.Load<RuntimeAnimatorController>("Wolf/AnimController_Animal_Wolf"), Resources.Load<Sprite>("Wolf/Wolf-Sheet"), 3f, 10f);
    public static EnemyFlyWeight Owl = new EnemyFlyWeight(Resources.Load<RuntimeAnimatorController>("Owl/AnimController_Animal_Owl"), Resources.Load<Sprite>("Owl/Owl-Sheet"), 5f, 40f);
}