using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyOwl : MonoBehaviour, IEnemies
{
    public string Name { get; set; } = "Owl";
    public float MoveSpeed { get; set; } = 5.0f;
    public float HitPoints { get; set; } = 40.0f;
    public int Cost { get; set; } = 2;
    public string RunTimeController { get; set; } = "Owl/AnimController_Animal_Owl";

    // Store enemy controller
    private Enemy mEnemyController;

    private void Start()
    {
        // Set Tag as Owl
        gameObject.tag = "Owl";

        // Get Owl controller
        mEnemyController = GetComponent<Enemy>();

        // move owl up on start
        transform.position += new Vector3(0, 3.1f, 0);

        // Delete shadow // !!! THIS IS HORRIBLE AND WILL CAUSE PROBLEMS NO DOUBT !!!
        Destroy(transform.GetChild(0).gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ValveWheel"))
        {
            // Set wheel to closing when owl touches it
            collision.GetComponent<ValveWheel>().AnimalInteract();
        }
    }
}

