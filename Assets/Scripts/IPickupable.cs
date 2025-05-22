using UnityEngine;

public interface IPickupable
{
    public bool CanPickup();
    public GameObject Pickup();
    public void PutDown();

    public float GetOriginalYPosition();
}