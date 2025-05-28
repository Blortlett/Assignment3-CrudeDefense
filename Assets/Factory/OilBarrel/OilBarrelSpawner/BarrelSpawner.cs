using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BarrelSpawner : MonoBehaviour, IPickupable, IButtonable, IPressurePlateable
{
    // Real barrel to spawn
    [SerializeField] GameObject mBarrelSpawnObject;
    // Barrel prop graphic
    [SerializeField] GameObject mBarrelSprite;
    // Points to tween towards
    [SerializeField] Transform mTweenPoint1;
    [SerializeField] Transform mTweenPoint2;
    [SerializeField] Transform mTweenPoint3;
    // Lerp timer
    private float mLerpTime = 0f;
    private float mAnimationDownTimer = 0f; // wait time before next lerp animation plays
    // Track barrel progress through machine
    bool mBarrelReachedPoint1 = false;
    bool mBarrelReachedPoint2 = false;
    bool mBarrelReachedPoint3 = false;
    // Gfx for current barrel in machine
    private GameObject mpAnimatedBarrelSprite = null;
    // Machine got a barrel in it? variables
    private bool mIsBarrelReadyForPickup = false;
    private bool mIsMachineFull = false;
    // Barrel fill variables
    private int mBarrelCurrentFullness = 0;
    private const int mBarrelMaxFullness = 3;
    private bool mBarrelFull = false;
    // Fill barrel particle feedback
    [SerializeField] private ParticleSystem mFillBarrelParticles;

    // Oil Silo to take from
    [SerializeField] private float mAmountOilToTake = 7f;
    [SerializeField] private RefinedOilSilo mOilSiloScript;


    void Start()
    {
    }

    void FixedUpdate()
    {
        // Below is code exclusively for Barrel spawn animation.
        if (mpAnimatedBarrelSprite == null)// If barrel sprite doesnt exist, then return... don't bother animating
            return;

        // Lerp Barrel to first point (Barrel falls out of machine)
        if (!mBarrelReachedPoint1)
        {
            LerpToPoint1();
        }
        // Lerp Barrel to second point (Push barrel along to fill spot)
        else if (mBarrelReachedPoint1 && !mBarrelReachedPoint2)
        {
            LerpToPoint2();
        }
        // Lerp Barrel to third point (Push barrel along to pickup point) // Barrel should also be full
        else if (mBarrelReachedPoint2 && !mBarrelReachedPoint3 && mBarrelFull)
        {
            LerpToPoint3();
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
        // Check if machine already has a barrel in it
        if (mIsMachineFull)
            return; // If machine already occupied - Do nothing

        // Create barrel sprite to animate the machine with
        mpAnimatedBarrelSprite = Instantiate(mBarrelSprite, mBarrelSprite.transform.position, mBarrelSprite.transform.rotation);
        mIsMachineFull = true;
    }

    private void ResetMachine()
    {
        // Create room for new barrel to enter machine
        mIsMachineFull = false;
        // Reset barrel lerp points to allow new barrel to roll through machine
        mBarrelReachedPoint1 = false;
        mBarrelReachedPoint2 = false;
        mBarrelReachedPoint3 = false;
        // Barrel no longer ready for spawn and pickup
        mIsBarrelReadyForPickup = false;
        // Reset lerp time to make absolute sure
        mLerpTime = 0f;
        // New barrel should be empty at start
        mBarrelCurrentFullness = 0;
        mBarrelFull = false;
    }

    public void PressurePlatePushed()
    {
        if (mOilSiloScript == null) // Error check oil silo script is attatched
        {
            Debug.Log("No oil silo script attatched, returning from function");
            return;
        }
        if (mOilSiloScript.GetOilLevel() <= mAmountOilToTake) return; // Do not fill barrel if not enough oil

        mOilSiloScript.RemoveOil(mAmountOilToTake);
        mFillBarrelParticles.Play();

        // Check barrel is in ready to fill position
        if (mBarrelReachedPoint2 && !mBarrelReachedPoint3)
        {
            // Fill barrel with oil
            mBarrelCurrentFullness++;
        }

        // Check if barrel is full
        if (mBarrelCurrentFullness >= mBarrelMaxFullness)
        {
            mBarrelFull = true;
        }
    }

    private void LerpToPoint1()
    {
        mLerpTime += Time.deltaTime / 2f; // Increase lerp time for calculation below
        float TweenYPos = Mathf.Lerp(mpAnimatedBarrelSprite.transform.position.y, mTweenPoint1.position.y, mLerpTime); // Use lerp to calculate barrel Y position
        mpAnimatedBarrelSprite.transform.position = new Vector3(mBarrelSprite.transform.position.x, TweenYPos, 0f); // Apply lerp
        if (mLerpTime >= 1f)
        {
            // Set / Reset animator parameters
            mBarrelReachedPoint1 = true; // set track animation bool to complete so we don't repeat above code
            mLerpTime = 0f; // reset animation timer
        }
    }

    private void LerpToPoint2()
    {
        mLerpTime += Time.deltaTime; // Increase lerp time for calculation below
        float TweenXPos = Mathf.Lerp(mpAnimatedBarrelSprite.transform.position.x, mTweenPoint2.position.x, mLerpTime); // Use lerp to calculate barrel X position
        mpAnimatedBarrelSprite.transform.position = new Vector3(TweenXPos, mpAnimatedBarrelSprite.transform.position.y, 0f); // Apply lerp
        if (mLerpTime >= 1f)
        {
            // Set / Reset animator parameters
            mBarrelReachedPoint2 = true; // set track animation bool to complete so we don't repeat above code
            mLerpTime = 0f; // reset animation timer
        }
    }

    private void LerpToPoint3()
    {
        mLerpTime += Time.deltaTime; // Increase lerp time for calculation below
        float TweenXPos = Mathf.Lerp(mpAnimatedBarrelSprite.transform.position.x, mTweenPoint3.position.x, mLerpTime); // Use lerp to calculate barrel X position
        mpAnimatedBarrelSprite.transform.position = new Vector3(TweenXPos, mpAnimatedBarrelSprite.transform.position.y, 0f); // Apply lerp
        if (mLerpTime >= 1f)
        {
            // Set / Reset animator parameters
            mBarrelReachedPoint3 = true; // set track animation bool to complete so we don't repeat above code
            mLerpTime = 0f; // reset animation timer
            mIsBarrelReadyForPickup = true; // This bool lets pickup barrel code get executed
        }
    }
}
