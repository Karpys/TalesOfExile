
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryObject
{
    protected InventoryObjectData m_Data = null;

    public InventoryObjectData Data => m_Data;
    public InventoryObject(InventoryObjectData data)
    {
        m_Data = data;
    }

    public virtual List<ItemButtonUIParameters> ButtonRequestOptionButton()
    {
        List<ItemButtonUIParameters> newItemButton = new List<ItemButtonUIParameters>();
        newItemButton.Add(new ItemButtonUIParameters(DisplayName,"Display Name"));
        return newItemButton;
    }

    private void DisplayName()
    {
        Debug.Log(m_Data.ObjectName);
    }
}