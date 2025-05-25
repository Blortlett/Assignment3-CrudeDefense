using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrFurnace : MonoBehaviour, IInteractable
{
    // How coal the furnace has left to burn
    private const float mMaxCoalAmount = 100f;
    private float mCurrentCoalAmount = 0f;
    // Coal Burn rate
    [SerializeField] private float mCoalBurnRate = 1f;
    // ProgressTrackerUI
    [SerializeField] private ProgressBar mProgressBarScr;

    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        Debug.Log("Player Interacted with furnace");
        mCurrentCoalAmount = mMaxCoalAmount;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Burn coal by burn rate per frame
        if (mCurrentCoalAmount > 0f)
        {
            mCurrentCoalAmount -= mCoalBurnRate * Time.fixedDeltaTime;
        }

        // Update progress bar
        mProgressBarScr.SetProgress(mCurrentCoalAmount);
    }
}
