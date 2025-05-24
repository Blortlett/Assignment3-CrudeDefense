using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateScript : MonoBehaviour
{
    [SerializeField] Transform mPressurePadGraphic;

    Collider2D mPressurePlateTrigger;
    bool mIsPressed;
    Vector2 mButtonUnpressedPosition = new Vector2(0, 0.2539f);
    Vector2 mButtonPressedPosition = new Vector2(0, 0);

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
            mPressurePadGraphic.localPosition = mButtonPressedPosition;
        }
        else // not pressed
        {
            mPressurePadGraphic.localPosition = mButtonUnpressedPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            mIsPressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        mIsPressed = false;
    }
}