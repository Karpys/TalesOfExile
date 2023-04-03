﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityLootable : Lootable
{
    [SerializeField] private int m_DrawCount = 3;
    [SerializeField] private ItemPoolType m_ItemPoolType = ItemPoolType.Tier1Items;
    
    private BoardEntity m_AttachedEntity = null;
    private void Awake()
    {
        m_AttachedEntity = GetComponent<BoardEntity>();
        ComputeLoot();
    }

    private void Start()
    {
        m_AttachedEntity.EntityEvent.OnDeath += SpawnLoot;
    }

    private void OnDestroy()
    {
        m_AttachedEntity.EntityEvent.OnDeath -= SpawnLoot;
    }

    protected override void ComputeLoot()
    {
        base.ComputeLoot();
        m_LootObjects = LootLibrary.Instance.ItemRequest(m_ItemPoolType,m_DrawCount);
    }

    protected override Vector2Int GetOriginPosition()
    {
        return Vector2Int.zero;
    }

    protected override Tile GetOriginTile()
    {
        return MapData.Instance.GetTile(m_AttachedEntity.EntityPosition);
    }

    protected override List<Tile> GetLootTiles()
    {
        MapData mapData = MapData.Instance;
        Vector2Int entityPosition = m_AttachedEntity.EntityPosition;
        
        foreach (Vector2Int position in m_LootZones)
        {
            Tile tile = mapData.GetTile(position + entityPosition);

            if (tile is {Walkable: true})
            {
                m_LootTiles.Add(tile);
            }
        }

        return m_LootTiles;
    }
}