using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateScript : MonoBehaviour
{
    [SerializeField] Transform mPressurePadGraphic;

    Collider2D mPressurePlateTrigger;
    bool mIsPressed;
    [SerializeField] Transform mButtonUnpressedPosition;
    [SerializeField] Transform mButtonPressedPosition;

    [SerializeField] GameObject mPressureplateableObject;
    IPressurePlateable mTargetTriggerableScript;

    // Start is called before the first frame update
    void Start()
    {
        // Get collider for player to jump on to activate pressure plate
        mPressurePlateTrigger = gameObject.GetComponent<Collider2D>();
        // Get script to activate on Pressureplate pushed
        mTargetTriggerableScript = mPressureplateableObject.GetComponent<IPressurePlateable>();
        // Throw warning if script not found
        if (mTargetTriggerableScript == null)
        {
            Debug.LogError("No IPressurePlateable script attatched to pressure plate");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (mIsPressed)
        {
            // Set graphic to lowered position
            mPressurePadGraphic.position = mButtonPressedPosition.position;
        }
        else // not pressed
        {
            // Set graphic to raised position
            mPressurePadGraphic.position = mButtonUnpressedPosition.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // only activate for player
        {
            mIsPressed = true;
            // Trigger target script
            mTargetTriggerableScript.PressurePlatePushed();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // only activate for player
        {
            mIsPressed = false;
        }
    }
}