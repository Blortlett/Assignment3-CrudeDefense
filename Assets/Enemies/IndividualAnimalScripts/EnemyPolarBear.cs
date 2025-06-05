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

    // timer to ensure polar bear sleeps ontop of button
    float mMaxTimeToSleep = .5f;
    float mTimeToSleep;
    bool mShouldSleep = false;

    private void Start()
    {
        // Set Tag as polarbear
        gameObject.tag = "PolarBear";

        // Setup timer for sleeping center of button
        mTimeToSleep = mMaxTimeToSleep;
        // Get polar bear controller and animator
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
    }

    private void Update()
    {
        if (!mShouldSleep) return;
        mTimeToSleep -= Time.deltaTime;
        if (mTimeToSleep <= 0f)
        {
            mAnimator.SetBool("IsSleeping", true);
            mEnemyController.mIsTrapped = true; // using this to get old mate to go to take a nap
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PressurePlate"))
        {
            mShouldSleep = true;
        }
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}