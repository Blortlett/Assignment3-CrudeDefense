using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour, IAcceptBarrels
{
    [SerializeField] private BarrelHolder mBarrelHolderScr;


    // Boat Bobbing variables
    [SerializeField] private float mBobDistance = .05f;
    [SerializeField] private float mBobTimerMax = .8f;
    private float mBobTimer = .8f;
    private bool mHasBobbed = false;

    void Start()
    {
        mBobTimer = mBobTimerMax;
    }

    void Update()
    {
        // Move boat up n down thru code
        BobBoat();
    }


    void BobBoat()
    {
        // countdown bob timer
        mBobTimer -= Time.deltaTime;
        // Check bobtimer
        if (mBobTimer <= 0f)
        {
            // reset bobtimer
            mBobTimer = mBobTimerMax;

            // create bob move vector
            Vector3 BobbingMoveVector = new Vector3(0, mBobDistance, 0);

            // Toggle up/down direction of bob
            if (mHasBobbed)
            {
                // Bob boat
                transform.position += BobbingMoveVector;
            } else
            {
                // Bob boat
                transform.position -= BobbingMoveVector;
            }
            // revert bob direction bool for next bob
            mHasBobbed = !mHasBobbed;
        }
    }

    public void OnRecieveBarrel()
    {
        Debug.Log("Barrel Accepted :)");
    }
}
