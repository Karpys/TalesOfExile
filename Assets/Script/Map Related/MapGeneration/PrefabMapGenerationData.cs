namespace KarpysDev.Script.Map_Related.MapGeneration
{
    using PrefabMap;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Map/SpriteMap/Prefab Map", fileName = "PrefabMap", order = 0)]

    public class PrefabMapGenerationData : MapGenerationData
    {
        [SerializeField] private MapDataHolder m_MapDataHolder = null;
        
        public void UpdateData()
        {
            m_Width = m_MapDataHolder.MapDimension.x;
            m_Height = m_MapDataHolder.MapDimension.y;
            m_SpawnPosition = m_MapDataHolder.StartPosition;
        }

        public override GenerationMapInfo Generate(MapData mapData)
        {
            GenerationMapInfo info = base.Generate(mapData);
            MapDataHolder mapDataHolder = Instantiate(m_MapDataHolder);

            int count = 0;

            for (int x = 0; x < mapDataHolder.MapDimension.x; x++)
            {
                for (int y = 0; y < mapDataHolder.MapDimension.y; y++)
                {
                    m_Map.AssignWorldTile(mapDataHolder.WorldTiles[count],x,y,true);
                    count++;
                }
            }

            info.StartPosition = mapDataHolder.StartPosition;
            return info;
        }
    }
}