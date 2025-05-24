using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValveWheel : MonoBehaviour, IInteractable
{
    // How much the turnpipe is turned
    float mTurnpipeCurrentOpenAmount = 0f;
    float mTurnpipeOpenMax = 100f;

    int TurnpipeTurnDirection = 0; // -1 for closing direction. +1 for opening direction
    bool TurnpipeOpen = false;


    // Prefab's own things
    [SerializeField] private Animator mAnimator;

    // Object's outside the prefab
    [SerializeField] private GameObject mToggleableObject;
    private IButtonable mToggleableScript;

    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        if (mAnimator.speed == 0f)
        {
            mAnimator.speed = 1f;
            
        } else
        {
            mAnimator.speed = 0f;
        }

        // Make the Valve wheel do something
        if (mToggleableScript != null)
        {
            mToggleableScript.OnButtonPress();
        }
        else Debug.Log("No Toggleable present"); // Error detection
    }

    // Start is called before the first frame update
    void Start()
    {
        mToggleableScript = mToggleableObject.GetComponent<IButtonable>();
;       mAnimator.speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
