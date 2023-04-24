using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Lootable:MonoBehaviour
{
    [SerializeField] protected Zone m_LootZoneSelection = null;
    [SerializeField] protected bool m_IsStatic = false;
    
    protected List<Vector2Int> m_LootZones = new List<Vector2Int>();
    protected List<Item> m_LootObjects = new List<Item>();
    protected List<Tile> m_LootTiles = new List<Tile>();


    protected virtual void ComputeLoot()
    {
        Vector2Int originPosition = m_IsStatic ? GetOriginPosition() : Vector2Int.zero;
        m_LootZones = ZoneTileManager.GetSelectionZone(m_LootZoneSelection, originPosition, m_LootZoneSelection.Range);
    }

    protected abstract Vector2Int GetOriginPosition();
    protected abstract Tile GetOriginTile();

    protected virtual void SpawnLoot()
    {
        LootController.Instance.SpawnLootFrom(m_LootObjects,GetOriginTile(),GetLootTiles());
    }

    protected abstract List<Tile> GetLootTiles();
}