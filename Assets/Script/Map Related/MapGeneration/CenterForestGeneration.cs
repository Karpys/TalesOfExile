using System.Collections.Generic;
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

            foreach (Tile tile in m_MapData.Map.Tiles)
            {
                if(tile.TilePosition.x >= m_StartPos.x - m_BlockMonsterSpawn.x 
                   && tile.TilePosition.x <= m_StartPos.x + m_BlockMonsterSpawn.x 
                   && tile.TilePosition.y >= m_StartPos.y - m_BlockMonsterSpawn.y 
                   && tile.TilePosition.y <= m_StartPos.y + m_BlockMonsterSpawn.y)
                    continue;
            
                if(tile.Walkable)
                    tiles.Add(tile);
            }

            return tiles;
        }
    }
}