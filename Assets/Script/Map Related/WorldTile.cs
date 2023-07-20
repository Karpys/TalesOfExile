using System;
using UnityEngine;

namespace KarpysDev.Script.Map_Related
{
    public class WorldTile : MonoBehaviour
    {
        [SerializeField] private bool m_Walkable = true;
        protected Tile m_AttachedTile = null;

        public Tile Tile => m_AttachedTile;

        public void SetTile(Tile tile)
        {
            tile.SetWorldTile(this);
            m_AttachedTile = tile;
            tile.Walkable = m_Walkable;
        }
    }
}