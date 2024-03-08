using System;
using System.Collections.Generic;
using System.Linq;
using KarpysDev.KarpysUtils;
using KarpysDev.Script.Map_Related;
using KarpysDev.Script.PathFinding;
using KarpysDev.Script.PathFinding.LinePath;
using UnityEngine;

namespace KarpysDev.Script.Widget
{
    public static class TileHelper
    {
        private static Vector2Int[] DirectionalCheck = new Vector2Int[]
        {
            new Vector2Int(0, 1),
            new Vector2Int(1, 0),
            new Vector2Int(0, -1),
            new Vector2Int(-1, 0),
        };
        
        private static Vector2Int[] SquareCheck = new Vector2Int[]
        {
            new Vector2Int(0, 1),
            new Vector2Int(1, 0),
            new Vector2Int(0, -1),
            new Vector2Int(-1, 0),
            new Vector2Int(-1, -1),
            new Vector2Int(-1, 1),
            new Vector2Int(1, 1),
            new Vector2Int(1, -1),
        };

    
        public static Vector2Int GetOppositePosition(Vector2Int pivotPos, Vector2Int oppositeBasePos)
        {
            Vector2Int pivoToOppo = new Vector2Int(oppositeBasePos.x - pivotPos.x, oppositeBasePos.y - pivotPos.y);
            return pivotPos - pivoToOppo;
        }
    
        public static List<Tile> GetNeighboursTile(Tile tile,NeighbourType type,MapData mapData)
        {
            List<Tile> neighbours = new List<Tile>();

            if (type == NeighbourType.Square)
            {
                GetSquareNeighboursTile(tile,neighbours,mapData);
            }else if (type == NeighbourType.Cross)
            {
                GetCrossNeighboursTile(tile,neighbours,mapData);
            }
        
            return neighbours;
        }
        
        public static List<Vector2Int> GetMapNeighbours(Vector2Int originPosition,NeighbourType type,MapData mapData)
        {
            List<Vector2Int> neighbours = new List<Vector2Int>();

            if (type == NeighbourType.Square)
            {
                GetMapSquareNeighbours(originPosition,neighbours,mapData);
            }else if (type == NeighbourType.Cross)
            {
                GetMapCrossNeighbours(originPosition,neighbours,mapData);
            }
        
            return neighbours;
        }
        
    
        public static List<Tile> GetNeighboursWalkable(Tile tile,NeighbourType type,MapData mapData)
        {
            List<Tile> neighbours = new List<Tile>();

            if (type == NeighbourType.Square)
            {
                GetSquareNeighboursTile(tile,neighbours,mapData);
            }else if (type == NeighbourType.Cross)
            {
                GetCrossNeighboursTile(tile,neighbours,mapData);
            }

            neighbours = neighbours.Where(n => n.Walkable).ToList();
            Debug.Log("Walkable Neighours :" + neighbours.Count);
            return neighbours;
        }

        public static Tile GetFreeClosestAround(Tile aroundTile,Vector3 entityWorldPosition)
        {
            List<Tile> neighbours = GetNeighboursTile(aroundTile,NeighbourType.Square, MapData.Instance);

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
        
        public static bool IsClosestAroundWalkable(Vector2Int position,Vector3 entityWorldPosition)
        {
            List<Vector2Int> neighbours = GetMapNeighbours(position,NeighbourType.Square, MapData.Instance);
            
            Vector2Int closest = neighbours[0];
            float minDistance = Vector3.Distance(MapData.Instance.GetTilePosition(closest),
                entityWorldPosition);
        
            for(int i = 1; i < neighbours.Count; i++)
            {
                float distance = Vector3.Distance(MapData.Instance.GetTilePosition(neighbours[i]),entityWorldPosition);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    closest = neighbours[i];
                }
            }

            return MapData.Instance.IsWalkable(closest);
        }

        private static void GetCrossNeighboursTile(Tile tile, List<Tile> neighbours,MapData mapData)
        {
            foreach (Vector2Int position in DirectionalCheck)
            {
                Vector2Int check = new Vector2Int(position.x + tile.XPos, position.y + tile.YPos);

                if (check.x >= 0 && check.x < mapData.Map.Width && check.y >= 0 && check.y < mapData.Map.Height)
                {
                    neighbours.Add(mapData.Map.Tiles[check.x,check.y]);
                }
            }
        }

