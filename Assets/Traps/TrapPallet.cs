using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPallet : MonoBehaviour, IPickupable
{
    [SerializeField] GameObject TrapSpawnable;

    public bool CanPickup()
    {
        return true;
    }

    public float GetOriginalYPosition()
    {
        throw new System.NotImplementedException();
    }

    public GameObject Pickup()
    {
        GameObject spawnedBarrel = Instantiate(TrapSpawnable, gameObject.transform.position, Quaternion.identity);

        return spawnedBarrel;
    }

    public void PutDown()
    {
        throw new System.NotImplementedException();
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
