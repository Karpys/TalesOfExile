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

    public static BoardEntity SpawnEntityOnMap(BoardEntity entity,int x,int y,MapData map)
    {
        BoardEntity boardEntity = GameObject.Instantiate(entity,map.transform);
        boardEntity.Place(x,y,map);
        return boardEntity;
    }
    
    public static BoardEntity GetClosestEntity(List<BoardEntity> entities,Vector2Int originPosition)
    {
        return entities.OrderBy(e => DistanceUtils.GetSquareDistance(originPosition, e.EntityPosition)).First();
    }
}
