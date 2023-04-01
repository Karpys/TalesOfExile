using UnityEngine;

public abstract class InventoryObject
{
    [SerializeField] protected InventoryObjectData MDataData = null;

    public InventoryObjectData DataData => MDataData;
    public InventoryObject(InventoryObjectData dataData)
    {
        MDataData = dataData;
    }
}

[System.Serializable]
public class DefaultInventoryObject : InventoryObject
{
    public DefaultInventoryObject(InventoryObjectData dataData) : base(dataData){}
}
