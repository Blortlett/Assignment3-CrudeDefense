using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValveWheel : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator mAnimator;

    public bool CanInteract()
    {
        return true;
    }

    public bool CanPickup()
    {
        return false;
    }

    public float GetOriginalYPosition()
    {
        return 0f;
    }

    public void Interact()
    {
        if (mAnimator.speed == 0f)
        {
            Debug.Log("Turning Valve Wheel");
            mAnimator.speed = 1f;
        } else
        {
            Debug.Log("Stop turning Valve Wheel");
            mAnimator.speed = 0f;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        mAnimator.speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
