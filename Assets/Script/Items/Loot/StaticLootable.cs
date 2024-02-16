﻿using System.Collections.Generic;
using System.Linq;
using KarpysDev.Script.Map_Related;
using UnityEngine;

namespace KarpysDev.Script.Items.Loot
{
    public abstract class StaticLootable : Lootable
    {
        protected override void ComputeLoot()
        {
            base.ComputeLoot();

            MapData mapData = MapData.Instance;
        
            foreach (Vector2Int position in m_LootZones)
            {
                Tile tile = mapData.GetTile(position);

                if (tile != null)
                {
                    m_LootTiles.Add(tile);
                }
            }
        }

        protected override List<Tile> GetLootTiles()
        {
            return m_LootTiles.Where(t => t.Walkable).ToList();
        }
    }
}