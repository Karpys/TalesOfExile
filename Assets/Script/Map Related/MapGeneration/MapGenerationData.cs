using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewMapData", menuName = "MapData", order = 0)]
//Make this abstract
public class MapGenerationData : ScriptableObject
{
    public List<Vector2Int> PivotPoints = new List<Vector2Int>();
    [SerializeField] private Tile m_RoadTile = null;

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
        Tile LastTile = PlaceTileAt(m_RoadTile,currentX,currentY);

        for (int i = 0; i < PivotPoints.Count; i++)
        {
            List<Vector2Int> path = PathFinding.FindPath(LastTile, m_Map.Tiles[PivotPoints[i].x, PivotPoints[i].y]);
            
            for (int j = 0; j < path.Count; j++)
            {
                LastTile = PlaceTileAt(m_RoadTile, path[j].x, path[j].y);
                if(j == 0)
                    LastTile.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.black;
            }
        }
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
