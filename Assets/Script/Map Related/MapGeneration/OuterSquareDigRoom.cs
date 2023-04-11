using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Map/Room/OuterSquareDig", fileName = "New RoomMap", order = 0)]
public class OuterSquareDigRoom : MapGenerationData
{
    [SerializeField] private WorldTile m_HoleTile = null;
    [SerializeField] private Zone m_HoleSelection = null;
    
    [Header("Shrink Rules")]
    [SerializeField] private Zone m_ShrinkSelection = null;
    [SerializeField] private Zone m_ShrinkZone = null;
    [Range(0f,100f)]
    [SerializeField] private float m_ShrinkChance = 50f;
    public override GenerationMapInfo Generate(MapData mapData)
    {
        GenerationMapInfo info = base.Generate(mapData);

        info.StartPosition = new Vector2Int(m_Width / 2, (int)(m_Height * 0.25f));
        DigHole();
        Shrink();
        return info;
    }
    private void DigHole()
    {
        List<Vector2Int> holeZone = ZoneTileManager.GetSelectionZone(m_HoleSelection,new Vector2Int(m_Width/2,m_Height/2),m_HoleSelection.Range);

        foreach (Vector2Int holePos in holeZone)
        {
            m_Map.TryPlaceTileAt(m_HoleTile, holePos);
        }
    }
    
    private void Shrink()
    {
        List<Vector2Int> shrinkSelection = ZoneTileManager.GetSelectionZone(m_ShrinkSelection,new Vector2Int(m_Width/2,m_Height/2),m_ShrinkSelection.Range);

        foreach (Vector2Int shrinkSelectionPosition in shrinkSelection)
        {
            float random = Random.Range(0, 100f);

            if (random < m_ShrinkChance)
            {
                List<Vector2Int> shrinkZone = ZoneTileManager.GetSelectionZone(m_ShrinkZone,shrinkSelectionPosition,m_ShrinkZone.Range);

                foreach (Vector2Int shrinkZonePosition in shrinkZone)
                {
                    m_Map.TryPlaceTileAt(m_HoleTile, shrinkZonePosition);
                }
            }
        }
    }
}