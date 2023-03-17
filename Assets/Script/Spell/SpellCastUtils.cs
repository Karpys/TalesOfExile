using System.Collections.Generic;
using UnityEngine;

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

    public static bool IsRestricted(SpellRestrictionType type,Vector2Int targetPosition,TriggerSpellData spellData)
    {
        switch (type)
        {
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
            default:
                return false;
        }
    }
}
