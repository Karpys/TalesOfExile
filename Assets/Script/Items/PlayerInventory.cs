using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : SingletonMonoBehavior<PlayerInventory>
{
    private List<InventoryObject> m_PlayerInventory = new List<InventoryObject>();

    [SerializeField] private List<InventoryObjectScriptable> m_StartInvetoryTest = new List<InventoryObjectScriptable>();

    private void Start()
    {
        foreach (InventoryObjectScriptable invObject in m_StartInvetoryTest)
        {
            AddObject(invObject);
        }
    }

    public void AddObject(InventoryObjectScriptable scriptable)
    {
        m_PlayerInventory.Add(scriptable.ToIventoryObject());
    }
}
