using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class LootLibrary : SingletonMonoBehavior<LootLibrary>
{
    [SerializeField] private InventoryPoolObject m_Tier1PoolObject = null;

    private InventoryObject ToInventoryObject(InventoryObjectData objectData)
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

    public List<InventoryObject> ItemRequest(ItemPoolType poolType,int drawCount)
    {
        List<InventoryObjectData> inventoryObjectDatas = new List<InventoryObjectData>();
        
        switch (poolType)
        {
            case ItemPoolType.Tier1Items:
                inventoryObjectDatas = m_Tier1PoolObject.Draw(drawCount);
                break;
            case ItemPoolType.None:
                break;
            default:
                Debug.LogError("Item Pool Type has not been set up" + poolType);
                break;
        }

        return inventoryObjectDatas.Select(ToInventoryObject).ToList();
    }
}

public enum ItemPoolType
{
    Tier1Items,
    None,
}

[System.Serializable]
public class InventoryPoolObject
{
    [SerializeField] private WeightElementDraw<InventoryObjectData> m_ObjectDataPool = null;
    [Range(0,100)]
    [SerializeField] private float m_DrawChance = 50f;

    public List<InventoryObjectData> Draw(int drawCount)
    {
        List<InventoryObjectData> itemDrawn = new List<InventoryObjectData>();

        for (int i = 0; i < drawCount; i++)
        {
            float shouldDraw = Random.Range(0, 100);

            if (shouldDraw < m_DrawChance)
            {
                itemDrawn.Add(m_ObjectDataPool.Draw());
            }
        }

        return itemDrawn;
    }
}