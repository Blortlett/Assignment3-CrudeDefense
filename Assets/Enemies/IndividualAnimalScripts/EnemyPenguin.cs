using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPenguin : MonoBehaviour, IEnemies
{
    public string Name { get; set; } = "Penguin";
    public float MoveSpeed { get; set; } = 2.0f;
    public float HitPoints { get; set; } = 10.0f;
    public int Cost { get; set; } = 4;
    public string RunTimeController { get; set; } = "Penguin/AnimController_Animal_Penguin";

    // Store enemy controller
    private Enemy mEnemyController;
    private Animator mAnimator;

    private void Start()
    {
        // Set Tag as Penguin
        gameObject.tag = "Penguin";

        // Get Penguin controller
        mEnemyController = GetComponent<Enemy>();
        mAnimator = mEnemyController.GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ValveWheel"))
        {
            // Make Penguin stand still
            mEnemyController.mIsTrapped = true;
            mAnimator.speed = 0f;
            // Make Valve wheel start closing when penguin interacts
            collision.GetComponent<ValveWheel>().AnimalInteract();
        }
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}
