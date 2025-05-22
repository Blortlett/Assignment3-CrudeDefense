using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OilSiloScript : MonoBehaviour, IButtonable
{
    // Slider varaibles (OilLevel Calculations)
    [SerializeField] private Slider mOilLevelSlider; // Reference to the Slider component   // This was a stupid idea, unhook this and delete the slider component for all I care
    [SerializeField] private Sprite mOilLevelSprite; // Reference to the oil level sprite
    private const float mMaxOilLevel = 100f;
    private float mCurrentOilLevel;
    private const float mMinimumOilLevel = 30f;
    // Rate at which the tank fills
    private const float mFillRate = 20f;

    // Silo running/filling variables
    public bool mMachineRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        mCurrentOilLevel = 30f;
    }

    // Update is called once per frame
    void Update()
    {
        TryFillOil();
        mOilLevelSlider.value = mCurrentOilLevel / mMaxOilLevel;
    }

    // This actually runs when wheel turns not on button press but we hacky out here
    public void OnButtonPress()
    { // Toggle machine running on and off
        Debug.Log("Toggled filling Silo with oil");
        mMachineRunning = !mMachineRunning;
    }

    // function to run filling oil levels
    private void TryFillOil()
    {
        if (!mMachineRunning) return; // If not filling oil dont worry about this function, just move on
        Debug.Log("Filling Silo with oil");
        mCurrentOilLevel += mFillRate * Time.deltaTime;
    }

    private void UpdateOilLevelVisual()
    {
        if (mOilLevelSprite != null && mOilLevelSlider != null)
        {
            //mOilLevelSprite.rect.height = mOilLevelSlider.value;
            //scale.y = mOilLevelSlider.value;
            //mOilLevelSprite.localScale = scale;
        }
    }
}
