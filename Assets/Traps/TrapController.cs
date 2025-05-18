using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour, IPickupable
{
    public Animator mAnimator;
    private const string TriggerTrapName = "TrapTriggered";

    const bool mCanPickup = true;
    private float mOriginalYPosition;

    public bool CanPickup()
    {
        return mCanPickup;
    }

    public void Pickup()
    {
        mAnimator.SetBool(TriggerTrapName, true);
    }

    public void PutDown()
    {
        mAnimator.SetBool(TriggerTrapName, false);
    }

    public float GetOriginalYPosition()
    {
        return mOriginalYPosition;
    }

    private void Awake()
    {
        mOriginalYPosition = transform.position.y;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
