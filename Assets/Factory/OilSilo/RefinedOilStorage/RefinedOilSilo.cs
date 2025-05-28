using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefinedOilSilo: MonoBehaviour, IValveWheelable, IFurnaceable
{
    // Oil level visuals
    [SerializeField] private Transform mOilLevelParent; // Reference to the oil level sprite
    private const float mMaxOilLevel = 100f;
    private float mCurrentOilLevel = 0f;

    // Rate at which the tank fills
    [SerializeField] private float mFillRate = .1f;
    [SerializeField] private float mStealMult = 1.6f;     // Rate at which the tank takes from prerequisite

    // Silo running/filling variables
    public bool mMachineRunning = false;
    // MaxGraphicSize
    [SerializeField] private float mMaxOilGraphicYScale = 3;

    // Game objects with prerequisite variables to fill
    [SerializeField] private CrudeOilSilo mCrudeOilSiloScript;   // crude oil silo
    private bool mIsFurnaceRunning = false;                      // furnace

    // Start is called before the first frame update
    void Start()
    {
        mCurrentOilLevel = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateOilLevelVisual();
    }

    private void UpdateOilLevelVisual()
    {
        if (mOilLevelParent != null) // error check
        {
            // oil graphics size calculations
            float PercentFull = (mCurrentOilLevel / mMaxOilLevel);
            float yLevel = 1 + (PercentFull * mMaxOilGraphicYScale); // 1 + () is here because we want some oil at bottom if 0
            // Set oil graphics size
            mOilLevelParent.localScale = new Vector2(1, yLevel);
        }
    }

    public void FillTank(float WheelOpenPercent)
    {
        // Check wheel is open
        if (WheelOpenPercent <= 0f) return;                                                 // Wheel not open, return from function
        // Check if furnace is running
        if (!mIsFurnaceRunning) return;                                                     // If furnace not running, return from function do not continue
        float CrudeOilLevel = mCrudeOilSiloScript.GetOilLevel();
        // check if any crude oil is available to take from
        if (CrudeOilLevel <= (mFillRate * WheelOpenPercent) * mStealMult) return;           // if other silo has less than steal rate, return from function

        // Fill oil level only if tank has room
        if (mCurrentOilLevel >= mMaxOilLevel) return;                                       // tank already fill, exit function
        // Remove oil from CrudeStorage
        mCrudeOilSiloScript.RemoveOil((mFillRate * WheelOpenPercent) * mStealMult);
        // Increase RefinedOil storage
        mCurrentOilLevel += mFillRate * WheelOpenPercent;
    }

    public float GetOilLevel()
    {
        return mCurrentOilLevel;
    }

    public void RemoveOil(float Amount)
    {
        // Remove oil
        mCurrentOilLevel -= Amount;

        // clamp min value to 0
        if (mCurrentOilLevel < 0f)
        {
            mCurrentOilLevel = 0f;
        }
    }

    public void OnToggleFurnace(bool _isOn)
    {
        mIsFurnaceRunning = _isOn;
    }
}
