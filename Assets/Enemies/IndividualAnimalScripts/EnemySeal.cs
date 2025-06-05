using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySeal : MonoBehaviour, IEnemies
{
    public string Name { get; set; } = "Seal";
    public float MoveSpeed { get; set; } = 0.7f;
    public float HitPoints { get; set; } = 40.0f;
    public int Cost { get; set; } = 1;
    public string RunTimeController { get; set; } = "Seal/AnimController_Animal_Seal";

    // Store enemy controllers
    private Enemy mEnemyController;
    private Animator mAnimator;
    private BoxCollider2D mCollider;

    private void Start()
    {
        // Set Tag as Seal
        gameObject.tag = "Seal";

        // Get Seal components
        mEnemyController = GetComponent<Enemy>();
        mAnimator = mEnemyController.GetComponentInChildren<Animator>();
        mCollider = gameObject.GetComponent<BoxCollider2D>();

        // Resize Collider
        Vector2 newColliderSize = new Vector2(mCollider.size.x + .6f, mCollider.size.y + 1.6f);
        mCollider.size = newColliderSize;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SealStopPoint"))
        {
            // Make Penguin stand still
            mEnemyController.mIsTrapped = true;
            mAnimator.speed = 0f;
            // Make Valve wheel start closing when penguin interacts
            mCollider.isTrigger = false;
        }
    }

    public void Die()
    {
        Debug.Log("HIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIII");
        Destroy(this.gameObject);
    }
}