        private static void GetSquareNeighboursTile(Tile tile, List<Tile> neighbours,MapData mapData)
        {
            foreach (Vector2Int position in SquareCheck)
            {
                Vector2Int check = new Vector2Int(position.x + tile.XPos, position.y + tile.YPos);

                if (check.x >= 0 && check.x < mapData.Map.Width && check.y >= 0 && check.y < mapData.Map.Height)
                {
                    neighbours.Add(mapData.Map.Tiles[check.x,check.y]);
                }
            }
        }
        
        private static void GetMapCrossNeighbours(Vector2Int originPosition, List<Vector2Int> neighbours,MapData mapData)
        {
            foreach (Vector2Int position in DirectionalCheck)
            {
                Vector2Int check = new Vector2Int(position.x + originPosition.x, position.y + originPosition.y);

                if (check.x >= 0 && check.x < mapData.Map.Width && check.y >= 0 && check.y < mapData.Map.Height)
                {
                    neighbours.Add(check);
                }
            }
        }
        
        private static void GetMapSquareNeighbours(Vector2Int originPosition, List<Vector2Int> neighbours,MapData mapData)
        {
            foreach (Vector2Int position in SquareCheck)
            {
                Vector2Int check = new Vector2Int(position.x + originPosition.x, position.y + originPosition.y);

                if (check.x >= 0 && check.x < mapData.Map.Width && check.y >= 0 && check.y < mapData.Map.Height)
                {
                    neighbours.Add(check);
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
        
            List<Vector2Int> path = LinePath.GetPathTile(currentTile, tileFrom,NeighbourType.Square);
            Vector2Int oppositePosition = Vector2Int.zero;

            if (path.Count > 0)
            {
                oppositePosition = GetOppositePosition(currentTile, path[0]);
            }
            else
            {
                oppositePosition = GetOppositePosition(currentTile, tileFrom);
            }

            return oppositePosition;
        }

        private static int GetBitMaskingValue(Tile tile,List<Tile> tiles,MapData mapData)
        {
            string bitMask = "";
        
            for (int i = DirectionalCheck.Length - 1; i >= 0; i--)
            {
                int checkX = tile.XPos + DirectionalCheck[i].x;
                int checkY = tile.YPos + DirectionalCheck[i].y;
            
                if (checkX >= 0 && checkX < mapData.Map.Width && checkY >= 0 && checkY < mapData.Map.Height)
                {
                    if (tiles.Contains(mapData.Map.Tiles[checkX, checkY]))
                    {
                        bitMask += "1";
                        continue;
                    }
                }

                bitMask += "0";
            }
            return Convert.ToInt32(bitMask, 2);
        }

        public static void GenerateTileSet(List<Tile> tiles,List<SpriteRenderer> renderers,Sprite[] tileMap,MapData mapData)
        {
            for (int i = 0; i < tiles.Count; i++)
            {
                Tile tile = tiles[i];
                renderers[i].sprite = tileMap[GetBitMaskingValue(tile, tiles, mapData)];
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
    
        public static List<Tile> ToTile(this List<Vector2Int> path)
        {
            List<Tile> tiles = new List<Tile>();

            for (int i = 0; i < path.Count; i++)
            {
                tiles.Add(MapData.Instance.GetTile(path[i]));
            }

            return tiles;
        }
    
        public static List<Tile> ToTile(this List<WorldTile> path)
        {
            List<Tile> tiles = new List<Tile>();

            for (int i = 0; i < path.Count; i++)
            {
                tiles.Add(path[i].Tile);
            }

            return tiles;
        }
    
        public static List<WorldTile> ToWorldTile(this List<Tile> tiles)
        {
            List<WorldTile> worldTiles = new List<WorldTile>();

            for (int i = 0; i < tiles.Count; i++)
            {
                worldTiles.Add(tiles[i].WorldTile);
            }

            return worldTiles;
        }
    }
}
