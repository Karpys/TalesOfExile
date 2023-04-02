using System.Collections.Generic;
using TweenCustom;
using UnityEngine;

public class LootController : SingletonMonoBehavior<LootController>
{
    [SerializeField] private InventoryObjectHolder m_BaseInventoryHolder = null;
    [SerializeField] private float m_YJumpForce = 1;
    [SerializeField] private float m_YJumpDuration = 1;
    [SerializeField] private float m_XMovementDuration = 1;

    public void SpawnLootFrom(List<InventoryObject> inventoryObjects,Vector2Int position)
    {
        Debug.Log("Entity Positon : +" + position);
        MapData mapData = MapData.Instance;

        Tile originTile = mapData.GetTile(position);
        List<Tile> neightboursWalkableTiles = TileHelper.GetNeighboursWalkable(originTile,NeighbourType.Square,mapData);
        neightboursWalkableTiles.Add(originTile);
        int tilesCount = neightboursWalkableTiles.Count;
        
        foreach (InventoryObject inventoryObject in inventoryObjects)
        {
       
            Tile targetTile = neightboursWalkableTiles[Random.Range(0, tilesCount - 1)];
            InventoryObjectHolder worldHolder = Instantiate(m_BaseInventoryHolder,originTile.WorldTile.transform.position,Quaternion.identity,MapData.Instance.transform);
        
            worldHolder.InitalizeHolder(inventoryObject);
            worldHolder.DisplayWorldVisual();
            LootJumpTo(worldHolder,targetTile);

        }
    }

    private void LootJumpTo(InventoryObjectHolder worldHolder,Tile tile)
    {
        Vector3 worldTilePosition = tile.WorldTile.transform.position;
        worldHolder.transform.DoMove(worldTilePosition, m_XMovementDuration);
        worldHolder.JumpHolder.transform.DoLocalMove(new Vector3(0,m_YJumpForce,0),m_YJumpDuration/2).OnComplete(() =>
        {
            worldHolder.JumpHolder.transform.DoLocalMove(Vector3.zero, m_YJumpDuration / 2);
        });
    }
}