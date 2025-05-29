using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatSystem: MonoBehaviour
{
    // Boat GameObject
    [SerializeField] GameObject mBoatObject;
    [SerializeField] BarrelHolder mBoatBarrelHolderScr;

    // Boat inventory tracker -- boat will wait @ wait point until this is true
    private bool mBoatIsFull = false;

    // timer for how long boat should wait @ EndPoint before returning to StartPoint
    [SerializeField] float mBoatRespawnMaxTimer = 5f;
    float mBoatRespawnTimer = 5f;

    // -= Animations =-
    // Tween animation points
    [SerializeField] private Transform TweenPointBoatStart;
    [SerializeField] private Transform TweenPointBoatWaitPoint;
    [SerializeField] private Transform TweenPointBoatEndPoint;
    // Lerp timer
    private float mLerpTime = 0f;
    // Track boat animation points
    bool mBoatReachedWaitPoint = true;
    bool mBoatReachedEndPoint = false;

    void Start()
    {
        // set respawn wait timer to max time
        mBoatRespawnTimer = mBoatRespawnMaxTimer;
        // Set boat to start position
        mBoatObject.transform.position = TweenPointBoatWaitPoint.position;
    }

    void Update()
    {
        if (!mBoatReachedWaitPoint)
        {
            LerpToWaitPoint();
        }
        else if (mBoatIsFull && !mBoatReachedEndPoint)
        {
            LerpToEndPoint();
        }
        else if (mBoatReachedEndPoint)
        {
            ReturnToStartPoint();
        }
    }

    public void OnBoatFull()
    {
        mBoatIsFull = true;
    }

    private void LerpToWaitPoint()
    {
        float lerpDuration = 5.0f; // Changes lerp time
        mLerpTime += Time.deltaTime / lerpDuration; // Calculate lerp time
        // Calculate new position with lerp
        float TweenXPos = Mathf.Lerp(TweenPointBoatStart.position.x, TweenPointBoatWaitPoint.position.x, mLerpTime); // Use lerp to calculate barrel Y position
        // Apply new position
        mBoatObject.transform.position = new Vector3(TweenXPos, mBoatObject.transform.position.y, 0f); // Apply lerp

        // If lerp complete
        if (mLerpTime >= 1f)
        {
            // Set / Reset animator parameters
            mBoatReachedWaitPoint = true; // set track animation bool to complete so we don't repeat above code
            mLerpTime = 0f; // reset animation timer
        }
    }

    private void LerpToEndPoint()
    {
        float lerpDuration = 10.0f; // Changes lerp time
        mLerpTime += Time.deltaTime / lerpDuration; // Calculate lerp time
        // Calculate new position with lerp
        float TweenXPos = Mathf.Lerp(TweenPointBoatWaitPoint.position.x, TweenPointBoatEndPoint.position.x, mLerpTime); // Use lerp to calculate barrel Y position
        // Apply new position
        mBoatObject.transform.position = new Vector3(TweenXPos, mBoatObject.transform.position.y, 0f); // Apply lerp

        // If lerp complete
        if (mLerpTime >= 1f)
        {
            // Set / Reset animator parameters
            mBoatReachedEndPoint = true; // set track animation bool to complete so we don't repeat above code
            mLerpTime = 0f; // reset animation timer
        }
    }

    private void ReturnToStartPoint()
    {
        // Countdown respawn timer
        mBoatRespawnTimer -= Time.deltaTime;
        if (mBoatRespawnTimer < 0f)
        {
            // Respawn boat
            mBoatObject.transform.position = TweenPointBoatStart.position;
            ResetAnimationVariables();
            // Empty boat barrels before respawning
            mBoatBarrelHolderScr.EmptyBoat();
            Globals.instance.BarrelSold();
        }
    }

    private void ResetAnimationVariables()
    {
        mBoatIsFull = false;
        mBoatReachedWaitPoint = false;
        mBoatReachedEndPoint = false;
        mBoatRespawnTimer = 0f;
        mLerpTime = 0f;
    }
}
