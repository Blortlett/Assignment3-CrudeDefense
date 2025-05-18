using UnityEngine;

public interface IPickupable
{
    public bool CanPickup();
    public void Pickup();
    public void PutDown();

    public float GetOriginalYPosition();
}