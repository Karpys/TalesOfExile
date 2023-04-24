public class DefaultItem : Item
{
    public DefaultItem(InventoryItemData dataData) : base(dataData){}
    
    //Save Load Constructor
    public DefaultItem(string[] saveArgs):base(saveArgs){}
}