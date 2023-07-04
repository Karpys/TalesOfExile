using UnityEngine;

namespace KarpysDev.Script.Map_Related.MapGeneration
{
    [CreateAssetMenu(menuName = "Map/Flat DefaultMap", fileName = "New FlatMap", order = 0)]
    public class FlatDefaultMapGeneration : MapGenerationData
    {
        public override GenerationMapInfo Generate(MapData mapData)
        {
            GenerationMapInfo info = base.Generate(mapData);
        
            for (int x = 0; x < m_Width; x++)
            {
                for (int y = 0; y < m_Height; y++)
                {
                    m_Map.Tiles[x, y] = new Tile(x,y);
                    m_Map.PlaceTileAt(m_BaseTile, x, y);
                    OnGenerateBaseTile(x,y);
                }
            }

            return info;
        }

        protected virtual void OnGenerateBaseTile(int x, int y)
        {
            return;
        }
    }
}