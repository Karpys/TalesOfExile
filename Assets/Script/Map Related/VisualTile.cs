using UnityEngine;

namespace KarpysDev.Script.Map_Related
{
    public class VisualTile : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer m_Renderer = null;
        [SerializeField] private Color m_MinimapColor = Color.black;

        public SpriteRenderer Renderer => m_Renderer;
        public Color MinimapColor => m_MinimapColor;
    }
}