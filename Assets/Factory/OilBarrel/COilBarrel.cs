using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COilBarrel : MonoBehaviour , IInteractable
{
    const bool mCanPickup = true;
    private float mOriginalYPosition;

    public bool CanInteract()
    {
        return true;
    }

    public bool CanPickup()
    {
        return mCanPickup;
    }

    public void Interact()
    {
        
    }

    public float GetOriginalYPosition()
    {
        return mOriginalYPosition;
    }

    private void Awake()
    {
        mOriginalYPosition = transform.position.y;  //Sets original Y position
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
