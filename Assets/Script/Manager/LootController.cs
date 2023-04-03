using System.Collections.Generic;
using TweenCustom;
using UnityEngine;

public class LootController : SingletonMonoBehavior<LootController>
{
    [SerializeField] private InventoryObjectHolder m_BaseInventoryHolder = null;
    [SerializeField] private float m_YJumpForce = 1;
    [SerializeField] private float m_YJumpDuration = 1;
    [SerializeField] private float m_XMovementDuration = 1;

    public void SpawnLootFrom(List<InventoryObject> inventoryObjects,Tile originTile,List<Tile> tiles)
    {
        int tilesCount = tiles.Count;
        foreach (InventoryObject inventoryObject in inventoryObjects)
        {
            float delay = 0;
            foreach (Tile tile in tiles)
            {
                //Tile targetTile = tiles[Random.Range(0, tilesCount - 1)];
                Tile targetTile = tile;
                InventoryObjectHolder worldHolder = Instantiate(m_BaseInventoryHolder,originTile.WorldTile.transform.position,Quaternion.identity,MapData.Instance.transform);
            
                worldHolder.InitalizeHolder(inventoryObject);
                LootJumpTo(worldHolder,targetTile,delay);
                delay += 0.1f;
            }
        }
    }

    private void LootJumpTo(InventoryObjectHolder worldHolder,Tile tile,float delay)
    {
        Vector3 worldTilePosition = tile.WorldTile.transform.position;
        worldHolder.transform.DoMove(worldTilePosition, m_XMovementDuration).SetDelay(delay).OnStart(worldHolder.DisplayWorldVisual);
        worldHolder.JumpHolder.transform.DoLocalMove(new Vector3(0,m_YJumpForce,0),m_YJumpDuration/2).OnComplete(() =>
        {
            worldHolder.JumpHolder.transform.DoLocalMove(Vector3.zero, m_YJumpDuration / 2);
        }).SetDelay(delay);
    }
}