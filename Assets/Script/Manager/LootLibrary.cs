using UnityEngine;

public class LootLibrary : SingletonMonoBehavior<LootLibrary>
{
    [SerializeField] private InventoryObjectData m_TestObjectData = null;

    //Replace that with a real library and use new InventoryObject instead of library class//
    public InventoryObject GetDropTest()
    {
        switch (m_TestObjectData.ObjectType)
        {
            case ObjectType.DefaultObject:
                return new DefaultInventoryObject(m_TestObjectData);
            case ObjectType.Equipement:
                return new EquipementObject(m_TestObjectData as EquipementObjectData);
            default:
                Debug.LogError("Equipement Object Data type not set up" + m_TestObjectData.ObjectType);
                return null;
        }
    }
}