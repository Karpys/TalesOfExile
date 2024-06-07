using KarpysDev.Script.Entities;
using KarpysDev.Script.Manager;
using UnityEngine;

namespace KarpysDev.Script.Map_Related.Minimap
{
    public class EntityDynamicTile : MonoBehaviour,IPositionable
    {
        [SerializeField] private BoardEntity m_Entity = null;
        [SerializeField] private Color m_Color = Color.black;

        private DynamicMinimapTile m_DynamicMinimapTile = null;
        protected virtual void Awake()
        {
            m_Entity.A_OnEntityInitialization += CreateTile;
        }

        private void CreateTile()
        {
            DynamicMinimapTile dynamicTile = MinimapRenderer.Instance.AddDynamicTile(this,m_Entity.EntityPosition, m_Color);
            m_DynamicMinimapTile = dynamicTile;
            m_Entity.EntityEvent.OnRemoveFromMap += dynamicTile.Return;
        }

        public Vector2 Position => m_Entity.EntityPosition;
    }
}