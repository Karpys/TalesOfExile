using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private int m_XPosition = 0;
    [SerializeField] private int m_YPosition = 0;

    public void Initialize(int x,int y)
    {
        m_XPosition = x;
        m_YPosition = y;
    }
}