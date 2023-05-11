using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class LootLibrary : SingletonMonoBehavior<LootLibrary>
{
    [SerializeField] private InventoryPoolObject m_Tier1PoolObject = null;
    

    public List<Item> ItemRequest(ItemPoolType poolType,ItemDraw itemDraw)
    {
        List<Item> inventoryObject = new List<Item>();
        
        switch (poolType)
        {
            case ItemPoolType.Tier1Items:
                inventoryObject = m_Tier1PoolObject.Draw(itemDraw);
                break;
            case ItemPoolType.None:
                break;
            default:
                Debug.LogError("Item Pool Type has not been set up" + poolType);
                break;
        }

        return inventoryObject;
    }
    
    public List<Item> ItemRequest(InventoryPoolObject objectPool,ItemDraw itemDraw)
    {
        List<Item> inventoryObjects = objectPool.Draw(itemDraw);
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
    [SerializeField] private StaticWeightElementDraw<InventoryItemData> m_ObjectDataPool = null;
    [SerializeField] private WeightEnumDraw<Rarity> m_RarityDraw = null;
    public List<Item> Draw(ItemDraw itemDraw)
    {
        List<Item> itemDrawn = new List<Item>();

        for (int i = 0; i < itemDraw.DrawCount; i++)
        {
            float shouldDraw = Random.Range(0, 100);

            if (shouldDraw < itemDraw.DrawChance)
            {
                Item item = m_ObjectDataPool.Draw().ToInventoryObject();
                itemDrawn.Add(item);

                //Additional Modifier based on rarity drawn
                Debug.Log("Try set rarity");
                if (item.Data.ObjectType == ObjectType.Equipement && item.Data.Rarity == Rarity.Null)
                {
                    Rarity rarityDrawn = m_RarityDraw.Draw();
                    ((EquipementItem)item).InitializeRarity(rarityDrawn,RarityLibrary.Instance.GetParametersViaKey(rarityDrawn));
                    
                }
            }
        }

        return itemDrawn;
    }
}


[System.Serializable]
public struct ItemDraw
{
    public int DrawCount;
    [Range(0,100f)]
    public float DrawChance;

    public ItemDraw(int drawCount, float drawChance)
    {
        DrawCount = drawCount;
        DrawChance = drawChance;
    }
}