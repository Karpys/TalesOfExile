using System;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerInventory : MonoBehaviour,ISaver
{
    [SerializeField] private string m_SaveName = string.Empty;
    
    private List<InventoryObject> m_PlayerInventory = new List<InventoryObject>();

    public List<InventoryObject> Inventory => m_PlayerInventory;

    public void PickUp(InventoryObject inventoryObject)
    {
        m_PlayerInventory.Add(inventoryObject);
    }

    
    //Save Part

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            WriteSaveData(m_SaveName,FetchSaveData());
        }
    }

    public void WriteSaveData(string saveName, string[] data)
    {
        SaveUtils.WriteSave(saveName,data);
    }

    public string[] FetchSaveData()
    {
        string[] itemDataSaves = new string[m_PlayerInventory.Count];

        for (int i = 0; i < m_PlayerInventory.Count; i++)
        {
            itemDataSaves[i] = m_PlayerInventory[i].GetSaveData();
        }
        
        return itemDataSaves;
    }
}
