using System;
using System.Collections.Generic;
using KarpysDev.Script.Items;
using KarpysDev.Script.Utils;
using KarpysDev.Script.Widget;
using UnityEngine;
using Random = UnityEngine.Random;

namespace KarpysDev.Script.Manager.Library
{
    public class LootLibrary : SingletonMonoBehavior<LootLibrary>
    {
        [SerializeField] private GenericLibrary<InventoryPoolObjectScriptable,ItemPoolType> m_PoolObjects = null;
        
        public List<Item> ItemRequest(ItemPoolType poolType,ItemDraw itemDraw)
        {
            List<Item> inventoryObject = new List<Item>();

            InventoryPoolObjectScriptable poolObject = m_PoolObjects.GetViaKey(poolType);

            if (poolObject != null)
            {
                inventoryObject = poolObject.Draw(itemDraw);
            }
            else
            {
                Debug.LogError("Item Pool Type has not been set up" + poolType);
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
        None = 0,
        Tier0Items = 1,
        Tier1Items = 2,
    }

    [Serializable]
    public class InventoryPoolObject
    {
        [SerializeField] private StaticWeightElementDraw<InventoryItemData> m_ObjectDataPool = null;
        [SerializeField] private WeightElementDraw<Rarity> m_RarityDraw = null;
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


    [Serializable]
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
}