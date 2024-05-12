using System;
using UnityEngine;

namespace KarpysDev.Script.Map_Related
{
    public class WorldTile : MonoBehaviour
    {
        [SerializeField] private bool m_Walkable = true;
        [SerializeField] private Color m_MinimapColor = Color.white;
        protected Tile m_AttachedTile = null;

        public Tile Tile => m_AttachedTile;
        public Color MinimapColor => m_MinimapColor;

        public void SetTile(Tile tile)
        {
            tile.SetWorldTile(this);
            m_AttachedTile = tile;
            tile.Walkable = m_Walkable;
        }
    }
}