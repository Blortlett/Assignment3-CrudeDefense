using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelSpawner : MonoBehaviour
{
    [SerializeField] GameObject mBarrelSpawnPoint;
    [SerializeField] Transform mTweenPoint1;
    [SerializeField] Transform mTweenPoint2;
    private float mLerpTime = 0f;
    private float mAnimationDownTimer = 0f;

    bool mBarrelReachedPoint1 = false;
    bool mBarrelReachedPoint2 = false;

    private GameObject mpSpawnedBarrel = null;



    void Start()
    {
        mpSpawnedBarrel = Instantiate(mBarrelSpawnPoint, mBarrelSpawnPoint.transform.position, mBarrelSpawnPoint.transform.rotation);
    }

    void FixedUpdate()
    {
        Debug.Log("LerpTime: " + mLerpTime);
        // Lerp Barrel to first point (Barrel falls out of machine)
        if (!mBarrelReachedPoint1)
        {
            mLerpTime += Time.deltaTime / 2f; // Increase lerp time for calculation below
            float TweenYPos = Mathf.Lerp(mpSpawnedBarrel.transform.position.y, mTweenPoint1.position.y, mLerpTime); // Use lerp to calculate barrel Y position
            mpSpawnedBarrel.transform.position = new Vector3(mBarrelSpawnPoint.transform.position.x, TweenYPos, 0f); // Apply lerp
            if (mLerpTime >= 1f)
            {
                // Reset animator parameters
                mBarrelReachedPoint1 = true; // set track animation bool to complete so we don't repeat above code
                mLerpTime = 0f; // reset animation timer
            }
        }

        // Lerp Barrel to second point (Push barrel along the machine)
        if (mBarrelReachedPoint1 && !mBarrelReachedPoint2)
        {
            mLerpTime += Time.deltaTime; // Increase lerp time for calculation below
            float TweenXPos = Mathf.Lerp(mpSpawnedBarrel.transform.position.x, mTweenPoint2.position.x, mLerpTime); // Use lerp to calculate barrel X position
            mpSpawnedBarrel.transform.position = new Vector3(TweenXPos, mpSpawnedBarrel.transform.position.y , 0f); // Apply lerp
            if (mLerpTime >= 1f)
            {
                // Reset animator parameters
                mBarrelReachedPoint2 = true; // set track animation bool to complete so we don't repeat above code
                mLerpTime = 0f; // reset animation timer
            }
        }
    }
}
