﻿using UnityEngine;

public class LeapCrashTrigger : DamageSpellTrigger
{
    public LeapCrashTrigger(DamageSpellScriptable damageSpellData) : base(damageSpellData)
    {
    }

    protected override void TileHit(Vector2Int tilePosition, TriggerSpellData spellData)
    {
        if(tilePosition == spellData.AttachedEntity.EntityPosition)
            return;
        
        base.TileHit(tilePosition, spellData);
    }

    public override void Trigger(TriggerSpellData spellData, SpellTiles spellTiles)
    {
        spellData.AttachedEntity.MoveTo(spellTiles.OriginTiles[0]);
        base.Trigger(spellData, spellTiles);
    }
}