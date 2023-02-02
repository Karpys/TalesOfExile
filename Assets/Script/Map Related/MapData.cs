using System;
using System.Collections.Generic;
using UnityEngine;

public class MapData : SingletonMonoBehavior<MapData>
{
    [SerializeField] private MapDataLibrary m_MapDataLibrary = null;
    private Map m_Map = null;
    public Map Map
    {
        get { return m_Map; }
        set { m_Map = value; }
    }
    
    public Tile FindClosestTile(Vector2Int startPos, Vector2Int playerPos)
    {
        Tile startTile = m_Map.Tiles[startPos.x, startPos.y];
        Tile playerTile = m_Map.Tiles[playerPos.x, playerPos.y];

        return FindClosestTile(startTile, playerTile);
    }
    public Tile FindClosestTile(Tile startTile, Tile playerTile)
    {
        Tile tile = FindLastTile(startTile, playerTile);
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


    public List<Tile> FindPath(Vector2Int startPos, Vector2Int playerPos)
    {
        Tile startTile = m_Map.Tiles[startPos.x, startPos.y];
        Tile playerTile = m_Map.Tiles[playerPos.x, playerPos.y];

        return FindPath(startTile, playerTile);
    }

    public List<Tile> FindPath(Tile startTile, Tile playerTile)
    {
        Tile lastTile = FindLastTile(startTile, playerTile);
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

    public Tile FindLastTile(Vector2Int startPos, Vector2Int playerPos)
    {
        Tile startTile = m_Map.Tiles[startPos.x, startPos.y];
        Tile playerTile = m_Map.Tiles[playerPos.x, playerPos.y];
        
        return FindLastTile(startTile, playerTile);
    }
    public Tile FindLastTile(Tile startTile, Tile playerTile)
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

            foreach (Tile neighbour in GetNeighbours(currentTile))
            {
                //Player Not Walkable
                if (neighbour == playerTile)
                {
                    /*DebugTile(startTile,currentTile);*/
                    return currentTile;
                }
                
                if (!neighbour.Walkable || closeSet.Contains(neighbour))
                    continue;

                float newMovementCost = currentTile.gCost + GetDistance(currentTile, neighbour,false);
                if (newMovementCost < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCost;
                    neighbour.hCost = GetDistance(neighbour, playerTile,true);
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

    private void DebugTile(Tile start,Tile end)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        Tile currentTile = end;

        while (currentTile != start)
        {
            path.Add(new Vector2Int(currentTile.XPos,currentTile.YPos));
            currentTile = currentTile.ParentTile;
        }
        
        HighlightTilesManager.Instance.GenerateHighlightTiles(path);
    }

    public float GetDistance(Tile tileStart, Tile tileEnd,bool precise)
    {
        //Source//
        //http://theory.stanford.edu/~amitp/GameProgramming/Heuristics.html//
        //Base
        int distX = Mathf.Abs(tileStart.XPos - tileEnd.XPos);
        int distY = Mathf.Abs(tileStart.YPos - tileEnd.YPos);
        
        //Heuristics GOOD : D1 = 1 and D2 = 1.01f//
        // float D1 = 1;
        // float D2 = 1.1f;
        // return D1 * (distX + distY) + (D2 - 2 * D1) * Math.Min(distX, distY);

        //Euclidian Distance Perfect Result.. Higher Cost But thats exactly what i was thinking ^^//
        return (float)Math.Sqrt(distX * distX + distY * distY);

        // if (distX > distY)
        //     return D1 * (distX - distY) + D2 * distY;
        // return D1 * (distY - distX) + D2 * distX;
    }

    public List<Tile> GetNeighbours(Tile tile)
    {
        List<Tile> neighbours = new List<Tile>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if(x == 0 && y == 0)
                    continue;

                int checkX = tile.XPos + x;
                int checkY = tile.YPos + y;

                if (checkX >= 0 && checkX < m_Map.Width && checkY >= 0 && checkY < m_Map.Height)
                {
                    neighbours.Add(m_Map.Tiles[checkX,checkY]); 
                }
            }
        }

        return neighbours;
    }

    public bool IsWalkable(int x, int y)
    {
        if (x >= 0 && x < m_Map.Width && y >= 0 && y < m_Map.Height && m_Map.Tiles[x,y].Walkable)
        {
            return true;
        }
        return false;
    }

    public bool IsWalkable(Vector2Int pos)
    {
        return IsWalkable(pos.x, pos.y);
    }

    public Vector3 GetTilePosition(int x, int y)
    {
        return new Vector3(x * m_MapDataLibrary.TileSize, y * m_MapDataLibrary.TileSize, 0);
    }

    public Vector2Int GetPlayerPosition()
    {
        return GameManager.Instance.Player.EntityPosition;
    }
}