using System;
using KarpysDev.Script.Entities;
using UnityEngine;

namespace KarpysDev.Script.Map_Related.MapGeneration
{
    public abstract class MapGenerationData : ScriptableObject
    {
        [SerializeField] protected int m_Width = 50;
        [SerializeField] protected int m_Height = 50;
        [Header("Base Map Data")] 
        [SerializeField] protected WorldTile m_BaseTile = null;
    
    
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

            m_Map.Tiles = new Tile[m_Width,m_Height];
        
            return new GenerationMapInfo(new Vector2Int(0,0));
        }

    }

    [Serializable]
    public struct EntitySpawn
    {
        public BoardEntity EntityPrefab;
        public Vector2Int EntityPosition;
    }
}