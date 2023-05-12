﻿using UnityEngine;

public class RushTrigger : WeaponDamageTrigger
{
    public RushTrigger(DamageSpellScriptable damageSpellData, float baseWeaponDamageConvertion) : base(damageSpellData, baseWeaponDamageConvertion)
    {
    }
    public override void Trigger(TriggerSpellData spellData, SpellTiles spellTiles,CastInfo castInfo)
    {
        MoveToClosestFreeTile(spellData,spellTiles.OriginTiles[0]);
        base.Trigger(spellData, spellTiles,castInfo);
    }
    
    private void MoveToClosestFreeTile(TriggerSpellData spellData,Vector2Int position)
    {
        Tile closestFree = TileHelper.GetFreeClosestAround(MapData.Instance.GetTile(position),spellData.AttachedEntity.WorldPosition);
        spellData.AttachedEntity.MoveTo(closestFree.TilePosition);
    }
}