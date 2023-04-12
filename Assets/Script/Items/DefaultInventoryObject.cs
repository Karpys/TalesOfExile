public class DefaultInventoryObject : InventoryObject
{
    public DefaultInventoryObject(InventoryObjectData dataData) : base(dataData){}
    
    //Save Load Constructor
    public DefaultInventoryObject(string[] saveArgs):base(saveArgs){}
}