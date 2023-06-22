using System.Collections.Generic;
using UnityEngine;

namespace KarpysDev.Script.Map_Related.MapGeneration
{
    [CreateAssetMenu(menuName = "Map/Center Forest Map", fileName = "New FlatMap", order = 0)]
    public class ForestCenterGenerationData : FlatDefaultMapGeneration
    {
        [SerializeField] private Vector2Int m_SpawnPosition = Vector2Int.one;
        [Range(0,100)]
        [SerializeField] private float m_TreeChance = 10;
        [SerializeField] private WorldTile m_TreeTile = null;

        [Header("Monster Generation")]
        [SerializeField] private Vector2Int m_BlockMonsterSpawn = Vector2Int.one;
        [SerializeField] private MonsterGeneration m_MonsterGeneration = null;

        public override GenerationMapInfo Generate(MapData mapData)
        {
            GenerationMapInfo info =  base.Generate(mapData);
            MonsterGeneration();
            info.StartPosition = m_SpawnPosition;
            return info;
        }

        protected override void OnGenerateBaseTile(int x, int y)
        {
            TryGenerateTree(x,y);
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
        
        private void MonsterGeneration()
        {
            List<Tile> tiles = new List<Tile>();

            foreach (Tile tile in m_MapData.Map.Tiles)
            {
                if(tile.TilePosition.x >= m_SpawnPosition.x - m_BlockMonsterSpawn.x && tile.TilePosition.x <= m_SpawnPosition.x + m_BlockMonsterSpawn.x
                   && tile.TilePosition.y >= m_SpawnPosition.y - m_BlockMonsterSpawn.y && tile.TilePosition.y <= m_SpawnPosition.y + m_BlockMonsterSpawn.y)
                    continue;
            
                if(tile.Walkable)
                    tiles.Add(tile);
            }

            m_MonsterGeneration.Generate(tiles);
        }
    }
}