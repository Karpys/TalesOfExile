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
            WriteSaveData(GetSaveName(),FecthSaveData());
        }
    }

    public void WriteSaveData(string saveName, string[] datas)
    {
        string savePath = SaveUtils.GetSaveDirectory() + saveName;
        File.WriteAllLines(savePath,datas);
    }
    
    public string GetSaveName()
    {
        return m_SaveName;
    }

    public string[] FecthSaveData()
    {
        string[] itemDatasSaves = new string[m_PlayerInventory.Count];

        for (int i = 0; i < m_PlayerInventory.Count; i++)
        {
            itemDatasSaves[i] = m_PlayerInventory[i].GetSaveData();
        }
        
        return itemDatasSaves;
    }
}
