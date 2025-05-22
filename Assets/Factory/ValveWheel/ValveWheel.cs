using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValveWheel : MonoBehaviour, IInteractable
{
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
        else Debug.Log("No Toggleable present");
    }

    // Start is called before the first frame update
    void Start()
    {
        mToggleableScript = mToggleableObject.GetComponent<IButtonable>();
;        mAnimator.speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
