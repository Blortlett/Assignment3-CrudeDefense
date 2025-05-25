using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COilBarrel : MonoBehaviour , IPickupable
{
    const bool mCanPickup = true;
    private float mOriginalYPosition;

    public bool CanPickup()
    {
        return mCanPickup;
    }

    public GameObject Pickup()
    {
        return gameObject;
    }

    public void PutDown()
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
