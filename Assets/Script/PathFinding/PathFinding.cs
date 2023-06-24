using System;
using System.Collections.Generic;
using KarpysDev.Script.Map_Related;
using KarpysDev.Script.Widget;
using UnityEngine;

namespace KarpysDev.Script.PathFinding
{
    public enum NeighbourType
    {
        Square,
        Cross
    }
    public static class PathFinding
    {
        public static int BASE_MAX_ITERATION_COUNT = 100;
        public static NeighbourType NeighbourType = NeighbourType.Square;

        private static TileHeap openSet = null;
        public static int maxIteration
        {
            get;
            set;
        }


        public static MapData mapData = null;

        public static void UpdatePathFinding()
        {
            openSet = new TileHeap(mapData.MaxSize);
        }
        
        public static Tile FindClosestTile(Vector2Int startPos, Vector2Int endPos,bool ignoreWall = false)
        {
            Tile tile = FindLastTile(startPos, endPos,ignoreWall);
            Tile currentTile = tile;

            if (currentTile.TilePosition == startPos)
                return currentTile;
            while (currentTile.ParentTile.TilePosition != startPos)
            {
                currentTile = currentTile.ParentTile;
            }

            return currentTile;
        }
        
        public static List<Tile> FindTilePath(Vector2Int startPos, Vector2Int endPos,bool ignoreWall = false)
        {
            Tile startTile = mapData.Map.Tiles[startPos.x, startPos.y];
            Tile playerTile = mapData.Map.Tiles[endPos.x, endPos.y];

            Tile lastTile = FindLastTile(startPos, endPos,ignoreWall);

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


        public static List<Vector2Int> FindPath(Vector2Int startPos, Vector2Int endPos, bool ignoreWall = false)
        {
            List<Vector2Int> path = new List<Vector2Int>();
            List<Tile> tilePath = FindTilePath(startPos, endPos, ignoreWall);

            if (tilePath == null)
                return null;

            foreach (Tile tile in tilePath)
            {
                path.Add(new Vector2Int(tile.XPos,tile.YPos));
            }

            return path;
        }
        
        //Core//
        public static Tile FindLastTile(Vector2Int startPos, Vector2Int endPos,bool ignoreWall = false)
        {
            openSet.Clear();
            Tile startTile = mapData.Map.Tiles[startPos.x, startPos.y];
            Tile playerTile = mapData.Map.Tiles[endPos.x, endPos.y];
        
            int iterationCount = 0;

            HashSet<Tile> closeSet = new HashSet<Tile>();
            openSet.Add(startTile);

            while (openSet.Count > 0)
            {
                if (iterationCount >= maxIteration)
                {
                    return openSet.RemoveFirst(); 
                }
                    
                iterationCount += 1;
                Tile currentTile = openSet.RemoveFirst();
                closeSet.Add(currentTile);

                /*if (currentTile == playerTile)
            {
                DebugTile(startTile,currentTile);
                return currentTile;
            }*/
            
                SetNeighbours(currentTile);

                for(int i = 0; i < NeightbourCount; i++)
                {
                    Tile neighbour = NeightboursTiles[i];
                    //Player Not Walkable
                    if (neighbour == playerTile)
                    {
                        //Debug.Log(iterationCount);
                        /*DebugTile(startTile,currentTile);*/
                        return currentTile;
                    }
                
                    if (!neighbour.Walkable && !ignoreWall || closeSet.Contains(neighbour))
                        continue;

                    float newMovementCost = currentTile.gCost + GetDistance(currentTile, neighbour, startTile);
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
        
        private static Tile[] NeightboursTiles = new Tile[8];
        private static int NeightbourCount = 0;
        private static void SetNeighbours(Tile tile)
        {
            if (NeighbourType == NeighbourType.Square)
            {
                SetSquareNeighbours(tile);
            }else if (NeighbourType == NeighbourType.Cross)
            {
                SetCrossNeighours(tile);
            }
            
        }

        private static void SetCrossNeighours(Tile tile)
        {
            int id = 0;
            for (int x = -1; x <= 1; x++)
            {
                if(x == 0)
                    continue;
            
                int checkX = tile.XPos + x;
                if (checkX >= 0 && checkX < mapData.Map.Width)
                {
                    NeightboursTiles[id] = mapData.Map.Tiles[checkX, tile.YPos];
                    id++;
                }
            }
        
            for (int y = -1; y <= 1; y++)
            {
                if(y == 0)
                    continue;
            
                int checkY = tile.YPos + y;
                if (checkY >= 0 && checkY < mapData.Map.Height)
                {
                    NeightboursTiles[id] = mapData.Map.Tiles[tile.XPos, checkY];
                    id++;
                }
            }

            NeightbourCount = id;
        }

        private static void SetSquareNeighbours(Tile tile)
        {
            int id = 0;
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
                        NeightboursTiles[id] = mapData.Map.Tiles[checkX, checkY];
                        id++;
                    }
                }
            }

            NeightbourCount = id;
        }
    }
}