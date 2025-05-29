using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialScr : MonoBehaviour
{
    public static TutorialScr instance { private set; get; } // Singleton variable

    // DialogUI Components
    [SerializeField] private GameObject DialogBox;
    [SerializeField] private TMP_Text DialogText;

    // First 3 barrels to drop off
    [SerializeField] private COilBarrel[] mBarrelObjects;

    // Boat BarrelHolder script
    [SerializeField] private BarrelHolder mBoatBarrelHolder;

    [SerializeField] private GameObject mTutorialArrowLittleWheel;
    [SerializeField] private GameObject mTutorialArrowCoalBags;
    [SerializeField] private GameObject mTutorialArrowFurnace;
    [SerializeField] private GameObject mTutorialArrowBigWheel;
    [SerializeField] private GameObject mTutorialArrowBarrelButton;
    [SerializeField] private GameObject mTutorialArrowPressurePad;
    [SerializeField] private GameObject mTutorialArrowSpawnerDropoff;


    // Dialog tutorial tracker
    private bool Tutorial1Complete = false;
    private bool Tutorial2Enabled = false;
    private bool Tutorial2CompletePart1 = false;
    private bool Tutorial2CompletePart2 = false;
    private bool Tutorial2Complete = false;

    // Component tutorial tracker
    private bool mIsTutorialLittleWheelFinished;
    private bool mIsTutorialCoalBagsFinished;
    private bool mIsTutorialFurnaceFinished;
    private bool mIsTutorialBigWheelFinished;
    private bool mIsTutorialBarrelButtonFinished;
    private bool mIsTutorialPressurePadFinished;
    private bool mIsTutorialSpawnerDropoffFinished;

    // Dialog Strings
    private string[] mTut2Dialogues = new string[]
    {
        "There ya go! Now fire up that machine and get to work.",
        "I need 3 more barrels before I can go sell them for us."
    };

    private void Awake()
    {
        //      -= Singleton stuff here =-
        if ((instance != null) && (instance != this))
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        // Enable tut arrows on all starter barrels
        foreach (COilBarrel barrel in mBarrelObjects)
        {
            barrel.EnableTutorialArrow();
        }
    }

    void Update()
    {
        // Get LeftMouse Button down // Dialog system
        if (Input.GetMouseButtonDown(0))
        {
            // Click to remove tutorial 1
            if (!Tutorial1Complete)
            {
                Tutorial1Complete = true;
                DialogBox.SetActive(false);
            }
            // Click to swap to tutorial 2 part 2 dialog
            else if (Tutorial2Enabled && !Tutorial2CompletePart1)
            {
                DialogText.text = mTut2Dialogues[1];
                Tutorial2CompletePart1 = true;
            }
            // Click to swap to close tutorial 2 dialog
            else if (Tutorial2CompletePart1 && !Tutorial2CompletePart2)
            {
                DialogBox.SetActive(false);
                Tutorial2CompletePart2 = true;
                Tutorial2Complete = true;
                mTutorialArrowLittleWheel.SetActive(true);
            }
        }

        // Barrels loaded on to boat
        if (!Tutorial2Enabled && mBoatBarrelHolder.GetBarrelCount() >= 3)
            // start tutorial 2
            EnableTutorial2();
    }

    private void EnableTutorial2()
    {
        DialogText.text = mTut2Dialogues[0];
        Tutorial2Enabled = true;
        DialogBox.SetActive(true);
    }

    public void TutorialLittleWheelComplete()
    {
        if (mIsTutorialLittleWheelFinished) return;
        mTutorialArrowLittleWheel.SetActive(false);
        mTutorialArrowCoalBags.SetActive(true);
        mIsTutorialLittleWheelFinished = true;
    }

    public void TutorialCoalBagsComplete()
    {
        if (!mIsTutorialLittleWheelFinished || mIsTutorialCoalBagsFinished) return;
        mTutorialArrowCoalBags.SetActive(false);
        mTutorialArrowFurnace.SetActive(true);
        mIsTutorialCoalBagsFinished = true;
    }

    public void TutorialFurnaceComplete()
    {
        if (!mIsTutorialCoalBagsFinished || mIsTutorialFurnaceFinished) return;
        mTutorialArrowFurnace.SetActive(false);
        mTutorialArrowBigWheel.SetActive(true);
        mIsTutorialFurnaceFinished = true;
    }

    public void TutorialBigWheelComplete()
    {
        if (!mIsTutorialFurnaceFinished || mIsTutorialBigWheelFinished) return;
        mTutorialArrowBigWheel.SetActive(false);
        mTutorialArrowBarrelButton.SetActive(true);
        mIsTutorialBigWheelFinished = true;
    }

    public void TutorialBarrelButtonComplete()
    {
        if (!mIsTutorialBigWheelFinished || mIsTutorialBarrelButtonFinished) return;
        mTutorialArrowBarrelButton.SetActive(false);
        mTutorialArrowPressurePad.SetActive(true);
        mIsTutorialBarrelButtonFinished = true;
    }

    public void TutorialPressurePadComplete()
    {
        if (!mIsTutorialBarrelButtonFinished || mIsTutorialPressurePadFinished) return;
        mTutorialArrowPressurePad.SetActive(false);
        mTutorialArrowSpawnerDropoff.SetActive(true);
        mIsTutorialPressurePadFinished = true;
    }

    public void TutorialOilBarrelPickupComplete()
    {
        if (mIsTutorialPressurePadFinished)
        mTutorialArrowSpawnerDropoff.SetActive(false);
        mIsTutorialPressurePadFinished = true;
    }
}
