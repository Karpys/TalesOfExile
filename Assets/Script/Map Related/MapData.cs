using System.Collections.Generic;
using System.Linq;
using KarpysDev.Script.Entities;
using KarpysDev.Script.Manager;
using UnityEngine;

namespace KarpysDev.Script.Map_Related
{
    public class MapData : SingletonMonoBehavior<MapData>
    {
        [SerializeField] private MapDataLibrary m_MapDataLibrary = null;
        private Map m_Map = null;
    
        public Map Map
        {
            get { return m_Map; }
            set { m_Map = value; }
        }
    
    
        public bool IsWalkable(int x, int y)
        {
            if (x >= 0 && x < m_Map.Width && y >= 0 && y < m_Map.Height && m_Map.Tiles[x,y].Walkable)
            {
                return true;
            }
            return false;
        }

        public bool IsWalkable(Vector2Int pos)
        {
            return IsWalkable(pos.x, pos.y);
        }

        public Vector3 GetTilePosition(int x, int y)
        {
            return new Vector3(x * m_MapDataLibrary.TileSize, y * m_MapDataLibrary.TileSize, 0);
        }

        public Vector3 GetTilePosition(Vector2Int pos)
        {
            return GetTilePosition(pos.x, pos.y);
        }

        public Tile GetTile(Vector2Int pos)
        {
            if (!Map.InMapBounds(pos))
                return null;
        
            return Map.Tiles[pos.x, pos.y];
        }

        public Vector2Int GetControlledEntityPosition()
        {
            return GameManager.Instance.ControlledEntity.EntityPosition;
        }
    
        public BoardEntity GetEntityAt(Vector2Int entityPos,EntityGroup targetEntityGroup)
        {
            if (!m_Map.InMapBounds(entityPos))
                return null;
            
            BoardEntity entityAt = m_Map.EntitiesTile[entityPos.x,entityPos.y];

            if (entityAt && entityAt.EntityGroup == targetEntityGroup)
                return entityAt;
            
            return null;
        }

        public Vector2Int MapClampedPosition(Vector2Int pos)
        {
            int clampedX = Mathf.Clamp(pos.x, 0,m_Map.Width - 1);
            int clampedY = Mathf.Clamp(pos.y,0, m_Map.Height - 1);
            return new Vector2Int(clampedX, clampedY);
        }
    }
}