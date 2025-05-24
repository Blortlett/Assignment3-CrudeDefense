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

    // Start is called before the first frame update
    void Start()
    {
        mPressurePlateTrigger = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mIsPressed)
        {
            mPressurePadGraphic.position = mButtonPressedPosition.position;
        }
        else // not pressed
        {
            mPressurePadGraphic.position = mButtonUnpressedPosition.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Pressurepad Triggered");
            mIsPressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        mIsPressed = false;
    }
}