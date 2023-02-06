using System.Collections.Generic;
using UnityEngine;


public class ZoneTileManager : SingletonMonoBehavior<ZoneTileManager>
{
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
            default:
                break;
        }
        
        return zones;
    }
}
