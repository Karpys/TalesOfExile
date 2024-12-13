namespace KarpysDev.Script.Map_Related.PrefabMap
{
    using System.Collections.Generic;
    using UnityEngine;

    public class MapDataHolder : MonoBehaviour
    {
        [SerializeField] private Vector2Int m_MapDimension = Vector2Int.zero;
        [SerializeField] private Vector2Int m_StartPosition = Vector2Int.zero;
        [SerializeField] private List<WorldTile> m_WorldTiles = null;
        
        public Vector2Int MapDimension => m_MapDimension;
        public Vector2Int StartPosition => m_StartPosition;
        public List<WorldTile> WorldTiles => m_WorldTiles;

        public void Initialize(Vector2Int mapDimension,Vector2Int startPosition,List<WorldTile> worldTiles)
        {
            m_MapDimension = mapDimension;
            m_WorldTiles = worldTiles;
            m_StartPosition = startPosition;
        }
    }
}