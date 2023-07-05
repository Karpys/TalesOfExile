using System.Collections.Generic;
using System.Linq;
using KarpysDev.Script.Manager;
using KarpysDev.Script.PathFinding;
using KarpysDev.Script.PathFinding.LinePath;
using KarpysDev.Script.Spell;
using KarpysDev.Script.Utils;
using UnityEngine;

namespace KarpysDev.Script.Map_Related
{
    public static class ZoneTileManager
    {
        //Maybe need an additional Vector2Int parameter use for spell target selection when two position are needed//
        //ex : Mouse To Player
    
        private static Vector2[] coneAdjacents = new Vector2[8]
        {
            new Vector2(-1, -1),
            new Vector2(-1.5f, 0),
            new Vector2(-1, 1),
            new Vector2(0, -1.5f),
            new Vector2(0, 1.5f),
            new Vector2(1, -1),
            new Vector2(1.5f, 0),
            new Vector2(1, 1)
        };
        
        private static float[] circleTolerances = new float[9]
        {
            .66f,
            .66f,
            .66f,
            .66f,
            .5f,
            .5f,
            .5f,
            .66f,
            .66f,
        };
    
        private const float CIRCLE_TOLERANCE = 0.66f;
        public static List<Vector2Int> GetSelectionZone(Zone zoneOption,Vector2Int selectionOrigin,int range,Vector2Int? castOrigin = null)
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
                    float circleTolerance = circleTolerances[Mathf.Min(range,8)];
                
                    for (int x = -range + 1; x < range; x++)
                    {
                        for (int y = -range + 1; y < range; y++)
                        {
                            Vector2Int vec = new Vector2Int(x, y);
                            if (Vector2Int.Distance(middleZone,vec) <= range - circleTolerance)
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
                        LinePath.NeighbourType = NeighbourType.Cross;
                        List<Vector2Int> playerToMouse = Bresenhams.GetPath(castOrigin.Value, selectionOrigin);
                        LinePath.NeighbourType = NeighbourType.Square;
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
                        List<Vector2Int> playerToMouse = LinePath.GetPathTile(castOrigin.Value,selectionOrigin,NeighbourType.Square);
                    
                        for (int i = 0; i < playerToMouse.Count; i++)
                        {
                            Vector2Int pathPoint = playerToMouse[i];
                            if (!zones.Contains(pathPoint))
                                zones.Add(pathPoint);

                            if (i >= playerToMouse.Count - range + 1)
                                continue;
                        
                            List<Vector2Int> pathLenght = GetSelectionZone(new Zone(ZoneType.Square, range), pathPoint, range);
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
                    float circleT = circleTolerances[Mathf.Min(range,8)];
                
                    for (int x = -range + 1; x < range; x++)
                    {
                        for (int y = -range + 1; y < range; y++)
                        {
                            Vector2Int vec = new Vector2Int(x, y);
                            if (Vector2Int.Distance(originCircle,vec) <= range  && Vector2Int.Distance(originCircle,vec) >= range - 1 - circleT)
                            {
                                zones.Add(vec + selectionOrigin);
                            }
                        }
                    }
                    break;
                case ZoneType.OuterSquare:
                    for (int x = -range + 1; x < range; x++)
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
                case ZoneType.Cone:
                    //Circle Selection
                    float angle = 90;
                    Vector2Int middle = Vector2Int.zero;
                    float coneCircleTolerance = circleTolerances[Mathf.Min(range,8)];
                
                    Vector2 nearest = coneAdjacents.OrderBy(a => Vector2.Distance(selectionOrigin, castOrigin.Value + a)).First();
                    selectionOrigin = castOrigin.Value + new Vector2Int(Mathf.FloorToInt(nearest.x),Mathf.FloorToInt(nearest.y));
                
                    for (int x = -range + 1; x < range; x++)
                    {
                        for (int y = -range + 1; y < range; y++)
                        {
                            Vector2Int offset = new Vector2Int(x, y);
                        
                            if (Vector2Int.Distance(middle,offset) > range - coneCircleTolerance)
                                continue; 
                        
                            Vector2 direction = (selectionOrigin - castOrigin.Value);
                            direction = direction.normalized;
                            float currentAngle = Vector2.SignedAngle(offset, direction);

                            if (Mathf.Abs(currentAngle) <= angle / 2)
                            {
                                zones.Add(offset + castOrigin.Value);
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

        public static bool IsInRange(Vector2Int origin, Vector2Int castPosition,Zone zoneSelection)
        {
            if (zoneSelection == null)
                return true;
        
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
                    
                    float circleTolerance = circleTolerances[Mathf.Min(zoneSelection.Range,8)];
                    
                    if (Vector2Int.Distance(origin, castPosition) <= zoneSelection.Range - circleTolerance)
                    {
                        return true;
                    }
                    return false;
                case ZoneType.Cone:
                    float coneCircleTolerance = circleTolerances[Mathf.Min(zoneSelection.Range,8)];
                    //Same as circle//
                    if (Vector2Int.Distance(origin, castPosition) <= zoneSelection.Range -coneCircleTolerance)
                    {
                        return true;
                    }
                    return false;
                default:
                
                    Debug.LogError("Target selection display type has not been set up: " + zoneSelection.DisplayType);
                    return false;
            }
        }
    
        public static ZoneType GetOuter(ZoneType type)
        {
            switch (type)
            {
                case ZoneType.Circle:
                    return ZoneType.OuterCircle;
                case ZoneType.Square:
                    return ZoneType.OuterSquare;
                default:
                    return ZoneType.OuterSquare;
            }
        }
    }
}
