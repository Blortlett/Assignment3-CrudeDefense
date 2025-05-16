using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValveWheel : MonoBehaviour, IInteractable
{
    public bool CanInteract()
    {
        return true;
    }

    public bool CanPickup()
    {
        return false;
    }

    public void Interact()
    {
        Debug.Log("Turning Valve Wheel");
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
