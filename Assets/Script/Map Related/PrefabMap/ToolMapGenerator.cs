namespace KarpysDev.Script.Map_Related.PrefabMap
{
    using MapGeneration;
    using UnityEngine;

    public class ToolMapGenerator : MonoBehaviour
    {
        [SerializeField] private MapData m_MapData = null;
        [SerializeField] private MapGenerationData m_MapGenerationData = null;

        private void Awake()
        {
            Generate();
        }

        public void Generate()
        {
            PathFinding.PathFinding.mapData = m_MapData;
            m_MapGenerationData.GenerateInEditor(m_MapData);
        }

        public void Clear()
        {
            for (int i = m_MapData.TileHolder.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(m_MapData.TileHolder.GetChild(0).gameObject);
            }
        }
    }
}