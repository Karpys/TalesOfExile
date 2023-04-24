using System;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerInventory : MonoBehaviour,ISaver
{
    [SerializeField] private string m_SaveName = string.Empty;
    
    private List<Item> m_PlayerInventory = new List<Item>();

    public List<Item> Inventory => m_PlayerInventory;

    public void PickUp(Item item)
    {
        m_PlayerInventory.Add(item);
        PrintItemPickUp(item);
    }

    private void PrintItemPickUp(Item item)
    {
        string colorCode = ColorUtility.ToHtmlStringRGB(RarityLibrary.Instance.GetParametersViaKey(item.Rarity).RarityColor);
        Debug.Log("<color=#"+ colorCode+">" + item.Data.ObjectName + " Item picked up : "+ item.Rarity + "</color>");
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
        List<Item> saveObjects = SaveUtils.InterpretSave<Item>(data);
        m_PlayerInventory = saveObjects;
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
