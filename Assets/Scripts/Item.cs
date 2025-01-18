using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    public Sprite itemIcon;
    public int amount;

    public void Interact(Inventory inventory)
    {
        inventory.AddToInventory(this.gameObject);
    }

    public void Interact()
    {
        throw new System.NotImplementedException();
    }

    public void ShowText(bool flag)
    {
        throw new System.NotImplementedException();
    }
}
