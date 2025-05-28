using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalBag : MonoBehaviour, IPickupable
{
    // Track Items In Collider
    public List<Collider2D> mFurnaceInTrigger = new List<Collider2D>();

    public bool CanPickup()
    {
        return true;
    }

    public float GetOriginalYPosition()
    {
        return transform.position.y;  // get Y position
    }

    public GameObject Pickup()
    {
        return gameObject;
    }

    public void PutDown()
    {
        // Return from function if no furnace in trigger
        if (mFurnaceInTrigger[0] == null) return;
        scrFurnace FurnaceScript = mFurnaceInTrigger[0].GetComponent<scrFurnace>();
        // Return if furnace script is not active
        if (FurnaceScript != null)
        {
            FurnaceScript.OnRecieveCoal(); // trigger furnace to restock progress
            Destroy(gameObject); // Destroy coal bag
        } 
        else
        {
            Debug.Log("No FurnaceScript attatched to furnace");
            return;
        }
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
        if (collision.CompareTag("Furnace"))
        {
            mFurnaceInTrigger.Add(collision);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Furnace"))
        {
            mFurnaceInTrigger.Remove(collision);
        }
    }
}
