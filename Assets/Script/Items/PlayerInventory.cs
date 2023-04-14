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
        PrintItemPickUp(inventoryObject);
    }

    private void PrintItemPickUp(InventoryObject inventoryObject)
    {
        string colorCode = ColorUtility.ToHtmlStringRGB(RarityLibrary.Instance.GetParametersViaKey(inventoryObject.Rarity).RarityColor);
        Debug.Log("<color=#"+ colorCode+">" + inventoryObject.Data.ObjectName + " Item picked up : "+ inventoryObject.Rarity + "</color>");
    }

    
    //Save Part

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            WriteSaveData(m_SaveName,FetchSaveData());
        }else if (Input.GetKeyDown(KeyCode.W))
        {
            InterpretSave();
        }
    }

    private void InterpretSave()
    {
        string[] data = File.ReadAllLines(SaveUtils.GetSavePath(m_SaveName));
        List<InventoryObject> saveObjects = SaveUtils.InterpretSave<InventoryObject>(data);
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
