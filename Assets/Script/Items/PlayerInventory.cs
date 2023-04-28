using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerInventory : MonoBehaviour,ISaver
{
    [SerializeField] private EntityEquipement m_Equipement = null;
    [SerializeField] private string m_SaveName = string.Empty;
    [SerializeField] private int m_InventoryItemCount = 0;
    [SerializeField] private int m_EquipementItemCount = 0;
    
    private Item[] m_PlayerInventory = null;
    public Item[] Inventory => m_PlayerInventory;
    public Item[] Equipement => m_Equipement.Equipement;

    public Action A_OnPickUp = null;
    private void Awake()
    {
        m_PlayerInventory = new Item[m_InventoryItemCount];
    }
    
    public bool TryPickUp(Item item)
    {
        bool onPickUp = false;
        for (int i = 0; i < m_PlayerInventory.Length; i++)
        {
            if (m_PlayerInventory[i] == null)
            {
                m_PlayerInventory[i] = item;
                onPickUp = true;
                A_OnPickUp?.Invoke();
                break;
            }
        } 
        //m_PlayerInventory.Add(item);
        PrintItemPickUp(item);

        return onPickUp;
    }

    private void PrintItemPickUp(Item item)
    {
        string colorCode = ColorUtility.ToHtmlStringRGB(RarityLibrary.Instance.GetParametersViaKey(item.Rarity).RarityColor);
        Debug.Log("<color=#"+ colorCode+">" + item.Data.ObjectName + " Item picked up : "+ item.Rarity + "</color>");
    }

    public void SwapItem(int id1, int id2)
    {
        (m_PlayerInventory[id1], m_PlayerInventory[id2]) = (m_PlayerInventory[id2], m_PlayerInventory[id1]);
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
        
        m_PlayerInventory = saveObjects.GetRange(0,m_PlayerInventory.Length).ToArray();
        m_Equipement.SetSaveEquipement(saveObjects.GetRange(m_PlayerInventory.Length,m_Equipement.Equipement.Length).ToArray());
    }
    
    public void WriteSaveData(string saveName, string[] data)
    {
        SaveUtils.WriteSave(saveName,data);
    }

    public string[] FetchSaveData()
    {
        string[] itemDataSaves = new string[m_PlayerInventory.Length + Equipement.Length];
        
        for (int i = 0; i < m_PlayerInventory.Length; i++)
        {
            if (m_PlayerInventory[i] != null)
            {
                itemDataSaves[i] = m_PlayerInventory[i].GetSaveData();
            }
            else
            {
                itemDataSaves[i] = "none";
            }
        }

        for (int i = 0; i < Equipement.Length; i++)
        {
            if (Equipement[i] != null)
            {
                itemDataSaves[i + m_PlayerInventory.Length] = Equipement[i].GetSaveData();
            }
            else
            {
                itemDataSaves[i + m_PlayerInventory.Length] = "none";
            }
        }
        
        return itemDataSaves;
    }
}
