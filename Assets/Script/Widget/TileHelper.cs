
using System;
using System.Collections.Generic;
using UnityEngine;

public static class TileHelper
{
    private static Vector2Int[] DirectionalCheck = new Vector2Int[]
    {
        new Vector2Int(0, 1),
        new Vector2Int(1, 0),
        new Vector2Int(0, -1),
        new Vector2Int(-1, 0),
    };
    
    public static Vector2Int GetOppositePosition(Vector2Int pivotPos, Vector2Int oppositeBasePos)
    {
        Vector2Int pivoToOppo = new Vector2Int(oppositeBasePos.x - pivotPos.x, oppositeBasePos.y - pivotPos.y);
        return pivotPos - pivoToOppo;
    }
    
    public static List<Tile> GetNeighbours(Tile tile,NeighbourType type,MapData mapData)
    {
        List<Tile> neighbours = new List<Tile>();

        if (type == NeighbourType.Square)
        {
            GetSquareNeighbours(tile,neighbours,mapData);
        }else if (type == NeighbourType.Cross)
        {
            GetCrossNeighbours(tile,neighbours,mapData);
        }
        
        return neighbours;
    }

    public static Tile GetFreeClosestAround(Tile aroundTile,Vector3 entityWorldPosition)
    {
        List<Tile> neighbours = GetNeighbours(aroundTile,NeighbourType.Square, MapData.Instance);

        for (int i = neighbours.Count - 1; i >= 0; i--)
        {
            if (!neighbours[i].Walkable)
            {
                neighbours.RemoveAt(i);
            }
        }

        if (neighbours.Count == 0)
        {
            Debug.LogError("NO FREE WALKABLE TILE ? ERROR");
            return null;
        }

        Tile closest = neighbours[0];
        float minDistance = Vector3.Distance(closest.WorldTile.transform.position,
            entityWorldPosition);
        
        for(int i = 1; i < neighbours.Count; i++)
        {
            float distance = Vector3.Distance(neighbours[i].WorldTile.transform.position,entityWorldPosition);

            if (distance < minDistance)
            {
                minDistance = distance;
                closest = neighbours[i];
            }
        }

        return closest;
    }

    private static void GetCrossNeighbours(Tile tile, List<Tile> neighbours,MapData mapData)
    {
        for (int x = -1; x <= 1; x++)
        {
            if(x == 0)
                continue;
            
            int checkX = tile.XPos + x;
            if (checkX >= 0 && checkX < mapData.Map.Width)
            {
                neighbours.Add(mapData.Map.Tiles[checkX,tile.YPos]);
            }
        }
        
        for (int y = -1; y <= 1; y++)
        {
            if(y == 0)
                continue;
            
            int checkY = tile.YPos + y;
            if (checkY >= 0 && checkY < mapData.Map.Height)
            {
                neighbours.Add(mapData.Map.Tiles[tile.XPos,checkY]);
            }
        }
    }

    private static void GetSquareNeighbours(Tile tile, List<Tile> neighbours,MapData mapData)
    {
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if(x == 0 && y == 0)
                    continue;

                int checkX = tile.XPos + x;
                int checkY = tile.YPos + y;

                if (checkX >= 0 && checkX < mapData.Map.Width && checkY >= 0 && checkY < mapData.Map.Height)
                {
                    neighbours.Add(mapData.Map.Tiles[checkX,checkY]); 
                }
            }
        }
    }
    

    public static Vector2Int GetOppositePositionFrom(Vector2Int currentTile, Vector2Int tileFrom)
    {
        if (currentTile == tileFrom)
        {
            Debug.LogError("GetOppositePositionFrom : Method call with same position");   
            return currentTile;
        }
        
        List<Tile> path = LinePath.GetPathTile(currentTile, tileFrom);
        Vector2Int oppositePosition = Vector2Int.zero;
        
        if (path.Count > 0)
        {
            oppositePosition = GetOppositePosition(currentTile, path[0].TilePosition);
        }
        else
        {
            oppositePosition = GetOppositePosition(currentTile, tileFrom);
        }

        return oppositePosition;
    }
    
    private static int GetBitMaskingValue(WorldTile tile,List<WorldTile> tiles,MapData mapData)
    {
        string bitMask = "";
        
        for (int i = DirectionalCheck.Length - 1; i >= 0; i--)
        {
            int checkX = tile.Tile.XPos + DirectionalCheck[i].x;
            int checkY = tile.Tile.YPos + DirectionalCheck[i].y;
            
            if (checkX >= 0 && checkX < mapData.Map.Width && checkY >= 0 && checkY < mapData.Map.Height)
            {
                if (tiles.Contains(mapData.Map.Tiles[checkX, checkY].WorldTile))
                {
                    bitMask += "1";
                    continue;
                }
            }

            bitMask += "0";
        }
        return Convert.ToInt32(bitMask, 2);
    }

    public static void GenerateTileSet(List<WorldTile> tiles,Sprite[] tileMap,MapData mapData)
    {
        foreach (WorldTile tile in tiles)
        {
            tile.GetComponentInChildren<SpriteRenderer>().sprite = tileMap[GetBitMaskingValue(tile, tiles, mapData)];
        }
    }
    
    public static List<Vector2Int> ToPath(this List<Tile> tiles)
    {
        List<Vector2Int> path = new List<Vector2Int>();

        for (int i = 0; i < tiles.Count; i++)
        {
            path.Add(tiles[i].TilePosition);
        }

        return path;
    }
}
