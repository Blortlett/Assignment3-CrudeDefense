using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteractable : MonoBehaviour,  Interactable
{

    public bool CanInteract()
    {
        Debug.Log("Button in range");
        return true;
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
