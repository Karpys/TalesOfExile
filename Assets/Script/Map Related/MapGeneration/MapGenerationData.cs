using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewMapData", menuName = "MapData", order = 0)]
//Make this abstract
public class MapGenerationData : ScriptableObject
{
    public List<Vector2Int> PivotPoints = new List<Vector2Int>();
    [SerializeField] private TileSet m_RoadTileSet = null;

    private MapData m_MapData = null;
    private Map m_Map = null;
    public void Generate(MapData mapData)
    {
        m_MapData = mapData;
        m_Map = mapData.Map;
        int currentY = 0;
        int currentX = 0;

        currentY = Random.Range(0, m_Map.Height);
        currentY = 6;
        
        //Road Generation and TileSet
        List<Tile> roadTiles = new List<Tile>();
        
        Tile lastTile = PlaceTileAt(m_RoadTileSet.TilePrefab,currentX,currentY);
        roadTiles.Add(lastTile);
        
        for (int i = 0; i < PivotPoints.Count; i++)
        {
            PathFinding.NeighbourType = NeighbourType.Cross;
            List<Vector2Int> path = PathFinding.FindPath(lastTile, m_Map.Tiles[PivotPoints[i].x, PivotPoints[i].y]);
            PathFinding.NeighbourType = NeighbourType.Square;
            
            for (int j = 0; j < path.Count; j++)
            {
                lastTile = PlaceTileAt(m_RoadTileSet.TilePrefab, path[j].x, path[j].y);
                roadTiles.Add(lastTile);
            }
        }
        
        TileHelper.GenerateTileSet(roadTiles,m_RoadTileSet.TileMap,m_MapData);
    }

    private Tile PlaceTileAt(Tile tilePrefab, int x, int y)
    {
        Destroy(m_Map.Tiles[x,y].gameObject);
        Tile tile = Instantiate(tilePrefab, m_MapData.GetTilePosition(x, y), Quaternion.identity, m_MapData.transform);
        tile.Initialize(x, y);
        m_Map.Tiles[x, y] = tile;
        return tile;
    }
}

[System.Serializable]
public class TileSet
{
    [SerializeField] private Sprite[] m_TileMap = new Sprite[0];
    [SerializeField] private Tile m_TilePrefab = null;

    public Tile TilePrefab => m_TilePrefab;
    public Sprite[] TileMap => m_TileMap;
}
