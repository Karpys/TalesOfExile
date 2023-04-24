using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemLibrary : SingletonMonoBehavior<ItemLibrary>
{
    [SerializeField] private InventoryItemData[] m_BaseObjectScriptable = null;

    private Dictionary<int, InventoryItemData> m_InventoryObjectDictionary = new Dictionary<int, InventoryItemData>();
    private void Awake()
    {
        foreach (InventoryItemData inventoryObject in m_BaseObjectScriptable)
        {
            m_InventoryObjectDictionary.Add(inventoryObject.UniqueId,inventoryObject);
        }
    }

    public InventoryItemData GetBaseDataViaId(int id)
    {
        m_InventoryObjectDictionary.TryGetValue(id, out InventoryItemData baseData);
        
        if(baseData == null)
            Debug.LogError("Base data id : " + id + "is not recognised");

        return baseData;
    }
}
