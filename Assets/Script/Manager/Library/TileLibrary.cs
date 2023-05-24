using System;
using KarpysDev.Script.Map_Related;
using KarpysDev.Script.Widget;
using UnityEngine;

namespace KarpysDev.Script.Manager.Library
{
    public class TileLibrary : SingletonMonoBehavior<TileLibrary>
    {
        [SerializeField] private GenericLibrary<WorldTile, TileType> m_TileLibrary = null;

        private void Awake()
        {
            m_TileLibrary.InitializeDictionary();
        }

        public WorldTile GetTileViaKey(TileType type)
        {
            return m_TileLibrary.GetViaKey(type);
        }
    }

    [Serializable]
    public class TileKey
    {
        [SerializeField] private TileType m_Type = TileType.None;
        [SerializeField] private WorldTile m_Tile = null;

        public TileType Type => m_Type;
        public WorldTile Tile => m_Tile;
    }

    public enum TileType
    {
        None,
        IceWall,
    }
}