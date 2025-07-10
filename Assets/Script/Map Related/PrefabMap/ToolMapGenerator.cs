namespace KarpysDev.Script.Map_Related.PrefabMap
{
    using MapGeneration;
    using UnityEngine;

    public class ToolMapGenerator : MonoBehaviour
    {
        [SerializeField] private MapData m_MapData = null;
        [SerializeField] private MapGenerationData m_MapGenerationData = null;
        [SerializeField] private Transform m_DefaultMapDataParent = null;
        [SerializeField] private MapDataHolder m_DefaultMapDataHolder = null;

        private void Awake()
        {
            Generate();
        }

        public void Generate()
        {
            Clear();
            MapDataHolder holder = Instantiate(m_DefaultMapDataHolder, m_DefaultMapDataParent);
            PathFinding.PathFinding.mapData = m_MapData;
            m_MapData.TileHolder = holder.transform;
            GenerationMapInfoEditor generationMapInfoEditor = m_MapGenerationData.GenerateInEditor(m_MapData);
            holder.Initialize(new Vector2Int(m_MapGenerationData.Width,m_MapGenerationData.Height),generationMapInfoEditor.StartPosition, generationMapInfoEditor.WorldTiles);
        }

        public void Clear()
        {
            for (int i = m_MapData.TileHolder.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(m_MapData.TileHolder.GetChild(0).gameObject);
            }

            m_MapData.TileHolder = m_DefaultMapDataParent;
        }
    }
}