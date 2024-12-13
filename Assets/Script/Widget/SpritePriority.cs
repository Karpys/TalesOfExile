using KarpysDev.Script.Map_Related;
using UnityEngine;

namespace KarpysDev.Script.Widget
{
    public class SpritePriority : MonoBehaviour
    {
        [SerializeField] private WorldTile m_OriginTile = null;
        [SerializeField] private SpriteRenderer m_Renderer = null;

        private void Start()
        {
            SetSpritePriority(-m_OriginTile.Tile.YPos);
        }

        private void SetSpritePriority(int priority)
        {
            m_Renderer.sortingOrder = priority;
        }
    }
}
