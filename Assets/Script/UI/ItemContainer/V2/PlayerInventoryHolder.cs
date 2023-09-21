using KarpysDev.Script.Items;

namespace KarpysDev.Script.UI.ItemContainer.V2
{
    public class PlayerInventoryHolder : ItemUIHolder
    {
        public override bool CanReceiveItem(Item item, ItemHolderGroup holderSource)
        {
            return true;
        }
    }
}