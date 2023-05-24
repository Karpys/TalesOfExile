using KarpysDev.Script.Items;
using UnityEngine;

namespace KarpysDev.Script.Utils
{
    public static class LootUtils
    {
        public static Item ToInventoryObject(this InventoryItemData itemData)
        {
            switch (itemData.ObjectType)
            {
                case ObjectType.DefaultObject:
                    return new DefaultItem(itemData);
                case ObjectType.Equipement:
                    return new EquipementItem(itemData as EquipementItemData);
                default:
                    Debug.LogError("Equipement Object Data type not set up" + itemData.ObjectType);
                    return null;
            }
        }
    }
}
