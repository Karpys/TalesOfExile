using System.Collections.Generic;
using KarpysDev.Script.Spell;
using KarpysDev.Script.Utils;
using UnityEngine;

namespace KarpysDev.Script.Map_Related.MapGeneration
{
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

        public OuterSquareDigRoom(Map map, WorldTile holeTile, float fillPercentage, ZoneType fillType, float shrinkChance):base(map)
        {
            m_HoleTile = holeTile;
            m_ShrinkChance = shrinkChance;
            fillPercentage /= 100; 
            int fillValue = Mathf.CeilToInt(map.Width * fillPercentage / 2);
            m_HoleSelection = new Zone(fillType, fillValue);
            m_ShrinkSelection = new Zone(ZoneTileManager.GetOuter(fillType), fillValue);
            int shrinkZone =Mathf.CeilToInt(m_Map.Width * 0.15f);
            m_ShrinkZone = new Zone(fillType, shrinkZone);
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
}