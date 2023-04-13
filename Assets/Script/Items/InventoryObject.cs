
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryObject:ISavable
{
    protected InventoryObjectData m_Data = null;

    protected Rarity m_ItemRarity = Rarity.Null;
    protected InventoryUIHolder m_UIHolder = null;
    public InventoryObjectData Data => m_Data;
    public InventoryUIHolder UIHolder => m_UIHolder;
    public InventoryObject(InventoryObjectData data)
    {
        m_Data = data;
        m_ItemRarity = data.Rarity;
    }

    public void SetHolder(InventoryUIHolder holder)
    {
        m_UIHolder = holder;
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
        return GetType() + " " + m_Data.UniqueId + " " + (int)m_ItemRarity;
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