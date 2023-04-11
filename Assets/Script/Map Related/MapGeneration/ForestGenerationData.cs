using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Map/Forest", fileName = "New ForestMap", order = 0)]
public class ForestGenerationData : MapGenerationData
{
    [SerializeField] List<FloatPercentageSlider> m_RoadPivots = new List<FloatPercentageSlider>();
    [SerializeField] private TileSet m_RoadTileSet = null;
    [SerializeField] private WorldTile m_TreeTile = null;
    [SerializeField] private MonsterGeneration m_MonsterGeneration = null;

    public override GenerationMapInfo Generate(MapData mapData)
    {
        GenerationMapInfo mapInfo = new GenerationMapInfo();
        base.Generate(mapData);
        int currentY = 0;
        int currentX = 0;

        currentY = Random.Range(0, m_Map.Height);

        mapInfo.StartPosition = new Vector2Int(currentX, currentY);
        
        //Road Generation and TileSet
        GenerateRoad(currentX,currentY);
        MonsterGeneration();
        return mapInfo;
    }
    
    protected override void OnGenerateBaseTile(int x, int y)
    {
        base.OnGenerateBaseTile(x, y);
        TryGenerateTree(x,y);
    }
    
    public void GenerateRoad(int x,int y)
    {
        List<Tile> roadTiles = new List<Tile>();
        List<SpriteRenderer> roadRenderers = new List<SpriteRenderer>();

        Tile lastTile = m_Map.Tiles[x, y];
        roadTiles.Add(lastTile);
        roadRenderers.Add(m_Map.InsertVisualTile(m_RoadTileSet.TilePrefab, lastTile.WorldTile).Renderer);
        
        bool lastPivot = false;

        for (int i = 0; i < m_RoadPivots.Count; i++)
        {
            x = (int)(m_RoadPivots[i].Value / 100 * m_Width);
            y = Random.Range(0, m_Height);
            lastPivot = i == m_RoadPivots.Count - 1;

            if (lastPivot)
                x = m_Width - 1;
            
            //TODO:Method in Path finding
            PathFinding.NeighbourType = NeighbourType.Cross;

            List<Vector2Int> path = null;

            int loopCount = 0;

            List<Tile> pathTile = new List<Tile>();
            pathTile.AddRange(LinePath.GetPathTile(new Vector2Int(lastTile.XPos, lastTile.YPos), new Vector2Int(x, y),NeighbourType.Cross).ToTile());

            RemoveUnreachableTileOnPath(pathTile);
            
            if (lastPivot)
                m_Map.PlaceTileAt(m_BaseTile, x, y);

            path = new List<Vector2Int>();
            path.Add(new Vector2Int(lastTile.XPos,lastTile.YPos));
            path.AddRange(PathFinding.FindPath(lastTile, m_Map.Tiles[x,y]));
            
            PathFinding.NeighbourType = NeighbourType.Square;
        
            for (int j = 0; j < path.Count; j++)
            {
                lastTile = m_Map.Tiles[path[j].x,path[j].y];
                roadTiles.Add(lastTile);
                roadRenderers.Add(m_Map.InsertVisualTile(m_RoadTileSet.TilePrefab, lastTile.WorldTile).Renderer);
            }
            
        }
        
        
    
        TileHelper.GenerateTileSet(roadTiles,roadRenderers,m_RoadTileSet.TileMap,m_MapData);
    }

    private void MonsterGeneration()
    {
        List<Tile> tiles = new List<Tile>();

        foreach (Tile tile in m_MapData.Map.Tiles)
        {
            if(tile.Walkable)
                tiles.Add(tile);
        }

        m_MonsterGeneration.GenerateEnemies(tiles,m_MapData);
    }

    private void RemoveUnreachableTileOnPath(List<Tile> tiles)
    {
        int tilesCount = tiles.Count;
        
        for (int i = 0; i < tilesCount; i++)
        {
            bool reachable = false;
            
            List<Tile> neighours = TileHelper.GetNeighbours(tiles[i], NeighbourType.Cross, m_MapData);
            
            foreach(Tile neighbour in neighours)
            {
                if (neighbour.Walkable)
                    reachable = true;
            }

            if (!reachable)
            {
                foreach (Tile tile in neighours)
                {
                    m_Map.PlaceTileAt(m_BaseTile, tile.XPos, tile.YPos);
                }
            }
        }
    }

    private void TryGenerateTree(int x, int y)
    {

        if (Random.Range(0, 20) == 10)
        {
            GenerateTree(x, y);
        }
    }

    private void GenerateTree(int x,int y)
    {
        WorldTile tree = m_Map.PlaceTileAt(m_TreeTile, x, y);
        tree.GetComponentInChildren<SpriteHelper>().SetSpritePriority(-y);
    }
}

[System.Serializable]
public class TileSet
{
    [SerializeField] private Sprite[] m_TileMap = new Sprite[0];
    [SerializeField] private VisualTile m_TilePrefab = null;

    public VisualTile TilePrefab => m_TilePrefab;
    public Sprite[] TileMap => m_TileMap;
}