
using System;
using System.Collections.Generic;
using UnityEngine;

public static class LinePath
{
    public static MapData mapData = null;
    public static NeighbourType NeighbourType = NeighbourType.Square;
    
    public static List<Tile> GetPathTile(Vector2Int from, Vector2Int to)
    {
        Tile fromTile = mapData.Map.Tiles[from.x, from.y];
        Tile toTile = mapData.Map.Tiles[to.x, to.y];

        return GetPathTile(fromTile, toTile);
    }
    
    public static List<Tile> GetPathTile(Vector2Int from, Vector2Int to,out List<Tile> roundTiles)
    {
        Tile fromTile = mapData.Map.Tiles[from.x, from.y];
        Tile toTile = mapData.Map.Tiles[to.x, to.y];

        return GetPathTile(fromTile, toTile,out roundTiles);
    }

    public static List<Tile> GetPathTile(Tile from, Tile to)
    {
        List<Tile> path = new List<Tile>();
        Vector2Int fromPosition = from.TilePosition;
        Vector2Int toPosition = to.TilePosition;

        Vector2Int Vec = toPosition - fromPosition;
        

        if (Mathf.Abs(Vec.y) > Mathf.Abs(Vec.x))
        {
            path = AroundY(path,fromPosition, toPosition, Vec);
        }
        else if (Mathf.Abs(Vec.x) > Mathf.Abs(Vec.y))
        {
            path = AroundX(path,fromPosition, toPosition, Vec);
        }
        else
        {
            path = AroundDiagonal(path, fromPosition, toPosition, Vec);
        }

        return path;
    }
    
    public static List<Tile> GetPathTile(Tile from, Tile to,out List<Tile> roundedTile)
    {
        List<Tile> path = new List<Tile>();
        roundedTile = new List<Tile>();
        Vector2Int fromPosition = from.TilePosition;
        Vector2Int toPosition = to.TilePosition;

        Vector2Int Vec = toPosition - fromPosition;
        

        if (Mathf.Abs(Vec.y) > Mathf.Abs(Vec.x))
        {
            path = AroundYRounded(path,fromPosition, toPosition, Vec,out roundedTile);
        }
        else if (Mathf.Abs(Vec.x) > Mathf.Abs(Vec.y))
        {
            path = AroundXRounded(path,fromPosition, toPosition, Vec,out roundedTile);
        }
        else
        {
            path = AroundDiagonal(path, fromPosition, toPosition, Vec);
        }

        return path;
    }

    private static List<Tile> AroundXRounded(List<Tile> path, Vector2Int fromPosition, Vector2Int toPosition, Vector2Int Vec, out List<Tile> roundedTile)
    {
        roundedTile = new List<Tile>();
        float currentRatio = 0.49f;
        Vector2Int dir = GetDirection(Vec);
        float ratioPerStep =  (float)Math.Abs(Vec.y) / Math.Abs(Vec.x);

        while (fromPosition != toPosition)
        {
            currentRatio += ratioPerStep;
            if (currentRatio >= 1)
            {
                if (NeighbourType == NeighbourType.Cross)
                {
                    Tile roundTile = mapData.Map.Tiles[fromPosition.x, fromPosition.y + dir.y];
                    roundedTile.Add(roundTile);
                    path.Add(roundTile);
                }
                //Move on Y Axis//
                fromPosition += dir;
                currentRatio -= 1;
            }
            else
            {
                fromPosition += new Vector2Int(dir.x,0);
            }
            path.Add(mapData.Map.Tiles[fromPosition.x,fromPosition.y]);
        }

        return path;
    }

    private static List<Tile> AroundYRounded(List<Tile> path, Vector2Int fromPosition, Vector2Int toPosition, Vector2Int Vec, out List<Tile> roundedTile)
    {
        roundedTile = new List<Tile>();
        
        float currentRatio = 0.49f;
        Vector2Int dir = GetDirection(Vec);
        float ratioPerStep = (float)Math.Abs(Vec.x) / Math.Abs(Vec.y);

        while (fromPosition != toPosition)
        {
            currentRatio += ratioPerStep;

            
            if (currentRatio >= 1)
            {
                if (NeighbourType == NeighbourType.Cross)
                {
                    Tile roundTile = mapData.Map.Tiles[fromPosition.x + dir.x, fromPosition.y]; 
                    roundedTile.Add(roundTile);
                    path.Add(roundTile);
                }
                //Move on X Axis//
                fromPosition += dir;
                currentRatio -= 1;
            }
            else
            {
                fromPosition += new Vector2Int(0,dir.y);
            }
            path.Add(mapData.Map.Tiles[fromPosition.x,fromPosition.y]);
        }

        return path;
    }


    private static List<Tile> AroundDiagonal(List<Tile> path, Vector2Int fromPosition, Vector2Int toPosition, Vector2Int Vec)
    {
        Vector2Int dir = GetDirection(Vec);
        
        while (fromPosition != toPosition)
        {
            fromPosition += dir;
            path.Add(mapData.Map.Tiles[fromPosition.x,fromPosition.y]);
        }

        return path;
    }

    private static List<Tile> AroundY(List<Tile> path,Vector2Int fromPosition, Vector2Int toPosition, Vector2Int Vec)
    {
        float currentRatio = 0.49f;
        Vector2Int dir = GetDirection(Vec);
        float ratioPerStep = (float)Math.Abs(Vec.x) / Math.Abs(Vec.y);

        while (fromPosition != toPosition)
        {
            currentRatio += ratioPerStep;

            
            if (currentRatio >= 1)
            {
                if(NeighbourType == NeighbourType.Cross)
                    path.Add(mapData.Map.Tiles[fromPosition.x  + dir.x,fromPosition.y]);
                //Move on X Axis//
                fromPosition += dir;
                currentRatio -= 1;
            }
            else
            {
                fromPosition += new Vector2Int(0,dir.y);
            }
            path.Add(mapData.Map.Tiles[fromPosition.x,fromPosition.y]);
        }

        return path;
    }

    private static List<Tile> AroundX(List<Tile> path, Vector2Int fromPosition, Vector2Int toPosition, Vector2Int Vec)
    {
        float currentRatio = 0.49f;
        Vector2Int dir = GetDirection(Vec);
        float ratioPerStep =  (float)Math.Abs(Vec.y) / Math.Abs(Vec.x);

        while (fromPosition != toPosition)
        {
            currentRatio += ratioPerStep;
            if (currentRatio >= 1)
            {
                if(NeighbourType == NeighbourType.Cross)
                    path.Add(mapData.Map.Tiles[fromPosition.x,fromPosition.y + dir.y]);
                //Move on Y Axis//
                fromPosition += dir;
                currentRatio -= 1;
            }
            else
            {
                fromPosition += new Vector2Int(dir.x,0);
            }
            path.Add(mapData.Map.Tiles[fromPosition.x,fromPosition.y]);
        }

        return path;
    }

    private static Vector2Int GetDirection(Vector2Int Vec)
    {
        return new Vector2Int((int)Mathf.Sign(Vec.x), (int)Mathf.Sign(Vec.y));
    }
}
