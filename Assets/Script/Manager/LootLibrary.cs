using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class LootLibrary : SingletonMonoBehavior<LootLibrary>
{
    [SerializeField] private InventoryPoolObject m_Tier1PoolObject = null;
    

    public List<InventoryObject> ItemRequest(ItemPoolType poolType,int drawCount)
    {
        List<InventoryObject> inventoryObject = new List<InventoryObject>();
        
        switch (poolType)
        {
            case ItemPoolType.Tier1Items:
                inventoryObject = m_Tier1PoolObject.Draw(drawCount);
                break;
            case ItemPoolType.None:
                break;
            default:
                Debug.LogError("Item Pool Type has not been set up" + poolType);
                break;
        }

        return inventoryObject;
    }
    
    public List<InventoryObject> ItemRequest(InventoryPoolObject objectPool,int drawCount)
    {
        List<InventoryObject> inventoryObjects =  objectPool.Draw(drawCount);
        return inventoryObjects;
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
    [SerializeField] private StaticWeightElementDraw<InventoryObjectData> m_ObjectDataPool = null;
    [Range(0,100)]
    [SerializeField] private float m_DrawChance = 50f;
    [SerializeField] private WeightEnumDraw<Rarity> m_RarityDraw = null;
    public List<InventoryObject> Draw(int drawCount)
    {
        List<InventoryObject> itemDrawn = new List<InventoryObject>();

        for (int i = 0; i < drawCount; i++)
        {
            float shouldDraw = Random.Range(0, 100);

            if (shouldDraw < m_DrawChance)
            {
                InventoryObject item = m_ObjectDataPool.Draw().ToInventoryObject();
                itemDrawn.Add(item);

                //Additional Modifier based on rarity drawn
                if (item.Data.ObjectType == ObjectType.Equipement && item.Data.Rarity == Rarity.Null)
                {
                    
                }
            }
        }

        return itemDrawn;
    }
}