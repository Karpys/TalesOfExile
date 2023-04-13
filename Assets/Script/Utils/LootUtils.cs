using UnityEngine;

public static class LootUtils
{
    public static InventoryObject ToInventoryObject(this InventoryObjectData objectData)
    {
        switch (objectData.ObjectType)
        {
            case ObjectType.DefaultObject:
                return new DefaultInventoryObject(objectData);
            case ObjectType.Equipement:
                return new EquipementObject(objectData as EquipementObjectData);
            default:
                Debug.LogError("Equipement Object Data type not set up" + objectData.ObjectType);
                return null;
        }
    }
}
