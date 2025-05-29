using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COilBarrel : MonoBehaviour , IPickupable
{
    // Track Items In Collider
    public List<Collider2D> mBoatInTrigger = new List<Collider2D>();

    // Pickup barrel variables
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
        // Return from function if no boat in trigger
        if (mBoatInTrigger == null || mBoatInTrigger.Count == 0) return;
        Boat BoatScr = mBoatInTrigger[0].GetComponent<Boat>();
        // Return if furnace script is not active
        if (BoatScr != null)
        {
            BoatScr.OnRecieveBarrel(); // trigger furnace to restock progress
            Destroy(gameObject); // Destroy coal bag
        }
        else
        {
            Debug.Log("No BoatScript attatched to Boat");
            return;
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boat"))
        {
            mBoatInTrigger.Add(collision);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Boat"))
        {
            mBoatInTrigger.Remove(collision);
        }
    }
}
