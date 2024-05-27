namespace KarpysDev.Script.Widget
{
    using System.Collections.Generic;
    using KarpysUtils;
    using Map_Related;
    using Map_Related.MapGeneration;
    using UnityEngine;

    public enum TileSetType
    {
        DirtRoad,
    }
    public class TileSetManager : SingletonMonoBehavior<TileSetManager>
    {
        [SerializeField] private GenericLibrary<TileSetType, TileSet> m_TileSetLibrary = null;

        private Dictionary<TileSetType, List<IVisualTile>> m_CurrentTileMap = new Dictionary<TileSetType, List<IVisualTile>>();
        private void Awake()
        {
            m_TileSetLibrary.InitializeDictionary();
        }

        public void AddVisualTile(IVisualTile visualTile,TileSetType tileSetType)
        {
            if (m_CurrentTileMap.TryGetValue(tileSetType, out var tile))
            {
                tile.Add(visualTile);
            }
            else
            {
                m_CurrentTileMap.Add(tileSetType,new List<IVisualTile>{visualTile});
            }
        }
        
        public void ApplyTileSet()
        {
            foreach(KeyValuePair<TileSetType, List<IVisualTile>> tiles in m_CurrentTileMap)
            {
                TileSet tileSet = m_TileSetLibrary.GetViaKey(tiles.Key);
                int count = tiles.Value.Count;
                
                for (int i = 0; i < count; i++)
                {
                    TileHelper.GenerateTileSet(tiles.Value,tileSet.TileMap);
                }
            }
        }
    }
}