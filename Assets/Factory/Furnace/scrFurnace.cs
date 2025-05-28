using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrFurnace : MonoBehaviour, IAcceptCoal
{
    // How much coal the furnace has left to burn
    private const float mMaxCoalAmount = 100f;
    private float mCurrentCoalAmount = 0f;
    // Coal Burn rate
    [SerializeField] private float mCoalBurnRate = .3f;
    // ProgressTrackerUI
    [SerializeField] private ProgressBar mProgressBarScr;
    // %Rate one coal bag fills furnace
    [SerializeField] private float mCoalbagMult = .6f;
    // Script the furnace will update
    [SerializeField] private GameObject mFurnaceableObject;
    private IFurnaceable mFurnaceableSript;



    // Start is called before the first frame update
    void Start()
    {
        mFurnaceableSript = mFurnaceableObject.GetComponent<IFurnaceable>();
        if (mFurnaceableSript == null)
        {
            Debug.Log("Furnace not attatched to Furnaceable");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Burn coal by burn rate per frame
        if (mCurrentCoalAmount > 0f) // furnace is running
        {
            mCurrentCoalAmount -= mCoalBurnRate * Time.fixedDeltaTime;

            if (mFurnaceableSript != null) // error check script is attached
            {
                mFurnaceableSript.OnToggleFurnace(true); // Tell oil silo furnace is running
            }
        }
        else // furnace empty
        {
            if (mFurnaceableSript != null) // error check script is attached
            {
                mFurnaceableSript.OnToggleFurnace(false); // Tell oil silo furnace is OFF
            }
        }

        // Update progress bar
        mProgressBarScr.SetProgress(mCurrentCoalAmount);
    }

    public void OnRecieveCoal()
    {
        // Add burn time to furnace
        mCurrentCoalAmount += mMaxCoalAmount * mCoalbagMult;

        // Clamp to 100
        if (mCurrentCoalAmount > 100f)
        {
            mCurrentCoalAmount = 100f;
        }
    }

    public bool CanInteract()
    {
        return true;
    }
}
