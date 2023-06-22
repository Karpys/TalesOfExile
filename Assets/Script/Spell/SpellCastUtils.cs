using System.Collections.Generic;
using KarpysDev.Script.Entities;
using KarpysDev.Script.Map_Related;
using KarpysDev.Script.PathFinding;
using KarpysDev.Script.Widget;
using UnityEngine;

namespace KarpysDev.Script.Spell
{
    public static class SpellCastUtils
    {
        public static bool CanCastSpellAt(TriggerSpellData spellData,Vector2Int targetPosition)
        {
            bool canCast = true;

            for (int i = 0; i < spellData.TriggerData.SpellRestrictions.Count; i++)
            {
                SpellRestriction spellRestriction = spellData.TriggerData.SpellRestrictions[i];
                canCast = !IsRestricted(spellRestriction.Type, targetPosition,spellData);
            }

            return canCast;
        }
    
        public static void GetSpellTargetOrigin(TriggerSpellData spellData,ref Vector2Int newOrigin)
        {
            switch (spellData.TriggerData.OriginSelection)
            {
                case SpellOriginType.RandomWalkableInsideMainSelection:
                    List<Vector2Int> selection = ZoneTileManager.GetSelectionZone(spellData.GetMainSelection().Zone,
                        spellData.AttachedEntity.EntityPosition, spellData.GetMainSelection().Zone.Range);

                    List<Tile> tileSelection = new List<Tile>();
                    foreach (Vector2Int pos in selection)
                    {
                        Tile tile  = MapData.Instance.GetTile(pos);

                        if (tile is {Walkable: true})
                            tileSelection.Add(tile);
                    }

                    if(tileSelection.Count == 0)
                        return;
                
                    newOrigin = tileSelection[Random.Range(0, tileSelection.Count)].TilePosition;
                    break;
                //Default Target position
                case SpellOriginType.ClosestEnemy:
                    break;
                case SpellOriginType.Self:
                    newOrigin = spellData.AttachedEntity.EntityPosition;
                    break;
                default:
                    Debug.LogError("Return target position per default due to spell target origin not implemented");
                    break;
            }
        }

        public static bool IsRestricted(SpellRestrictionType type,Vector2Int targetPosition,TriggerSpellData spellData)
        {
            switch (type)
            {
                case SpellRestrictionType.OriginOnEnemy:
                    return !MapData.Instance.GetEntityAt(targetPosition, spellData.AttachedEntity.TargetEntityGroup);
                case SpellRestrictionType.OriginOnWalkable:
                    return !MapData.Instance.IsWalkable(targetPosition);
                case SpellRestrictionType.FreeTileAroundEnemyTarget:
                
                    BoardEntity target = MapData.Instance.GetEntityAt(targetPosition, EntityHelper.GetInverseEntityGroup(spellData.AttachedEntity.EntityGroup));

                    if (target == null)
                        return true;
                
                    List<Tile> neighbours = TileHelper.GetNeighbours(MapData.Instance.Map.Tiles[targetPosition.x, targetPosition.y],NeighbourType.Square, MapData.Instance);
                
                    foreach (Tile neighbour in neighbours)
                    {
                        if (neighbour.Walkable)
                            return false;
                    }
                    return true;
                case SpellRestrictionType.IsBowUser:
                    return spellData.AttachedEntity.EntityStats.IsBowUser == 0;
                default:
                    return false;
            }
        }
    
        public static void CastSpell(TriggerSpellData spellData,SpellTiles spellTiles,float efficiency = 1,bool freeCast = false)
        {
            spellData.Cast(spellData,spellTiles,efficiency,freeCast);
        }

        public static void CastSpellAt(TriggerSpellData spellData,Vector2Int pos,Vector2Int originPosition,float efficiency = 1,bool freeCast = false)
        {
            SpellTiles spellTiles = GetSpellTilesInfo(spellData,pos, originPosition);
            CastSpell(spellData, spellTiles, efficiency,freeCast);
        }
    
        public static void TriggerSpellAt(TriggerSpellData spellData,Vector2Int pos,Vector2Int originPosition,float efficiency = 1)
        {
            SpellTiles spellTiles = GetSpellTilesInfo(spellData,pos, originPosition);
            spellData.SpellTrigger.TriggerSpell(spellData,spellTiles,null,efficiency);
        }

        public static SpellTiles GetSpellTilesInfo(TriggerSpellData spellData, Vector2Int pos, Vector2Int originPosition)
        {
            List<List<Vector2Int>> tilesActions = new List<List<Vector2Int>>();
            List<Vector2Int> originTiles = new List<Vector2Int>();

            for (int i = 0; i < spellData.TriggerData.m_Selection.Length; i++)
            {
                ZoneSelection currentSelection = spellData.TriggerData.m_Selection[i];
            
                if (currentSelection.ActionSelection)
                {  
                    Vector2Int origin = Vector2Int.zero;
                    if (currentSelection.Origin == ZoneOrigin.Self)
                    {
                        origin = originPosition;
                    }
                    else
                    {
                        origin = pos;
                    }
                
                    tilesActions.Add(ZoneTileManager.GetSelectionZone(currentSelection.Zone,origin,currentSelection.Zone.Range,originPosition));
                    originTiles.Add(origin);
                }
            }

            return new SpellTiles(originPosition,originTiles, tilesActions);
        }
    }
}
