using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialScr : MonoBehaviour
{
    // DialogUI Components
    [SerializeField] private GameObject DialogBox;
    [SerializeField] private TMP_Text DialogText;

    // First 3 barrels to drop off
    [SerializeField] private COilBarrel[] mBarrelObjects;

    // Boat BarrelHolder script
    [SerializeField] private BarrelHolder mBoatBarrelHolder;


    // tutorial progress tracker
    private bool Tutorial1Complete = false;
    private bool Tutorial2Enabled = false;
    private bool Tutorial2CompletePart1 = false;
    private bool Tutorial2CompletePart2 = false;
    private bool Tutorial2Complete = false;



    private string[] mTut2Dialogues = new string[]
    {
        "There ya go! Now fire up that machine and get to work.",
        "I need 3 more barrels before I can go sell them for us."
    };


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
        // Get LeftMouse Button down
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
}
