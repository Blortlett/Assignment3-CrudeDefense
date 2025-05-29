using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatSystem: MonoBehaviour
{
    // Boat GameObject
    [SerializeField] GameObject mBoatObject;

    // Boat full of barrel tracker -- boat will wait @ wait point until this is true
    private bool mBoatIsFull = false;
    private bool mBoatTripComplete = false;

    // -= Animations =-
    // Tween animation points
    [SerializeField] private Transform TweenPointBoatStart;
    [SerializeField] private Transform TweenPointBoatWaitPoint;
    [SerializeField] private Transform TweenPointBoatEndPoint;
    // Lerp timer
    private float mLerpTime = 0f;
    // Track boat animation points
    bool mBoatReachedPoint1 = false;
    bool mBoatReachedWaitPoint = false;
    bool mBoatReachedPoint3 = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (!mBoatReachedWaitPoint)
        {
            LerpToWaitPoint();
        }
    }

    public void OnBoatFull()
    {
        mBoatIsFull = true;
    }

    private void LerpToWaitPoint()
    {
        Debug.Log("Lerpin to WaitPoint");
        mLerpTime += Time.deltaTime / 5f; // Increase lerp time for calculation below
        float TweenXPos = Mathf.Lerp(mBoatObject.transform.position.y, TweenPointBoatWaitPoint.position.y, mLerpTime); // Use lerp to calculate barrel Y position
        mBoatObject.transform.position = new Vector3(TweenXPos, mBoatObject.transform.position.y, 0f); // Apply lerp
        if (mLerpTime >= 1f)
        {
            // Set / Reset animator parameters
            mBoatReachedWaitPoint = true; // set track animation bool to complete so we don't repeat above code
            mLerpTime = 0f; // reset animation timer
            Debug.Log("Lerp complete");
        }
    }
}
