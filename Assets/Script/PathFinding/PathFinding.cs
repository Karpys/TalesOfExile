using System;
using System.Collections.Generic;
using UnityEngine;


public enum NeighbourType
{
    Square,
    Cross
}
public static class PathFinding
{
    public static MapData mapData = null;
    public static NeighbourType NeighbourType = NeighbourType.Square;
    public static Tile FindClosestTile(Vector2Int startPos, Vector2Int playerPos,bool ignoreWall = false)
    {
        Tile startTile = mapData.Map.Tiles[startPos.x, startPos.y];
        Tile playerTile = mapData.Map.Tiles[playerPos.x, playerPos.y];

        return FindClosestTile(startTile, playerTile,ignoreWall);
    }
    public static Tile FindClosestTile(Tile startTile, Tile playerTile,bool ignoreWall = false)
    {
        Tile tile = FindLastTile(startTile, playerTile,ignoreWall);
        Tile currentTile = tile;

        if (currentTile == startTile)
            return currentTile;
        while (currentTile.ParentTile != startTile)
        {
            currentTile = currentTile.ParentTile;
        }

        List<Vector2Int> path = new List<Vector2Int>();
        path.Add(new Vector2Int(currentTile.XPos,currentTile.YPos));
        return currentTile;
    }


    public static List<Tile> FindTilePath(Vector2Int startPos, Vector2Int playerPos,bool ignoreWall = false)
    {
        Tile startTile = mapData.Map.Tiles[startPos.x, startPos.y];
        Tile playerTile = mapData.Map.Tiles[playerPos.x, playerPos.y];

        return FindTilePath(startTile, playerTile,ignoreWall);
    }

    public static List<Tile> FindTilePath(Tile startTile, Tile playerTile,bool ignoreWall = false)
    {
        Tile lastTile = FindLastTile(startTile, playerTile,ignoreWall);

        if (ReferenceEquals(lastTile,null))
            return null;
        
        List<Tile> path = new List<Tile>();
        
        Tile currentTile = lastTile;

        if (currentTile == startTile)
        {
            path.Add(playerTile);
            return path;
        }
        
        while (currentTile != startTile)
        {
            path.Add(currentTile);
            currentTile = currentTile.ParentTile;
        }

        path.Reverse();
        path.Add(playerTile);
        return path;
    }


    public static List<Vector2Int> FindPath(Vector2Int startPos, Vector2Int playerPos, bool ignoreWall = false)
    {
        Tile startTile = mapData.Map.Tiles[startPos.x, startPos.y];
        Tile playerTile = mapData.Map.Tiles[playerPos.x, playerPos.y];

        return FindPath(startTile, playerTile, ignoreWall);
    }
    public static List<Vector2Int> FindPath(Tile startTile, Tile playerTile, bool ignoreWall = false)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        List<Tile> tilePath = FindTilePath(startTile, playerTile, ignoreWall);

        if (tilePath == null)
            return null;

        foreach (Tile tile in tilePath)
        {
            path.Add(new Vector2Int(tile.XPos,tile.YPos));
        }

        return path;
    }

    public static Tile FindLastTile(Vector2Int startPos, Vector2Int playerPos,bool ignoreWall = false)
    {
        Tile startTile = mapData.Map.Tiles[startPos.x, startPos.y];
        Tile playerTile = mapData.Map.Tiles[playerPos.x, playerPos.y];
        
        return FindLastTile(startTile, playerTile,ignoreWall);
    }
    
    //Core//
    public static Tile FindLastTile(Tile startTile, Tile playerTile,bool ignoreWall = false)
    {
        List<Tile> openSet = new List<Tile>();
        HashSet<Tile> closeSet = new HashSet<Tile>();
        openSet.Add(startTile);

        while (openSet.Count > 0)
        {
            Tile currentTile = openSet[0];

            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentTile.fCost ||
                    openSet[i].fCost == currentTile.fCost && openSet[i].hCost < currentTile.hCost)
                {
                    currentTile = openSet[i];
                }
            }

            openSet.Remove(currentTile);
            closeSet.Add(currentTile);

            /*if (currentTile == playerTile)
            {
                DebugTile(startTile,currentTile);
                return currentTile;
            }*/
            
            List<Tile> neighbours = TileHelper.GetNeighbours(currentTile, NeighbourType, mapData);

            foreach (Tile neighbour in neighbours)
            {
                //Player Not Walkable
                if (neighbour == playerTile)
                {
                    /*DebugTile(startTile,currentTile);*/
                    return currentTile;
                }
                
                if (!neighbour.Walkable && !ignoreWall || closeSet.Contains(neighbour))
                    continue;

                float newMovementCost = currentTile.gCost; /*+ GetDistance(currentTile, neighbour, startTile);*/
                if (newMovementCost < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCost;/*newMovementCost;*/
                    neighbour.hCost = GetDistance(neighbour, playerTile,startTile);
                    neighbour.ParentTile = currentTile;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }

        return startTile;
    }

    /*private void DebugTile(Tile start,Tile end)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        Tile currentTile = end;

        while (currentTile != start)
        {
            path.Add(new Vector2Int(currentTile.XPos,currentTile.YPos));
            currentTile = currentTile.ParentTile;
        }
        
        HighlightTilesManager.Instance.GenerateHighlightTiles(path);
    }*/

    public static float GetDistance(Tile currentTile, Tile tileEnd,Tile startTile)
    {
        //Source//
        //http://theory.stanford.edu/~amitp/GameProgramming/Heuristics.html//
        //Base
        //Euclidian Distance Perfect Result.. Higher Cost But thats exactly what i was thinking ^^//
        /*float distX = Mathf.Abs(currentTile.XPos - tileEnd.XPos);
        float distY = Mathf.Abs(currentTile.YPos - tileEnd.YPos);
        
        return (float)Math.Sqrt(distX * distX + distY * distY);*/



        /*Heuristics GOOD : D1 = 1 and D2 = 1.01f*/
        float distX = Mathf.Abs(currentTile.XPos - tileEnd.XPos);
        float distY = Mathf.Abs(currentTile.YPos - tileEnd.YPos);
        float D1 = 1;
        float D2 = 1.01f;
        float heuristic = D1 * (distX + distY) + (D2 - 2 * D1) * Math.Min(distX, distY);
       
        float dx1 = currentTile.XPos - tileEnd.XPos;
        float dy1 = currentTile.YPos - tileEnd.YPos;
        float dx2 = startTile.XPos - tileEnd.XPos;
        float dy2 = startTile.YPos - tileEnd.YPos;
        float cross = Math.Abs(dx1 * dy2 - dx2 * dy1);
        heuristic += cross * 0.001f;
        return heuristic;


        /*if (distX > distY)
            return D1 * (distX - distY) + D2 * distY;
         return D1 * (distY - distX) + D2 * distX;*/
    }
}
