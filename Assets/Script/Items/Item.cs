using System.Collections.Generic;
using KarpysDev.Script.Manager.Library;
using KarpysDev.Script.UI;
using KarpysDev.Script.UI.ItemContainer.V2;
using KarpysDev.Script.Utils;
using UnityEngine;

namespace KarpysDev.Script.Items
{
    public abstract class Item
    {
        protected InventoryItemData m_Data = null;
        public Rarity Rarity => GetRarity();
        public InventoryItemData Data => m_Data;
        public Item(InventoryItemData data)
        {
            m_Data = data;
        }
    
        protected virtual Rarity GetRarity()
        {
            return m_Data.Rarity;
        }

        //Save Load Constructor
        public Item(string[] saveArgs)
        {
            m_Data = ItemLibrary.Instance.GetBaseDataViaId(saveArgs[1].ToInt());
        }

        public virtual List<ItemButtonUIParameters> ButtonRequestOptionButton(ItemUIHolder inventoryUI)
        {
            List<ItemButtonUIParameters> newItemButton = new List<ItemButtonUIParameters>();
            newItemButton.Add(new ItemButtonUIParameters(DisplayName,"Display Name"));
            return newItemButton;
        }

        private void DisplayName()
        {
            Debug.Log(m_Data.ObjectName);
        }
    
        //Save Part//
        public virtual string GetSaveData()
        {
            return GetType() + " " + m_Data.UniqueId + " " + (int)Rarity + " ";
        }
    }
    public interface ISaver
    {
        public string GetSaveName { get;}
        public string[] FetchSaveData();
        public void WriteSaveData(string saveName, string[] data);
    }
}