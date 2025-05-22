using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteractable : MonoBehaviour,  IInteractable
{
    [SerializeField] private GameObject mToggleableObject;
    private IButtonable mToggleableScript;

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
        // Button animation code
        if (mButtonPressedTimer <= 0f)
        {
            Debug.Log("Button pressed :)");
            mButtonPressedTimer = mButtonPressedTimeMax;
            mButtonSprite.color = mPressedButtonColor;
        }

        // Make the button press do something
        if (mToggleableScript != null)
        {
            mToggleableScript.OnButtonPress();
        }
    }

    void Start()
    {
        if (mToggleableObject != null)
        {
            mToggleableScript = mToggleableObject.GetComponent<IButtonable>();
        }
    }

    void FixedUpdate()
    {
        // Button Animation
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
