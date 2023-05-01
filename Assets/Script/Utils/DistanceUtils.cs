using System.Collections.Generic;
using UnityEngine;

public static class DistanceUtils
{
    public static int GetSquareDistance(Vector2Int to, Vector2Int from)
    {
        int XDiff = Mathf.Abs(to.x - from.x);
        int YDiff = Mathf.Abs(to.y - from.y);

        if (XDiff > YDiff)
            return XDiff;

        return YDiff;
    }

    public static List<BoardEntity> GetZoneContactEntity(Zone contactZone, List<BoardEntity> entities, Vector2Int originPosition, int strikeCount)
    {
        List<BoardEntity> contactEntity = new List<BoardEntity>();
        Vector2Int lastPosition = originPosition;
        
        for (int i = 0; i < strikeCount; i++)
        {
            if(entities.Count <= 0)
                break;
            
            BoardEntity closestEntity = EntityHelper.GetClosestEntity(entities,lastPosition);
            
            if(!ZoneTileManager.IsInRange(lastPosition,closestEntity.EntityPosition,contactZone))
                break;

            contactEntity.Add(closestEntity);
            entities.Remove(closestEntity);
            lastPosition = closestEntity.EntityPosition;
        }

        return contactEntity;
    }
}