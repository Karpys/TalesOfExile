using System;
using System.Collections.Generic;
using KarpysDev.Script.PathFinding;
using KarpysDev.Script.PathFinding.LinePath;
using KarpysDev.Script.Utils;
using KarpysDev.Script.Widget;
using UnityEngine;
using Random = UnityEngine.Random;

namespace KarpysDev.Script.Map_Related.MapGeneration
{
    [CreateAssetMenu(menuName = "Map/Forest", fileName = "New ForestMap", order = 0)]
    public class ForestGenerationData : FlatDefaultMapGeneration
    {
        [SerializeField] List<FloatPercentageSlider> m_RoadPivots = new List<FloatPercentageSlider>();
        [SerializeField] private TileSet m_RoadTileSet = null;
        [Range(0,100)]
        [SerializeField] private float m_TreeChance = 10;
        [SerializeField] private WorldTile m_TreeTile = null;
        [SerializeField] private BaseMonsterGeneration m_MonsterGeneration = null;

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

            Tile lastTile = m_Map.Tiles[x][y];
            roadTiles.Add(lastTile);
            roadRenderers.Add(m_Map.CreateVisualTile(m_RoadTileSet.TilePrefab, lastTile.WorldTile).Renderer);
        
            bool lastPivot = false;

            for (int i = 0; i < m_RoadPivots.Count; i++)
            {
                x = (int)(m_RoadPivots[i].Value / 100 * m_Width);
                y = Random.Range(0, m_Height);
                lastPivot = i == m_RoadPivots.Count - 1;

                if (lastPivot)
                    x = m_Width - 1;
            
                List<Vector2Int> path = null;

                int loopCount = 0;

                List<Tile> pathTile = new List<Tile>();
                pathTile.AddRange(LinePath.GetPathTile(new Vector2Int(lastTile.XPos, lastTile.YPos), new Vector2Int(x, y),NeighbourType.Cross).ToTile());

                RemoveUnreachableTileOnPath(pathTile);
            
                if (lastPivot)
                    m_Map.PlaceTileAt(m_BaseTile, x, y);

                path = new List<Vector2Int>();
                path.Add(new Vector2Int(lastTile.XPos,lastTile.YPos));
                PathFinding.PathFinding.maxIteration = 1000;
                path.AddRange(PathFinding.PathFinding.FindPath(lastTile.TilePosition, m_Map.Tiles[x][y].TilePosition,NeighbourType.Cross));
                PathFinding.PathFinding.maxIteration = PathFinding.PathFinding.BASE_MAX_ITERATION_COUNT;
        
                for (int j = 0; j < path.Count; j++)
                {
                    lastTile = m_Map.Tiles[path[j].x][path[j].y];
                    roadTiles.Add(lastTile);
                    roadRenderers.Add(m_Map.CreateVisualTile(m_RoadTileSet.TilePrefab, lastTile.WorldTile).Renderer);
                }
            
            }
        
            MapDataLibrary.Instance.AddReloaderAt(lastTile.TilePosition);
            TileHelper.GenerateTileSet(roadTiles,roadRenderers,m_RoadTileSet.TileMap,m_MapData);
        }

        private void MonsterGeneration()
        {
            List<Tile> tiles = new List<Tile>();

            for (int x = 0; x < m_MapData.Map.Width; x++)
            {
                for (int y = 0; y < m_MapData.Map.Height; y++)
                {
                    Tile tile = m_MapData.Map.Tiles[x][y];
                    
                    if(tile.XPos < 9)
                        continue;
            
                    if(tile.Walkable)
                        tiles.Add(tile);
                }
            }
            
            m_MonsterGeneration.Generate(tiles);
        }

        private void RemoveUnreachableTileOnPath(List<Tile> tiles)
        {
            int tilesCount = tiles.Count;
        
            for (int i = 0; i < tilesCount; i++)
            {
                bool reachable = false;
            
                List<Tile> neighours = TileHelper.GetNeighboursTile(tiles[i], NeighbourType.Cross, m_MapData);
            
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
            if(x == 0)
                return;
        
            float random = Random.Range(0, 100f);

            if (random < m_TreeChance)
            {
                GenerateTree(x,y);
            }
        }

        private void GenerateTree(int x,int y)
        {
            WorldTile tree = m_Map.PlaceTileAt(m_TreeTile, x, y);
        }
    }

    [Serializable]
    public class TileSet
    {
        [SerializeField] private Sprite[] m_TileMap = new Sprite[0];
        [SerializeField] private VisualTile m_TilePrefab = null;

        public VisualTile TilePrefab => m_TilePrefab;
        public Sprite[] TileMap => m_TileMap;
    }
}