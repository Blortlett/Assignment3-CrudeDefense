using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValveWheel : MonoBehaviour, IInteractable
{
    // Animator Parameters
    string AnimParameterBackward = "IsSpinBackward";

    // Wheel states
    enum WheelState
    {
        WHEELSTATE_Closed,
        WHEELSTATE_Opening,
        WHEELSTATE_Open,
        WHEELSTATE_Closing
    }

    // AnimationSpeeds
    [SerializeField] float WheelForwardAnimSpeed = 1f;
    [SerializeField] float WheelBackwardAnimSpeed = 0.5f;

    // How much the turnpipe is turned
    float mTurnpipeCurrentOpenAmount = 0f;
    const float mTurnpipeMaxOpen = 100f;
    [SerializeField] float mTurnpipeOpeningSpeed = 2f;
    [SerializeField] float mTurnpipeClosingSpeed = .5f;
    WheelState mCurrentWheelState = WheelState.WHEELSTATE_Closed;

    // Turnpipe stay open for timer
    float mTurnPipeCurrentOpenTime = 0f;
    [SerializeField] float mTurnPipeOpenTimeMax = 3f;

    // Prefab's own components
    [SerializeField] private Animator mAnimator;

    // Objects outside the prefab
    [SerializeField] private GameObject mValveWheelableObject;
    private IValveWheelable mValveWheelableScript;

    // Progress Bar UI
    [SerializeField] private ProgressBar mProgressBarScr;

    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        // -= TUTORIAL =-
        TutorialScr.instance.TutorialLittleWheelComplete();
        TutorialScr.instance.TutorialBigWheelComplete();
        // -= TUTORIAL =-
        mCurrentWheelState = WheelState.WHEELSTATE_Opening;
        mAnimator.speed = .7f;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Get script of object to fill with liquid
        mValveWheelableScript = mValveWheelableObject.GetComponent<IValveWheelable>();
        if (mValveWheelableScript == null)
            throw new System.Exception("No ValveWheelable Script attached to me!!");
        // Check Progress bar script is attached
        if (mProgressBarScr == null)
            throw new System.Exception("No ValveWheelable Script attached to me!!");
        else // Update Progressbar UI
            mProgressBarScr.SetProgress(mTurnpipeCurrentOpenAmount);
        // set animator to not play on beginning
        mAnimator.speed = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Update silo - send in wheel open percentage for fill calculation
        mValveWheelableScript.FillTank(mTurnpipeCurrentOpenAmount / mTurnpipeMaxOpen);

        // Handle logic by wheel state
        switch (mCurrentWheelState)
        {
            case WheelState.WHEELSTATE_Opening:
                // set animator to forward animation
                mAnimator.SetBool(AnimParameterBackward, false);
                // set animator speed to normal speed
                mAnimator.speed = WheelForwardAnimSpeed; 
                // increase turnwheel open %   -- Opening animation should be playing here...
                mTurnpipeCurrentOpenAmount += mTurnpipeOpeningSpeed;
                // Update Progressbar UI
                mProgressBarScr.SetProgress(mTurnpipeCurrentOpenAmount);
                // If turnwheel == max time, set state to open & timer to full
                if (mTurnpipeCurrentOpenAmount >= mTurnpipeMaxOpen)
                {
                    mTurnPipeCurrentOpenTime = mTurnPipeOpenTimeMax; // reset timer for how long turnpipe should be open for
                    mCurrentWheelState = WheelState.WHEELSTATE_Open; // Set state to open
                }
                break;

            case WheelState.WHEELSTATE_Open:
                // Set animator speed to stopped
                mAnimator.speed = 0f;
                // Wheel stays open only for a certain timer
                mTurnPipeCurrentOpenTime -= Time.fixedDeltaTime;
                // If timer hit zero - turnpipe closed
                if (mTurnPipeCurrentOpenTime <= 0f)
                {
                    mCurrentWheelState = WheelState.WHEELSTATE_Closing;  // Set state to closing
                }
                break;

            case WheelState.WHEELSTATE_Closing:
                // Set animation to play backwards
                mAnimator.SetBool(AnimParameterBackward, true);
                // Set animation to play slower
                mAnimator.speed = WheelBackwardAnimSpeed;
                // decrease turnwheel open %   -- Closing animation should be playing here...
                mTurnpipeCurrentOpenAmount -= mTurnpipeClosingSpeed;
                // Update Progressbar UI
                mProgressBarScr.SetProgress(mTurnpipeCurrentOpenAmount);
                // If wheel fully closes..
                if (mTurnpipeCurrentOpenAmount <= 0f)
                {
                    // swap to closed state and stop animator
                    mAnimator.speed = 0f;
                    mCurrentWheelState = WheelState.WHEELSTATE_Closed;
                }
                break;
        }
    }
}
