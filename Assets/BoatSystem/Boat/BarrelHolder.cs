using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelHolder : MonoBehaviour
{
    // BoatSystem Script Reference
    [SerializeField] private BoatSystem mBoatSystem;

    // Barrel sprites
    [SerializeField] private GameObject[] mOilSprites = new GameObject[6];
    // Barrels on boat counter
    int mMaxBarrels = 6;
    int mBarrelCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject sprite in mOilSprites)
        {
            sprite.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AcceptBarrel()
    {
        // Incremenet barrel amount
        mBarrelCount++;

        // Increase barrel sprites shown on boat
        SetSprites();
        
        // If boat full, alert BoatSystem script
        if (mBarrelCount > 5)
        {
            mBoatSystem.OnBoatFull();
        }
    }

    private void SetSprites()
    {
        // Loop through all barrel sprites. Turn on/off sprites for mBarrelCount
        for (int i = 0; i < mMaxBarrels; i++)
        {
            // if barrel is part of barrel count
            if (i < mBarrelCount)
            {
                // Turn BarrelSpriteGFX on
                mOilSprites[i].SetActive(true);
            }
            else
            {
                // Turn BarrelSpriteGFX off
                mOilSprites[i].SetActive(false);
            }
        }
    }

    public void EmptyBoat()
    {
        mBarrelCount = 0;
        SetSprites();
    }

    public int GetBarrelCount()
    {
        return mBarrelCount;
    }
}
