using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script.PathFinding
{
    public static class PathFinding
    {
        public static MapData mapData = null;
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
           
            foreach (Tile tile in FindTilePath(startTile, playerTile, ignoreWall))
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

                foreach (Tile neighbour in GetNeighbours(currentTile))
                {
                    //Player Not Walkable
                    if (neighbour == playerTile)
                    {
                        /*DebugTile(startTile,currentTile);*/
                        return currentTile;
                    }
                    
                    if (!neighbour.Walkable && !ignoreWall || closeSet.Contains(neighbour))
                        continue;

                    float newMovementCost = currentTile.gCost + GetDistance(currentTile, neighbour);
                    if (newMovementCost < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newMovementCost;
                        neighbour.hCost = GetDistance(neighbour, playerTile);
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

        public static float GetDistance(Tile tileStart, Tile tileEnd)
        {
            //Source//
            //http://theory.stanford.edu/~amitp/GameProgramming/Heuristics.html//
            //Base
            int distX = Mathf.Abs(tileStart.XPos - tileEnd.XPos);
            int distY = Mathf.Abs(tileStart.YPos - tileEnd.YPos);
            
            //Heuristics GOOD : D1 = 1 and D2 = 1.01f//
            //float D1 = 1;
            //float D2 = 1.1f;
            //return D1 * (distX + distY) + (D2 - 2 * D1) * Math.Min(distX, distY);

            //Euclidian Distance Perfect Result.. Higher Cost But thats exactly what i was thinking ^^//
            return (float)Math.Sqrt(distX * distX + distY * distY);

            // if (distX > distY)
            //     return D1 * (distX - distY) + D2 * distY;
            // return D1 * (distY - distX) + D2 * distX;
        }

        public static List<Tile> GetNeighbours(Tile tile)
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

                    if (checkX >= 0 && checkX < mapData.Map.Width && checkY >= 0 && checkY < mapData.Map.Height)
                    {
                        neighbours.Add(mapData.Map.Tiles[checkX,checkY]); 
                    }
                }
            }

            return neighbours;
        }

    }
}