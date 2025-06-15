using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteractable : MonoBehaviour,  IInteractable
{
    [SerializeField] private GameObject mToggleableObject;
    private IButtonable mToggleableScript;

    [SerializeField] private Animator mButtonAnimator;


    private float mButtonPressedTimer; // Countdown timer
    private const float mButtonPressedTimeMax = .5f; // Time to reset to

    private void Awake()
    {
        
    }

    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        // Button animation code
        if (mButtonPressedTimer <= 0f)
        {
            mButtonPressedTimer = mButtonPressedTimeMax;
            mButtonAnimator.SetBool("IsPressed", true);
        }

        // Make the button press do something
        if (mToggleableScript != null)
        {
            mToggleableScript.OnButtonPress();

            // -= TUTORIAL =-
            TutorialScr.instance.TutorialBarrelButtonComplete();
            // -= TUTORIAL =-
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
            mButtonAnimator.SetBool("IsPressed", false);
        }
    }
}
