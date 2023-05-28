

using KarpysDev.Script.Entities;
using UnityEngine;

namespace KarpysDev.Script.Map_Related
{
    public class Map
    {
        public int Height = 0;
        public int Width = 0;
        public Tile[,] Tiles = null;
        public BoardEntity[,] EntitiesTile = null;
    
        public Map(int width, int height)
        {
            Height = height;
            Width = width;
            Tiles = new Tile[Width,Height];
            EntitiesTile = new BoardEntity[Width, Height];
        
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Tiles[x, y] = new Tile(x,y);
                }
            }
        }
    
        public WorldTile PlaceTileAt(WorldTile tilePrefab, int x, int y)
        {
            if(Tiles[x,y].WorldTile)
                GameObject.Destroy(Tiles[x,y].WorldTile.gameObject);
        
            WorldTile worldTile = GameObject.Instantiate(tilePrefab, MapData.Instance.GetTilePosition(x, y), Quaternion.identity, MapData.Instance.transform);
            worldTile.SetTile(Tiles[x,y]);
            return worldTile;
        }

        public WorldTile PlaceTileAt(WorldTile tilePrefab, Vector2Int pos)
        {
            return PlaceTileAt(tilePrefab, pos.x, pos.y);
        }
    
        public WorldTile TryPlaceTileAt(WorldTile tilePrefab, Vector2Int pos)
        {
            if (InMapBounds(pos))
                return PlaceTileAt(tilePrefab, pos);

            return null;
        }
    
        private WorldTile InsertWorldTileAt(WorldTile worldTile, int x,int y)
        {
            if(Tiles[x,y].WorldTile)
                GameObject.Destroy(Tiles[x,y].WorldTile.gameObject);

            worldTile.SetTile(Tiles[x,y]);
            worldTile.transform.position = MapData.Instance.GetTilePosition(x, y);
            return worldTile;
        }

        public WorldTile TryInsertWorldTileAt(WorldTile worldTile, Vector2Int pos)
        {
            if (InMapBounds(pos))
                return InsertWorldTileAt(worldTile, pos.x,pos.y);

            return null;
        }
    
    
        public VisualTile InsertVisualTile(VisualTile visual, WorldTile tile)
        {
            VisualTile visualTile = GameObject.Instantiate(visual, MapData.Instance.GetTilePosition(tile.Tile.TilePosition),
                Quaternion.identity, tile.transform);
            return visualTile;
        }
    
        public WorldTile GetDefaultMapTile()
        {
            return MapGenerator.Instance.CurrentMapData.DefaultTile;
        }
    
        public bool InMapBounds(Vector2Int pos)
        {
            return InMapBounds(pos.x, pos.y);
        }
        public bool InMapBounds(int x,int y)
        {
            if (x < 0 || y < 0 || x > Width - 1 || y > Height - 1)
                return false;

            return true;
        }
    }
}