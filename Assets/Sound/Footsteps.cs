using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [SerializeField] private List<AudioSource> FootSteps;

    int LastTriggered = 0;

    public void PlayFootStep()
    {
        //LastTriggered =
        int RandomInt = Random.Range(0, FootSteps.Count - 1);
        if (RandomInt == LastTriggered)
        {
            if (RandomInt != 0)
            {
                RandomInt = 0;
            }
            else
            {
                RandomInt = Random.Range(1, FootSteps.Count - 1);
            }
        }
        FootSteps[RandomInt].pitch = Random.Range(1f, 1.2f);
        FootSteps[RandomInt].Play();
        LastTriggered = RandomInt;
    }
}
