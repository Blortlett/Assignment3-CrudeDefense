using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OilSiloScript : MonoBehaviour, IButtonable
{
    // Oil level visuals
    [SerializeField] private Transform mOilLevelParent; // Reference to the oil level sprite
    private const float mMaxOilLevel = 100f;
    private float mCurrentOilLevel = 0f;
    // Rate at which the tank fills
    [SerializeField] private const float mFillRate = 20f;
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
    void Update()
    {
        TryFillOil();
        UpdateOilLevelVisual();
    }

    // This actually runs when wheel turns not on button press but we hacky out here
    public void OnButtonPress()
    { // Toggle machine running on and off
        mMachineRunning = !mMachineRunning;
    }

    // function to run filling oil levels
    private void TryFillOil()
    {
        if (!mMachineRunning) return; // If not filling oil dont worry about this function, just move on
        if (mCurrentOilLevel > +mMaxOilLevel) return; // If tank already full exit function

        Debug.Log("Filling Silo with oil");
        mCurrentOilLevel += mFillRate * Time.deltaTime; // Raise tank oil percentage
    }

    private void UpdateOilLevelVisual()
    {
        if (mOilLevelParent != null) // error check
        {
            // Set oil graphics size
            float PercentFull = (mCurrentOilLevel / mMaxOilLevel);
            float yLevel = 1 + (PercentFull * mMaxOilGraphicYScale);
            mOilLevelParent.localScale = new Vector2(1, yLevel);
        }
    }
}
