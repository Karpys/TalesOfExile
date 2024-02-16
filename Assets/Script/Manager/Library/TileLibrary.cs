using System;
using KarpysDev.Script.Map_Related;
using UnityEngine;

namespace KarpysDev.Script.Manager.Library
{
    using KarpysUtils;

    public class TileLibrary : SingletonMonoBehavior<TileLibrary>
    {
        [SerializeField] private GenericLibrary<TileType, VisualTile> m_TileLibrary = null;

        private void Awake()
        {
            m_TileLibrary.InitializeDictionary();
        }

        public VisualTile GetTileViaKey(TileType type)
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
        RockLifeWall,
    }
}