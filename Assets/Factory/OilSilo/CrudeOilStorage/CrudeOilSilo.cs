using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrudeOilSilo: MonoBehaviour, IValveWheelable
{
    // Oil level visuals
    [SerializeField] private Transform mOilLevelParent; // Reference to the oil level sprite
    private const float mMaxOilLevel = 100f;
    private float mCurrentOilLevel = 0f;
    // Rate at which the tank fills
    [SerializeField] private float mFillRate = .1f;
    // Silo running/filling variables
    public bool mMachineRunning = false;
    // MaxGraphicSize
    [SerializeField] private float mMaxOilGraphicYScale = 3;

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
        if (mCurrentOilLevel >= mMaxOilLevel) return;
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
}
