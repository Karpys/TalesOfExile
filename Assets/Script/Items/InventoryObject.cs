
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryObject:ISavable
{
    protected InventoryObjectData m_Data = null;

    public InventoryObjectData Data => m_Data;
    public InventoryObject(InventoryObjectData data)
    {
        m_Data = data;
    }

    public virtual List<ItemButtonUIParameters> ButtonRequestOptionButton()
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
        return GetType() + " " + m_Data.UniqueId;
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