using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class BarrelSpawner : MonoBehaviour, IPickupable, IButtonable
{
    [SerializeField] GameObject mBarrelSpawnObject;
    [SerializeField] GameObject mBarrelSprite;
    [SerializeField] Transform mTweenPoint1;
    [SerializeField] Transform mTweenPoint2;
    private float mLerpTime = 0f;
    private float mAnimationDownTimer = 0f;

    bool mBarrelReachedPoint1 = false;
    bool mBarrelReachedPoint2 = false;

    private GameObject mpAnimatedBarrelSprite = null;

    private bool mIsBarrelReadyForPickup = false;
    private bool mIsMachineFull = false;



    void Start()
    {
    }

    void FixedUpdate()
    {
        // Below is code exclusively for Barrel spawn animation.
        if (mpAnimatedBarrelSprite == null)// If barrel sprite doesnt exist, then return... don't bother animating
            return;

        Debug.Log("LerpTime: " + mLerpTime);

        // Lerp Barrel to first point (Barrel falls out of machine)
        if (!mBarrelReachedPoint1)
        {
            mLerpTime += Time.deltaTime / 2f; // Increase lerp time for calculation below
            float TweenYPos = Mathf.Lerp(mpAnimatedBarrelSprite.transform.position.y, mTweenPoint1.position.y, mLerpTime); // Use lerp to calculate barrel Y position
            mpAnimatedBarrelSprite.transform.position = new Vector3(mBarrelSprite.transform.position.x, TweenYPos, 0f); // Apply lerp
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
            float TweenXPos = Mathf.Lerp(mpAnimatedBarrelSprite.transform.position.x, mTweenPoint2.position.x, mLerpTime); // Use lerp to calculate barrel X position
            mpAnimatedBarrelSprite.transform.position = new Vector3(TweenXPos, mpAnimatedBarrelSprite.transform.position.y, 0f); // Apply lerp
            if (mLerpTime >= 1f)
            {
                // Reset animator parameters
                mBarrelReachedPoint2 = true; // set track animation bool to complete so we don't repeat above code
                mLerpTime = 0f; // reset animation timer
                mIsBarrelReadyForPickup = true; // This bool lets pickup barrel code get executed
            }
        }
    }

    public bool CanPickup()
    {
        if (mIsBarrelReadyForPickup)
        {
            return true;
        }
        else
            return false;
    }

    public GameObject Pickup()
    {
        Destroy(mpAnimatedBarrelSprite);
        mpAnimatedBarrelSprite = null;
        ResetMachine();
        GameObject spawnedBarrel = Instantiate(mBarrelSpawnObject, new Vector3(mBarrelSprite.transform.position.x, -2.716174f, mBarrelSprite.transform.position.z), mBarrelSprite.transform.rotation);
        return spawnedBarrel;
    }

    public void PutDown()
    {
        throw new System.NotImplementedException();
    }

    public float GetOriginalYPosition()
    {
        throw new System.NotImplementedException();
    }

    public void OnButtonPress()
    {
        Debug.Log("Barrel machine heard button press");

        if (mIsMachineFull)
            return; // If machine already occupied - Do nothing

        // Create barrel sprite to animate the machine with
        mpAnimatedBarrelSprite = Instantiate(mBarrelSprite, mBarrelSprite.transform.position, mBarrelSprite.transform.rotation);
        mIsMachineFull = true;
    }

    private void ResetMachine()
    {
        mBarrelReachedPoint1 = false;
        mBarrelReachedPoint2 = false;
        mIsMachineFull = false;
        mIsBarrelReadyForPickup = false;
        mLerpTime = 0f;
    }
}
