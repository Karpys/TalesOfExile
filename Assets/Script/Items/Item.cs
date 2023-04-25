
using System.Collections.Generic;
using UnityEngine;

public abstract class Item:ISavable
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

public interface ISavable
{
    public string GetSaveData();
}

public interface ILoadable
{
    public void LoadData(string saveData);
}

public interface ISaver
{
    public string[] FetchSaveData();
    public void WriteSaveData(string saveName, string[] data);
}