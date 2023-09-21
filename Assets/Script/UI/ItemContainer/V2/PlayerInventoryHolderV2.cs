using KarpysDev.Script.Items;

namespace KarpysDev.Script.UI.ItemContainer.V2
{
    public class PlayerInventoryHolderV2 : ItemUIHolderV2
    {
        public override bool CanReceiveItem(Item item, ItemHolderGroup holderSource)
        {
            return true;
        }
    }
}