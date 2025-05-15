using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_OilBarrel : MonoBehaviour, IInteractable
{
    bool mIsPickedUp;

    public bool CanInteract()
    {
        Debug.Log("OilBarrel in range");
        return true;
    }

    public void Interact()
    {
        Debug.Log("OilBarrel picked up :)");
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
