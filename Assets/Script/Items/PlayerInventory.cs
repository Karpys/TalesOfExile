using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<InventoryObject> m_PlayerInventory = new List<InventoryObject>();

    public List<InventoryObject> Inventory => m_PlayerInventory;

    public void PickUp(InventoryObject inventoryObject)
    {
        m_PlayerInventory.Add(inventoryObject);
    }
}
