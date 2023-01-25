using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private int m_XPosition = 0;
    [SerializeField] private int m_YPosition = 0;
    [SerializeField] private bool m_Walkable = true;

    //Properties//
    public int XPos => m_XPosition;
    public int YPos => m_YPosition;
    public Vector2Int TilePosition => new Vector2Int(XPos, YPos);

    public bool Walkable
    {
        get => m_Walkable;
        set => m_Walkable = value;
    }
    //Path Finding//
    public int gCost;
    public int hCost;

    private Tile m_ParentTile = null;
    public int fCost => gCost + hCost;

    public Tile ParentTile
    {
        get => m_ParentTile;
        set => m_ParentTile = value;
    }
    //
    public void Initialize(int x,int y)
    {
        m_XPosition = x;
        m_YPosition = y;
    }
}