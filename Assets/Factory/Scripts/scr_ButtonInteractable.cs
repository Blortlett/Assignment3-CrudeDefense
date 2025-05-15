using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_ButtonInteractable : MonoBehaviour,  IInteractable
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
