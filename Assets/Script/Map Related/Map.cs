

using UnityEngine;

public class Map
{
    public int Height = 0;
    public int Width = 0;
    public Tile[,] Tiles = null;
    
    public WorldTile PlaceTileAt(WorldTile tilePrefab, int x, int y)
    {
        if(Tiles[x,y].WorldTile)
            GameObject.Destroy(Tiles[x,y].WorldTile.gameObject);
        
        WorldTile worldTile = GameObject.Instantiate(tilePrefab, MapData.Instance.GetTilePosition(x, y), Quaternion.identity, MapData.Instance.transform);
        worldTile.SetTile(Tiles[x,y]);
        return worldTile;
    }
}