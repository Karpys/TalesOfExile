
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryObject:ISavable
{
    protected InventoryObjectData m_Data = null;
    protected InventoryUIHolder m_UIHolder = null;
    public Rarity Rarity => GetRarity();
    public InventoryObjectData Data => m_Data;
    public InventoryUIHolder UIHolder => m_UIHolder;
    public InventoryObject(InventoryObjectData data)
    {
        m_Data = data;
    }

    public void SetHolder(InventoryUIHolder holder)
    {
        m_UIHolder = holder;
    }

    protected virtual Rarity GetRarity()
    {
        return m_Data.Rarity;
    }

    //Save Load Constructor
    public InventoryObject(string[] saveArgs)
    {
        Debug.Log("Unique Id" + saveArgs[1]);
    }

    public virtual List<ItemButtonUIParameters> ButtonRequestOptionButton(InventoryUIHolder inventoryUI)
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
        return GetType() + " " + m_Data.UniqueId + " " + (int)Rarity;
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