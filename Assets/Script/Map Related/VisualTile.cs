using UnityEngine;

namespace KarpysDev.Script.Map_Related
{
    using System;

    public class VisualTile : MonoBehaviour
    {
        [SerializeField] protected SpriteRenderer m_Renderer = null;

        public SpriteRenderer Renderer => m_Renderer;

        public virtual void Place(Vector2Int position)
        {
            MapData.Instance.GetTilePosition(position);
        }
    }
}