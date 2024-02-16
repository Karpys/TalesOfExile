using System;
using System.Collections.Generic;
using System.Linq;
using KarpysDev.Script.Entities.EquipementRelated;
using KarpysDev.Script.Manager.Library;
using KarpysDev.Script.UI.ItemContainer.V2;
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

        private ItemUIHolder[] m_Holders = null;
        public PlayerEquipementHolder[] EquipementHolders => m_Equipement.Equipement;

        private int EquipementLenght => EquipementHolders.Length + m_InventoryItemCount;
        private void Awake()
        {
            GlobalSaver.AddSaver(this);
        }

        public void Init()
        {
            InterpretSave();
        }
        
        public void AssignInventoryHolders(ItemUIHolder[] holders)
        {
            m_Holders = holders;
        }

        public void AssignEquipementHolders(PlayerEquipementHolder[] holders)
        {
            m_Equipement.SetSaveEquipement(holders);
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
                if (m_Holders[i].AttachedItem == null)
                {
                    m_Holders[i].SetItem(item);
                    onPickUp = true;
                    //_OnPickUp?.Invoke();
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
        
            Item[] initInventory = saveObjects.GetRange(0,m_InventoryItemCount).ToArray();
            Item[] equipementInit = saveObjects.GetRange(initInventory.Length, m_Equipement.Equipement.Length).ToArray();
            
            for (int i = 0; i < m_Holders.Length; i++)
            {
                m_Holders[i].SetItem(initInventory[i]);
            }

            for (int i = 0; i < equipementInit.Length; i++)
            {
                m_Equipement.Equipement[i].SetItem(equipementInit[i]);
            }
        }
    
        public void WriteSaveData(string saveName, string[] data)
        {
            SaveUtils.WriteSave(saveName,data);
        }

        public string GetSaveName => m_SaveName;

        public string[] FetchSaveData()
        {
            string[] itemDataSaves = new string[EquipementLenght];
        
            for (int i = 0; i < m_Holders.Length; i++)
            {
                if (m_Holders[i].AttachedItem != null)
                {
                    itemDataSaves[i] = m_Holders[i].AttachedItem.GetSaveData();
                }
                else
                {
                    itemDataSaves[i] = "none";
                }
            }

            for (int i = 0; i < EquipementHolders.Length; i++)
            {
                if (EquipementHolders[i].AttachedItem != null)
                {
                    itemDataSaves[i + m_Holders.Length] = EquipementHolders[i].AttachedItem.GetSaveData();
                }
                else
                {
                    itemDataSaves[i + m_Holders.Length] = "none";
                }
            }
        
            return itemDataSaves;
        }
    }
}
