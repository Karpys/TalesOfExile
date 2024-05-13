using KarpysDev.Script.Entities;
using UnityEngine;

namespace KarpysDev.Script.Map_Related.Minimap
{
    public class EntityDynamicTile : MonoBehaviour
    {
        [SerializeField] private BoardEntity m_Entity = null;
        [SerializeField] private Color m_Color = Color.black;

        private void Awake()
        {
            m_Entity.A_OnEntityInitialization += CreateTile;
        }

        private void CreateTile()
        {
            MinimapRenderer.Instance.AddDynamicTile(m_Entity.EntityPosition, m_Color);
        }
    }
}