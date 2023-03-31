using UnityEngine;

public abstract class InventoryObject
{
    [SerializeField] protected InventoryObjectVisual m_VisualData = null;

    public InventoryObjectVisual VisualData => m_VisualData;
    public InventoryObject(InventoryObjectVisual visualData)
    {
        m_VisualData = visualData;
    }
}

[System.Serializable]
public class DefaultInventoryObject : InventoryObject
{
    public DefaultInventoryObject(InventoryObjectVisual visualData) : base(visualData){}
}
