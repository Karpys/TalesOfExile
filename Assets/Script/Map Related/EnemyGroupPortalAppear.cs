using System.Collections.Generic;
using KarpysDev.Script.Entities;
using KarpysDev.Script.Manager;
using KarpysDev.Script.Map_Related.MapGeneration;
using UnityEngine;

namespace KarpysDev.Script.Map_Related
{
    public class EnemyGroupPortalAppear : WorldTile
    {
        [SerializeField] private MapTileReloader m_TileReloader = null;
        private int m_Count = 0;

        private bool m_HasSpawnPortal = false;
        private void Awake()
        {
            MapGenerator.Instance.A_OnMapLoaded += CountEnemy;
        }
        private void CountEnemy()
        {
            m_Count = GameManager.Instance.EnemiesOnBoard.Count;
            List<BoardEntity> enemies = GameManager.Instance.EnemiesOnBoard;

            foreach (BoardEntity boardEntity in enemies)
            {
                boardEntity.EntityEvent.OnDeath += DecreaseCount;
            }
            
            MapGenerator.Instance.A_OnMapLoaded -= CountEnemy;
        }

        private void DecreaseCount()
        {
            if (m_HasSpawnPortal)
            {
                Debug.LogError("Has spawn portal but still decrease");
                return;
            }
                    
            m_Count--;
            if (m_Count == 0)
            {
                m_TileReloader.Initialize(m_AttachedTile.TilePosition);
                m_TileReloader.gameObject.SetActive(true);
            }
        }
    }
}