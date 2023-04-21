using System.Collections.Generic;
using UnityEngine;

public class SpawnSkeletonCurse : Buff
{
    private int m_SkeletonCount = 0;
    protected override void Apply()
    {
        m_Receiver.EntityEvent.OnDeath += SpawnSkeleton;
    }
    
    protected override void UnApply()
    {
        m_Receiver.EntityEvent.OnDeath -= SpawnSkeleton;
    }

    public override void InitializeBuff(BoardEntity caster, BoardEntity receiver, int cooldown, float buffValue, object[] args = null)
    {
        m_SkeletonCount = (int)args[0];
        base.InitializeBuff(caster, receiver, cooldown, buffValue, args);
    }

    private void SpawnSkeleton()
    {
        List<Tile> freeTile = TileHelper.GetNeighboursWalkable(MapData.Instance.Map.Tiles[m_Receiver.EntityPosition.x, m_Receiver.EntityPosition.y], NeighbourType.Square, MapData.Instance);
        freeTile.Add(MapData.Instance.GetTile(m_Receiver.EntityPosition));

        for (int i = 0; i < m_SkeletonCount; i++)
        {
            if(freeTile.Count <= 0)
                return;
            
            int targetTile = Random.Range(0, freeTile.Count);
            Vector2Int spawnPosition = freeTile[targetTile].TilePosition;
            
            BoardEntity entity = EntityHelper.SpawnEntityOnMap(EntityLibrary.Instance.GetEntityViaKey(EntityType.Skeleton),spawnPosition.x, spawnPosition.y, MapData.Instance);
            entity.gameObject.AddComponent<EntityLifeTurn>().SetTurnCount((int)m_BuffValue);

            freeTile.RemoveAt(targetTile);
        }
        
    }
}