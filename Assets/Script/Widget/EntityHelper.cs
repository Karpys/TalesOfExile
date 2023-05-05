using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class EntityHelper
{
    public static EntityGroup GetInverseEntityGroup(EntityGroup entityGroup)
    {
        if (entityGroup == EntityGroup.Enemy)
            return EntityGroup.Friendly;
        if (entityGroup == EntityGroup.Friendly)
            return EntityGroup.Enemy;
        return EntityGroup.Neutral;
    }

    public static BoardEntity SpawnEntityOnMap(Vector2Int pos,BoardEntity entityPrefabBase,EntityBehaviour entityIa,EntityGroup entityGroup,EntityGroup targetEntityGroup = EntityGroup.None)
    {
        MapData mapData = MapData.Instance;
        
        BoardEntity boardEntity = GameObject.Instantiate(entityPrefabBase,mapData.transform);
        boardEntity.EntityInitialization(entityIa,entityGroup,targetEntityGroup);
        boardEntity.Place(pos.x,pos.y,mapData);
        return boardEntity;
    }

    public static void SpawnEnemyViaEntitySpawn(EntitySpawn[] entitySpawn)
    {
        foreach (EntitySpawn spawn in entitySpawn)
        {
            SpawnEntityOnMap(spawn.EntityPosition, spawn.EntityPrefab, new MapEnemyEntityBehaviour(), EntityGroup.Enemy,
                EntityGroup.Friendly);
        }
    }
    
    public static BoardEntity GetClosestEntity(List<BoardEntity> entities,Vector2Int originPosition)
    {
        return entities.Where(en => en.Targetable).OrderBy(e => DistanceUtils.GetSquareDistance(originPosition, e.EntityPosition)).FirstOrDefault();
    }
}
