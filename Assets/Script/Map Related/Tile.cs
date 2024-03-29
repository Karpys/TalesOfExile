﻿using UnityEngine;

public class Tile
{
    private int m_XPosition = 0;
    private int m_YPosition = 0;
    private bool m_Walkable = true;
    private WorldTile m_WorldTile = null;

    //Properties//
    public int XPos => m_XPosition;
    public int YPos => m_YPosition;
    public Vector2Int TilePosition => new Vector2Int(XPos, YPos);
    public WorldTile WorldTile => m_WorldTile;

    public bool Walkable
    {
        get => m_Walkable;
        set => m_Walkable = value;
    }
    //Path Finding//
    public float gCost;
    public float hCost;

    private Tile m_ParentTile = null;
    public float fCost => gCost + hCost;

    public Tile ParentTile
    {
        get => m_ParentTile;
        set => m_ParentTile = value;
    }
    //

    public Tile(int x, int y)
    {
        m_XPosition = x;
        m_YPosition = y;
    }

    public void SetWorldTile(WorldTile worldTile)
    {
        m_WorldTile = worldTile;
    }
}