using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelSoldAlert : MonoBehaviour
{
    // Opacity object
    private CanvasGroup mOpacityGroup;

    // Stay active timer
    [SerializeField] private float mMaxDisplayTime = 5f;
    private float mDisplayTime;

    // UI Is active bool
    bool mUIActive = false;
    bool mLerpInComplete = false;
    bool mDisplayTimeComplete = false;
    bool mLerpOutComplete = false;

    // LerpTimers
    private float mLerpInTimer = 0f;
    private float mLerpOutTimer = 0f;
    [SerializeField] private float mLerpDuration = 2f;

    private void Start()
    {
        // Get OpacityGroup
        mOpacityGroup = gameObject.GetComponent<CanvasGroup>();
        // Set timer
        mDisplayTime = mMaxDisplayTime;
        // Turn invisable on start
        mOpacityGroup.alpha = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(mUIActive)
        {
            if (!mLerpInComplete)
                LerpIn();
            else if (!mDisplayTimeComplete)
                DisplayForTime();
            else if (!mLerpOutComplete)
                LerpOut();
            else
                ResetAnimationVariables();
        }
    }

    private void LerpIn()
    {
        mLerpInTimer += Time.deltaTime / mLerpDuration; // Calculate lerp time
        // Calculate new position with lerp
        float TweenOpacity = Mathf.Lerp(0, 1, mLerpInTimer);
        // Set Opactiy
        mOpacityGroup.alpha = TweenOpacity;
        // If lerp complete
        if (mLerpInTimer >= 1f)
        {
            mOpacityGroup.alpha = 1f;
            mLerpInComplete = true; // set lerp complete
            mLerpInTimer = 0f;
        }
    }

    private void LerpOut()
    {
        mLerpOutTimer += Time.deltaTime / mLerpDuration; // Calculate lerp time
        // Calculate new position with lerp
        float TweenOpacity = Mathf.Lerp(1, 0, mLerpOutTimer);
        // Set Opactiy
        mOpacityGroup.alpha = TweenOpacity;
        // If lerp complete
        if (mLerpInTimer >= 1f)
        {
            mOpacityGroup.alpha = 0f;
            mLerpOutComplete = true; // set lerp complete
            mLerpOutTimer = 0f;
        }
    }

    private void DisplayForTime()
    {
        // update display time
        mDisplayTime -= Time.deltaTime;

        // if out of display time
        if (mDisplayTime <= 0f)
        {
            // Display time complete
            mDisplayTimeComplete = true;
        }
    }

    private void ResetAnimationVariables()
    {
        mUIActive = false;
        mDisplayTime = mMaxDisplayTime;
        mLerpInComplete = false;
        mLerpOutComplete = false;
        mDisplayTimeComplete = false;
        mLerpInTimer = 0f;
        mLerpOutTimer = 0f;
        mDisplayTime = mMaxDisplayTime;
    }

    public void TriggerAlertUI()
    {
        mUIActive = true;
    }
}
