using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteractable : MonoBehaviour,  IInteractable
{
    [SerializeField] private SpriteRenderer mButtonSprite;
    [SerializeField] private Color mUnpressedButtonColor;
    [SerializeField] private Color mPressedButtonColor;

    private float mButtonPressedTimer; // Countdown timer
    private const float mButtonPressedTimeMax = .5f; // Time to reset to

    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        if (mButtonPressedTimer <= 0f)
        {
            Debug.Log("Button pressed :)");
            mButtonPressedTimer = mButtonPressedTimeMax;
            mButtonSprite.color = mPressedButtonColor;
        }
    }

    void Start()
    {
        
    }
    void Update()
    {
        if (mButtonPressedTimer > 0f)
        {
            mButtonPressedTimer -= Time.deltaTime;
        }
        else
        {
            // Reset color
            mButtonSprite.color = mUnpressedButtonColor;
        }
    }
}
