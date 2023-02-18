using System.Collections.Generic;
using UnityEngine;


public class ZoneTileManager : SingletonMonoBehavior<ZoneTileManager>
{
    //Maybe need an additional Vector2Int parameter use for spell target selection when two position are needed//
    //ex : Mouse To Player
    public List<Vector2Int> GetSelectionZone(ZoneSelection zoneOption,Vector2Int origin,int range)
    {
        List<Vector2Int> zones = new List<Vector2Int>();

        switch (zoneOption.DisplayType)
        {
            case ZoneType.Square:
                //Square Display
                for (int x = -range + 1; x <range; x++)
                {
                    for (int y = -range + 1; y < range ; y++)
                    {
                        zones.Add(new Vector2Int(x,y) + origin);
                    }
                }
                break;
            case ZoneType.Circle:
                //Circle Display
                Vector2Int middleZone = Vector2Int.zero;
                
                for (int x = -range + 1; x < range; x++)
                {
                    for (int y = -range + 1; y < range; y++)
                    {
                        Vector2Int vec = new Vector2Int(x, y);
                        if (Vector2Int.Distance(middleZone,vec) <= range - 0.66f)
                        {
                            zones.Add(vec + origin);
                        }
                    }
                }
                break;
            case ZoneType.MouseToPlayer:
                //Player To Mouse Display //
                List<Vector2Int> playerToMouse = LinePath.GetPathTile(GameManager.Instance.ControlledEntity.EntityPosition,origin).ToPath();

                foreach (Vector2Int pathPoint in playerToMouse)
                {
                    zones.Add(pathPoint);
                }
                break;
            default:
                break;
        }
        
        return zones;
    }
}
