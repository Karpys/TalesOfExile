using UnityEngine;

public class CubeViewer : MonoBehaviour
{
    [SerializeField] private Vector3 m_Size = Vector3.one;
    [SerializeField] private Color m_Color = Color.white;

    public void Initalize(Vector3 size,Color color)
    {
        m_Color = color;
        m_Size = size;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = m_Color;
        Gizmos.DrawWireCube(transform.position,m_Size);

    }
}