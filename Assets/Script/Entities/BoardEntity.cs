using System;
using UnityEngine;

public class BoardEntity : MonoBehaviour
{
    [SerializeField] private int m_XPosition = 0;
    [SerializeField] private int m_YPosition = 0;

    protected MapData m_TargetMap = null;

    public Vector2Int EntityPosition => new Vector2Int(m_XPosition, m_YPosition);
    public void Place(int x, int y, MapData targetMap)
    {
        m_XPosition = x;
        m_YPosition = y;
        m_TargetMap = targetMap;
        transform.position = targetMap.GetTilePosition(x, y);
    }

    public virtual void MoveTo(int x,int y)
    {
        m_XPosition = x;
        m_YPosition = y;
    }
    
    public void MoveTo(Vector2Int pos)
    {
        MoveTo(pos.x,pos.y);
    }
}