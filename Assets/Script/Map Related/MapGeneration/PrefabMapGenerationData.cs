namespace KarpysDev.Script.Map_Related.MapGeneration
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Map/SpriteMap/Prefab Map", fileName = "PrefabMap", order = 0)]

    public class PrefabMapGenerationData : MapGenerationData
    {
        [SerializeField] private GameObject m_Map = null;

        public override GenerationMapInfo Generate(MapData mapData)
        {
            GenerationMapInfo info = base.Generate(mapData);
            Instantiate(m_Map);
            return info;
        }
    }
}