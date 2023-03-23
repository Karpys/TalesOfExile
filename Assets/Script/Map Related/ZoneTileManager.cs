using System.Collections.Generic;
using UnityEngine;


public static class ZoneTileManager
{
    //Maybe need an additional Vector2Int parameter use for spell target selection when two position are needed//
    //ex : Mouse To Player
    private const float CIRCLE_TOLERANCE = 0.66f;
    public static List<Vector2Int> GetSelectionZone(ZoneSelection zoneOption,Vector2Int selectionOrigin,int range,Vector2Int? castOrigin = null)
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
                        if (Vector2Int.Distance(middleZone,vec) <= range - CIRCLE_TOLERANCE)
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
            case ZoneType.OuterCircle:
                Vector2Int originCircle = Vector2Int.zero;
                
                for (int x = -range + 1; x < range; x++)
                {
                    for (int y = -range + 1; y < range; y++)
                    {
                        Vector2Int vec = new Vector2Int(x, y);
                        if (Vector2Int.Distance(originCircle,vec) <= range - CIRCLE_TOLERANCE && Vector2Int.Distance(originCircle,vec) >= range - 1 - CIRCLE_TOLERANCE)
                        {
                            zones.Add(vec + selectionOrigin);
                        }
                    }
                }
                break;
            case ZoneType.OuterSquare:
                for (int x = -range + 1; x <range; x++)
                {
                    for (int y = -range + 1; y < range ; y++)
                    {
                        if (x == -range + 1 || y == -range + 1 || x == range - 1 || y == range - 1)
                        {
                            zones.Add(new Vector2Int(x,y) + selectionOrigin);
                        }
                    }
                }
                break;
            default:
                Debug.LogError("Zone selection type not register");
                break;
        }
        
        return zones;
    }

    public static bool IsInRange(TriggerSpellData spellData, Vector2Int castPosition)
    {
        ZoneSelection zoneSelection = spellData.GetMainSelection();
        
        if (zoneSelection == null)
            return true;
        
        Vector2Int origin = spellData.AttachedEntity.EntityPosition;

        switch (zoneSelection.DisplayType)
        {
            case ZoneType.Square:
                int diffX = Mathf.Abs(origin.x - castPosition.x);
                int diffY = Mathf.Abs(origin.y - castPosition.y);
                int biggestDiff = diffX;

                if (diffY > biggestDiff)
                    biggestDiff = diffY;

                if (zoneSelection.Range - 1 >= biggestDiff)
                    return true;

                return false;
            
            case ZoneType.Circle:
                if (Vector2Int.Distance(origin, castPosition) <= zoneSelection.Range -CIRCLE_TOLERANCE)
                {
                    return true;
                }
                return false;
            default:
                return false;
        }
    }
}
