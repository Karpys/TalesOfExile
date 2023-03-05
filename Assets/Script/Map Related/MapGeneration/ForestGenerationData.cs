using System.Collections.Generic;
using UnityEngine;


public class ForestGenerationData : MapGenerationData
{
    [SerializeField] List<FloatPercentageSlider> m_RoadPivots = new List<FloatPercentageSlider>();
    [SerializeField] private TileSet m_RoadTileSet = null;
    [SerializeField] private Tile m_TreeTile = null;


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
    
        Tile lastTile = PlaceTileAt(m_RoadTileSet.TilePrefab,x,y);
        roadTiles.Add(lastTile);
    
        for (int i = 0; i < m_RoadPivots.Count; i++)
        {
            x = (int)(m_RoadPivots[i].Value / 100 * m_Width);
            y = Random.Range(0, m_Height);
            
            //TODO:Method in Path finding
            PathFinding.NeighbourType = NeighbourType.Cross;
            List<Vector2Int> path = null;

            int loopCount = 0;

            List<Tile> pathTile = LinePath.GetPathTile(new Vector2Int(lastTile.XPos, lastTile.YPos), new Vector2Int(x, y));
            pathTile.Add(lastTile);
            RemoveUnreachableTileOnPath(pathTile);
            path = PathFinding.FindPath(lastTile, m_Map.Tiles[x,y]);
            
            PathFinding.NeighbourType = NeighbourType.Square;
        
            for (int j = 0; j < path.Count; j++)
            { 
                lastTile = PlaceTileAt(m_RoadTileSet.TilePrefab, path[j].x, path[j].y);
                roadTiles.Add(lastTile);
            }
        }
    
        TileHelper.GenerateTileSet(roadTiles,m_RoadTileSet.TileMap,m_MapData);
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
                    PlaceTileAt(m_BaseTile, tile.XPos, tile.YPos);
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
        Tile tree = PlaceTileAt(m_TreeTile, x, y);
        tree.GetComponentInChildren<SpriteHelper>().SetSpritePriority(-y);
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