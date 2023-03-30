

using UnityEngine;

public class Map
{
    public int Height = 0;
    public int Width = 0;
    public Tile[,] Tiles = null;

    private MapGenerator m_Generator = null;
    public Map(MapGenerator generator)
    {
        m_Generator = generator;
    }
    
    public WorldTile PlaceTileAt(WorldTile tilePrefab, int x, int y)
    {
        if(Tiles[x,y].WorldTile)
            GameObject.Destroy(Tiles[x,y].WorldTile.gameObject);
        
        WorldTile worldTile = GameObject.Instantiate(tilePrefab, MapData.Instance.GetTilePosition(x, y), Quaternion.identity, MapData.Instance.transform);
        worldTile.SetTile(Tiles[x,y]);
        return worldTile;
    }
    
    public VisualTile InsertVisualTile(VisualTile visual, WorldTile tile)
    {
        VisualTile visualTile = GameObject.Instantiate(visual, MapData.Instance.GetTilePosition(tile.Tile.TilePosition),
            Quaternion.identity, tile.transform);
        return visualTile;
    }
    
    public WorldTile GetDefaultMapTile()
    {
        return m_Generator.GenerationData.DefaultTile;
    }
}