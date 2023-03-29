using UnityEngine;

public class VisualTile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_Renderer = null;

    public SpriteRenderer Renderer => m_Renderer;
}