using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPolarBear : MonoBehaviour, IEnemies
{
    public string Name { get; set; } = "Polar Bear";
    public float MoveSpeed { get; set; } = 1.5f;
    public float HitPoints { get; set; } = 70.0f;
    public int Cost { get; set; } = 5;
    public string RunTimeController { get; set; } = "Polarbear/AnimController_Animal_PolarBear";

    private Enemy mEnemyController;
    private Animator mAnimator;


    private void Start()
    {
        mEnemyController = GetComponent<Enemy>();
        mAnimator = mEnemyController.GetComponentInChildren<Animator>();

        // Debug Checks
        if (mAnimator != null)
            Debug.Log("Polar bear animator present");
        else
            Debug.Log("Polar bear has no animator :(");

        if (mEnemyController != null)
            Debug.Log("Polar bear has a controller");
        else
            Debug.Log("Polar bear has no controller :(");

        //mEnemyController.mIsTrapped = true; // using this to get old mate to go to take a nap
    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PressurePlate"))
        {
            mAnimator.SetBool("IsSleeping", true);
            mEnemyController.mIsTrapped = true; // using this to get old mate to go to take a nap
        }
    }
}