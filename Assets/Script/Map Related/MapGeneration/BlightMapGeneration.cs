using KarpysDev.Script.Map_Related.Blight;
using UnityEngine;

namespace KarpysDev.Script.Map_Related.MapGeneration
{
    [CreateAssetMenu(menuName = "Map/Blight", fileName = "BlightMapGeneration", order = 0)]
    public class BlightMapGeneration : FlatDefaultMapGeneration
    {
        [SerializeField] private BlightCore m_BlightCore = null;

        private BlightCore m_BlightCoreGenerated = null;
        protected override void OnGenerateBaseTile(int x, int y)
        {
            if (x == m_Width / 2 && y == m_Height / 2)
            {
                BlightCore blightCore = (BlightCore)m_Map.PlaceTileAt(m_BlightCore,x,y);
                m_BlightCoreGenerated = blightCore;
            }
        
            base.OnGenerateBaseTile(x, y);
        }

        public override GenerationMapInfo Generate(MapData mapData)
        {
            GenerationMapInfo info = base.Generate(mapData);
            m_BlightCoreGenerated.Initalize(m_Map);
            info.StartPosition = new Vector2Int(m_Width / 2, m_Height / 2 - 2);
            return info;
        }
    }
}