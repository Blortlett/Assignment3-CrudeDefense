using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteractable : MonoBehaviour,  IInteractable
{

    public bool CanInteract()
    {
        Debug.Log("Button in range");
        return true;
    }

    public bool CanPickup()
    {
        return false;
    }

    public void Interact()
    {
        Debug.Log("Button pressed :)");
    }


    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
