using System.Collections.Generic;
using KarpysDev.Script.Entities;
using KarpysDev.Script.Entities.EntitiesBehaviour;
using KarpysDev.Script.Items.Loot;
using KarpysDev.Script.Manager;
using KarpysDev.Script.Widget;
using UnityEngine;

namespace KarpysDev.Script.Map_Related.Blight
{
    public class BlightSpawner:WorldTile
    {
        [SerializeField] private BoardEntity m_BlightMonster = null;
        [SerializeField] private Vector2Int m_Clock = new Vector2Int(2,5);
        [SerializeField] private int m_BlightMonsterCount = 10;
        [SerializeField] private ChestLootable m_ChestLoot = null;

        public int MonsterCount => m_BlightMonsterCount; 
        private int m_CurrentClock = 0;
        private bool m_IsActive = false;
        private List<Tile> m_BranchPath = null;
        private BlightCore m_BlightCore = null;

        public BlightCore BlightCore => m_BlightCore;

        private Map m_Map = null;

        public void Initialize(List<Tile> branchPath, BlightCore core,Map map)
        {
            m_BlightCore = core;
            m_BranchPath = branchPath;
            m_BranchPath.Reverse();
            m_Map = map;
        }
    
        private void Start()
        {
            GameManager.Instance.A_OnEndTurn += TrySpawnBlightMonster;
        }

        private void OnDestroy()
        {
            if(GameManager.Instance)
                GameManager.Instance.A_OnEndTurn -= TrySpawnBlightMonster;
        }

        public void ActiveBlight(bool active)
        {
            m_IsActive = active;
        }
    
        private void TrySpawnBlightMonster()
        {
            if(!m_IsActive || m_BlightMonsterCount <= 0)
                return;

            m_CurrentClock -= 1;
        
            if (m_CurrentClock < 0)
            {
                if (Tile.Walkable)
                {
                    BoardEntity entity = EntityHelper.SpawnEntityOnMap(Tile.TilePosition,m_BlightMonster,new BlightBehaviour(this),EntityGroup.Enemy);
                
                    m_CurrentClock = Random.Range(m_Clock.x, m_Clock.y);
                    m_BlightMonsterCount -= 1;
                }
            }

        }
    
        public Tile GetNextBranchPath(int id)
        {
            if (id >= m_BranchPath.Count - 1)
                return null;
            return m_BranchPath[id + 1];
        }

        public void PopBlightChest()
        {
            m_ChestLoot.OpenChest();
            WorldTile defaultTile = m_Map.PlaceTileAt(m_Map.GetDefaultMapTile(), Tile.XPos, Tile.YPos);
        }
    }
}
