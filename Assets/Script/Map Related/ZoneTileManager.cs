using System.Collections.Generic;
using UnityEngine;


public class ZoneTileManager : SingletonMonoBehavior<ZoneTileManager>
{
    //Maybe need an additional Vector2Int parameter use for spell target selection when two position are needed//
    //ex : Mouse To Player
    public List<Vector2Int> GetSelectionZone(ZoneSelection zoneOption,Vector2Int selectionOrigin,int range,Vector2Int? castOrigin = null)
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
                        zones.Add(new Vector2Int(x,y) + selectionOrigin);
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
                            zones.Add(vec + selectionOrigin);
                        }
                    }
                }
                break;
            case ZoneType.PlayerToMouse:
                //Player To Mouse Display //
                //Need to set the cast origin
                if (castOrigin.HasValue)
                {
                    List<Vector2Int> playerToMouse = LinePath.GetPathTile(castOrigin.Value,selectionOrigin).ToPath();
                    foreach (Vector2Int pathPoint in playerToMouse)
                    {
                        zones.Add(pathPoint);
                    }
                }
                else
                {
                    Debug.LogError("Need a cast origin Position");
                }
                break;
            case ZoneType.PlayerToMouseSquareRange:
                //Player To Mouse Display //
                //Need to set the cast origin
                if (castOrigin.HasValue)
                {
                    List<Vector2Int> playerToMouse = LinePath.GetPathTile(castOrigin.Value,selectionOrigin).ToPath();
                    
                    for (int i = 0; i < playerToMouse.Count; i++)
                    {
                        Vector2Int pathPoint = playerToMouse[i];
                        if (!zones.Contains(pathPoint))
                            zones.Add(pathPoint);

                        if (i >= playerToMouse.Count - range + 1)
                            continue;
                        
                        List<Vector2Int> pathLenght = GetSelectionZone(new ZoneSelection(ZoneType.Square, range), pathPoint, range);
                        foreach (Vector2Int pathAdd in pathLenght)
                        {
                            if (!zones.Contains(pathAdd))
                                zones.Add(pathAdd);
                        }
                    }
                }
                else
                {
                    Debug.LogError("Need a cast origin Position");
                }
                break;
            default:
                break;
        }
        
        return zones;
    }
}
