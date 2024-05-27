using UnityEngine;

namespace KarpysDev.Script.Map_Related
{
    using System;

    public class VisualTile : MonoBehaviour
    {
        [SerializeField] protected SpriteRenderer m_Renderer = null;
        [SerializeField] private Color m_MinimapColor = Color.black;

        public SpriteRenderer Renderer => m_Renderer;
        public Color MinimapColor => m_MinimapColor;
        
        public virtual void Place(Vector2Int position)
        {
            MapData.Instance.GetTilePosition(position);
        }
    }
}