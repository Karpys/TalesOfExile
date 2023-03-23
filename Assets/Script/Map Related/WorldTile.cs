using System;
using UnityEngine;

public class WorldTile : MonoBehaviour
{
    [SerializeField] private bool m_Walkable = true;
    private Tile m_AttachedTile = null;
    private LightTile m_LightTile = null;

    public Tile Tile => m_AttachedTile;
    public LightTile LightTile => m_LightTile;

    private void Start()
    {
        m_LightTile = GetComponent<LightTile>();
        m_LightTile.Init(m_AttachedTile);
    }

    public void SetTile(Tile tile)
    {
        tile.SetWorldTile(this);
        m_AttachedTile = tile;
        tile.Walkable = m_Walkable;
    }
}
