using System.Collections.Generic;
using KarpysDev.Script.PathFinding;
using KarpysDev.Script.PathFinding.LinePath;
using KarpysDev.Script.Utils;
using KarpysDev.Script.Widget;
using UnityEngine;

namespace KarpysDev.Script.Map_Related.MapGeneration
{
    [CreateAssetMenu(menuName = "Map/SpriteMap/CenterForestMap", fileName = "SpriteMap", order = 0)]
    public class CenterForestGeneration : SpriteMapGeneration
    {
        [Header("Center Forest Specific")]
        [Range(0,100)]
        [SerializeField] private float m_TreeChance = 10;
        [SerializeField] private WorldTile m_TreeTile = null;
        [SerializeField] private int m_AllowedSquareDistancePortalFromPlayer = 0;
        
        [Header("Monster Generation")]
        [SerializeField] private Vector2Int m_BlockMonsterSpawn = Vector2Int.one;

        private Vector2Int m_StartPos = Vector2Int.zero;
        public override GenerationMapInfo Generate(MapData mapData)
        {
            m_StartPos = new Vector2Int(Random.Range(5, m_Width - 5), Random.Range(5, m_Height - 5));
            GenerationMapInfo info = base.Generate(mapData);
            info.StartPosition = m_StartPos;
            return info;
        }

        protected override void GenerateTiles()
        {
            base.GenerateTiles();
            GenerateCenterTree();
            AddPortalNextMap();
        }

        private void AddPortalNextMap()
        {
            Vector2Int randomPos = new Vector2Int(Random.Range(5, m_Width - 5), Random.Range(5, m_Height - 5));

            while (DistanceUtils.GetSquareDistance(m_StartPos,randomPos) < m_AllowedSquareDistancePortalFromPlayer)
            {
                randomPos = new Vector2Int(Random.Range(5, m_Width - 5), Random.Range(5, m_Height - 5));
            }
            
            MapDataLibrary.Instance.AddPortalMapReloaderAt(randomPos);

            List<Vector2Int> playerPortalPath = LinePath.GetPathTile(m_StartPos, randomPos, NeighbourType.Cross);


            List<Tile> tiles = TileHelper.GetNeighboursTile(MapData.Instance.GetTile(randomPos), NeighbourType.Square, MapData.Instance);

            foreach (Tile tile in tiles)
            {
                m_Map.TryPlaceTileAt(m_BaseTile, tile.TilePosition);
            }
            
            foreach (Vector2Int pos in playerPortalPath)
            {
                if(!MapData.Instance.GetTile(pos).Walkable)
                {
                    m_Map.TryPlaceTileAt(m_BaseTile, pos);
                }
            }
        }

        private void GenerateCenterTree()
        {
            for (int x = 0; x < m_Width; x++)
            {
                for (int y = 0; y < m_Height; y++)
                {
                    TryGenerateTree(x,y);
                }
            }
        }
        
        private void TryGenerateTree(int x, int y)
        {
            if(x == m_StartPos.x && y == m_StartPos.y)
                return;
            
            float random = Random.Range(0, 100f);

            if (random < m_TreeChance)
            {
                GenerateTree(x,y);
            }
        }
        
        private void GenerateTree(int x,int y)
        {
            m_Map.PlaceTileAt(m_TreeTile, x, y);
        }

        protected override List<Tile> GetMonsterTiles()
        {
            List<Tile> tiles = new List<Tile>();

            for (int x = 0; x < m_MapData.Map.Width; x++)
            {
                for (int y = 0; y < m_MapData.Map.Height; y++)
                {
                    Tile tile = m_MapData.Map.Tiles[x][y];
                    
                    if(tile.TilePosition.x >= m_StartPos.x - m_BlockMonsterSpawn.x 
                       && tile.TilePosition.x <= m_StartPos.x + m_BlockMonsterSpawn.x 
                       && tile.TilePosition.y >= m_StartPos.y - m_BlockMonsterSpawn.y 
                       && tile.TilePosition.y <= m_StartPos.y + m_BlockMonsterSpawn.y)
                        continue;
            
                    if(tile.Walkable)
                        tiles.Add(tile);
                }
            }

            return tiles;
        }
    }
}