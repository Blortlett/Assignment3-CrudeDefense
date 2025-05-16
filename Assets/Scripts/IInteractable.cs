using UnityEngine;

public interface IInteractable
{
    public void Interact();
    public bool CanInteract();
    public bool CanPickup();

    public float GetOriginalYPosition();
}