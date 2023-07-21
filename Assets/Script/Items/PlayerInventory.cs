using System;
using System.Collections.Generic;
using System.Linq;
using KarpysDev.Script.Entities.EquipementRelated;
using KarpysDev.Script.Manager.Library;
using KarpysDev.Script.Utils;
using Script.Data;
using UnityEngine;

namespace KarpysDev.Script.Items
{
    public class PlayerInventory : MonoBehaviour,ISaver
    {
        [SerializeField] private TextAsset m_BaseInventory = null;
        [SerializeField] private EntityEquipement m_Equipement = null;
        [SerializeField] private string m_SaveName = string.Empty;
        [SerializeField] private int m_InventoryItemCount = 0;

        private Item[] m_PlayerInventory = null;
        public Item[] Inventory => m_PlayerInventory;
        public Item[] Equipement => m_Equipement.Equipement;

        public Action A_OnPickUp = null;

        private int EquipementLenght => Equipement.Length + m_PlayerInventory.Length;
        private void Awake()
        {
            GlobalSaver.AddSaver(this);
            m_PlayerInventory = new Item[m_InventoryItemCount + 1];
        }

        public void Init()
        {
            InterpretSave();
        }

        private void OnApplicationQuit()
        {
            //SaveInventory();
        }
    
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X))
                ClearPlayerInventory();
        }

        public bool TryPickUp(Item item)
        {
            bool onPickUp = false;
            for (int i = 0; i < m_InventoryItemCount; i++)
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
            string colorCode = RarityLibrary.Instance.GetParametersViaKey(item.Rarity).RarityColor.ToColorString();
            Debug.Log(colorCode.ToColorTag()+ item.Data.ObjectName + " Item picked up : "+ item.Rarity + "</color>");
        }

        public void UpdateItem(Item item, int id)
        {
            m_PlayerInventory[id] = item;
        }
        //Save Part
        private void SaveInventory()
        {
            WriteSaveData(GetSaveName,FetchSaveData());
        }
        
        private void ClearPlayerInventory()
        {
            string[] data = Enumerable.Repeat("none",EquipementLenght).ToArray();
            SaveUtils.WriteSave(m_SaveName,data);
            InterpretSave();
        }

        private void InterpretSave()
        {
            string[] data = SaveUtils.ReadData(GetSaveName,m_BaseInventory);
            List<Item> saveObjects = SaveUtils.InterpretSave<Item>(data);
        
            m_PlayerInventory = saveObjects.GetRange(0,m_PlayerInventory.Length).ToArray();
            m_Equipement.SetSaveEquipement(saveObjects.GetRange(m_PlayerInventory.Length,m_Equipement.Equipement.Length).ToArray());
        }
    
        public void WriteSaveData(string saveName, string[] data)
        {
            SaveUtils.WriteSave(saveName,data);
        }

        public string GetSaveName => m_SaveName;

        public string[] FetchSaveData()
        {
            string[] itemDataSaves = new string[EquipementLenght];
        
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
}
