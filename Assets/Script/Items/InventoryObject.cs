
public abstract class InventoryObject
{
    protected InventoryObjectData m_Data = null;

    public InventoryObjectData Data => m_Data;
    public InventoryObject(InventoryObjectData data)
    {
        m_Data = data;
    }
}