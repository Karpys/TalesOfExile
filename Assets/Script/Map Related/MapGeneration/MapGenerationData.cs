using System;
using KarpysDev.Script.Entities;
using KarpysDev.Script.Entities.EntitiesBehaviour;
using UnityEngine;

namespace KarpysDev.Script.Map_Related.MapGeneration
{
    public abstract class MapGenerationData : ScriptableObject
    {
        [SerializeField] protected int m_Width = 50;
        [SerializeField] protected int m_Height = 50;
        [Header("Base Map Data")] 
        [SerializeField] protected WorldTile m_BaseTile = null;
        [SerializeField] protected Vector2Int m_SpawnPosition = Vector2Int.one;
        
        protected MapData m_MapData = null;
        protected Map m_Map = null;
    
        public WorldTile DefaultTile => m_BaseTile;

        public virtual GenerationMapInfo Generate(MapData mapData)
        {
            m_MapData = mapData;
            m_Map = new Map(m_Width, m_Height);
            mapData.SetMap(m_Map);
        
            m_Map.Height = m_Height;
            m_Map.Width = m_Width;

            m_Map.Tiles = Tile.Init(m_Width, m_Height);
            
            return new GenerationMapInfo(m_SpawnPosition);
        }

    }

    [Serializable]
    public struct EntitySpawn
    {
        public BoardEntity EntityPrefab;
        public Vector2Int EntityPosition;
        public EntityIAType IAType;
        public EntityGroup EntityGroup;
        public EntityGroup TargetGroup;
        public bool ShouldInheritQuestModifier;
    }

    public enum EntityIAType
    {
        None = 0,
        MonsterMapEntity = 1,
        BaseEntity = 2,
        MissionEntity = 3,
        VendingEntity = 4,
        Dummy = 5,
    }
}