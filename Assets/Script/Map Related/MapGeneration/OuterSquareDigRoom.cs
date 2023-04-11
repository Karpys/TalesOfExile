using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class OuterSquareDigRoom:Room
{
    private WorldTile m_HoleTile = null;
    private Zone m_HoleSelection = null;
    
    private Zone m_ShrinkSelection = null;
    private Zone m_ShrinkZone = null;
    private float m_ShrinkChance = 50f;

    public OuterSquareDigRoom(Map map,WorldTile holeTile,Zone centerHole,Zone shrinkSelection,Zone shrinkZone,float shrinkChance) : base(map)
    {
        m_HoleTile = holeTile;
        m_HoleSelection = centerHole;
        m_ShrinkSelection = shrinkSelection;
        m_ShrinkZone = shrinkZone;
        m_ShrinkChance = shrinkChance;
    }
    public override void Generate()
    {
        DigHole();
        Shrink();
    }
    private void DigHole()
    {
        MapHelper.ZoneTryPlaceTile(m_HoleTile,m_HoleSelection,new Vector2Int(m_Map.Width/2,m_Map.Height/2),m_Map);
    }
    
    private void Shrink()
    {
        List<Vector2Int> shrinkSelection = ZoneTileManager.GetSelectionZone(m_ShrinkSelection,new Vector2Int(m_Map.Width/2,m_Map.Height/2),m_ShrinkSelection.Range);

        foreach (Vector2Int shrinkSelectionPosition in shrinkSelection)
        {
            float random = Random.Range(0, 100f);

            if (random < m_ShrinkChance)
            {
                MapHelper.ZoneTryPlaceTile(m_HoleTile,m_ShrinkZone,shrinkSelectionPosition,m_Map);
            }
        }
    }
}

public abstract class Room
{
    protected Map m_Map = null;

    public Map Map => m_Map;
    public Room(Map map)
    {
        m_Map = map;
    }

    public abstract void Generate();
}