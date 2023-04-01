using UnityEngine;

public abstract class InventoryObject
{
    [SerializeField] protected InventoryObjectData m_Data = null;

    public InventoryObjectData Data => m_Data;
    public InventoryObject(InventoryObjectData data)
    {
        m_Data = data;
    }
}

[System.Serializable]
public class DefaultInventoryObject : InventoryObject
{
    public DefaultInventoryObject(InventoryObjectData dataData) : base(dataData){}
}
