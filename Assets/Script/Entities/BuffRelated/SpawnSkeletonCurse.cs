using System.Collections.Generic;
using UnityEngine;

public class SpawnSkeletonCurse : Buff
{
    protected override void Apply()
    {
        m_Receiver.EntityEvent.OnDeath += SpawnSkeleton;
    }

    private void SpawnSkeleton()
    {
        List<Tile> freeTile = TileHelper.GetNeighboursWalkable(MapData.Instance.Map.Tiles[m_Receiver.EntityPosition.x, m_Receiver.EntityPosition.y], NeighbourType.Square, MapData.Instance);

        Vector2Int spawnPosition = freeTile[Random.Range(0, freeTile.Count)].TilePosition;
        
        EntityHelper.SpawnEntityOnMap(EntityLibrary.Instance.GetEntityViaKey(EntityType.Skeleton),spawnPosition.x, spawnPosition.y, MapData.Instance);
    }

    protected override void UnApply()
    {
        m_Receiver.EntityEvent.OnDeath -= SpawnSkeleton;
    }
}