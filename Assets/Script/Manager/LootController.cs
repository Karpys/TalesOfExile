using UnityEngine;

public class LootController : SingletonMonoBehavior<LootController>
{
    [SerializeField] private InventoryObjectHolder m_BaseInventoryHolder = null;

    public void SpawnLootAt(InventoryObject inventoryObject,int x,int y)
    {
        InventoryObjectHolder worldHolder = Instantiate(m_BaseInventoryHolder,MapData.Instance.GetTilePosition(x, y),Quaternion.identity,MapData.Instance.transform);
        worldHolder.InitalizeHolder(inventoryObject);
        worldHolder.DisplayWorldVisual();
    }
}