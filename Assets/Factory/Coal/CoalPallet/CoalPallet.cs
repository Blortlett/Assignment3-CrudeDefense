using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalPallet : MonoBehaviour, IPickupable
{
    [SerializeField] GameObject CoalBagSpawnable;

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
        GameObject spawnedBarrel = Instantiate(CoalBagSpawnable, gameObject.transform.position, Quaternion.identity);
        // -= TUTORIAL =-
        TutorialScr.instance.TutorialCoalBagsComplete();
        // -= TUTORIAL =-
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
