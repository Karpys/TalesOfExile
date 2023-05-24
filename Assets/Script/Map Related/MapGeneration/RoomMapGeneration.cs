using System.Collections.Generic;
using KarpysDev.Script.PathFinding;
using KarpysDev.Script.PathFinding.LinePath;
using UnityEngine;

namespace KarpysDev.Script.Map_Related.MapGeneration
{
    [CreateAssetMenu(menuName = "Map/Room", fileName = "New RoomMap", order = 0)]
    class RoomMapGeneration : FlatDefaultMapGeneration
    {

        [SerializeField] private WorldTile m_HoleTile = null;

        public override GenerationMapInfo Generate(MapData mapData)
        {
            GenerationMapInfo info = base.Generate(mapData);

            //Set Up Bresenham start / end lines
            int randomXRange = Random.Range((int)(m_Width * 0.33f),(int)(m_Width * 0.66f));
            Vector2Int startLinePoint = new Vector2Int(randomXRange, 0);
            randomXRange = Random.Range((int)(m_Width * 0.33f),(int)(m_Width * 0.66f));
            Vector2Int endLinePoint = new Vector2Int(randomXRange, m_Height - 1);

            List<Vector2Int> linePath = LinePath.GetPathTile(startLinePoint, endLinePoint, NeighbourType.Square);

            for (int i = 0; i < linePath.Count; i++)
            {
                Vector2Int linePos = linePath[i];

                m_Map.TryPlaceTileAt(m_HoleTile, linePos);

                int distanceFromStart = (m_Height/2) - Mathf.Abs(linePos.y - (m_Height / 2));
                for (int x = 1; x < distanceFromStart; x++)
                {
                    m_Map.TryPlaceTileAt(m_HoleTile, linePos + new Vector2Int(x,0));
                    m_Map.TryPlaceTileAt(m_HoleTile, linePos + new Vector2Int(-x,0));
                }
            }

            m_Map.TryPlaceTileAt(m_HoleTile, startLinePoint);

            info.StartPosition = startLinePoint;
            return info;
        }
    }
}