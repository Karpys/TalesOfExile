using KarpysDev.Script.Spell.DamageSpell;
using UnityEngine;

namespace KarpysDev.Script.Spell.ParameterLessSpell
{
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

        protected override void Trigger(TriggerSpellData spellData, SpellTiles spellTiles,CastInfo castInfo, float efficiency = 1)
        {
            spellData.AttachedEntity.MoveTo(spellTiles.FirstOrigin);
            base.Trigger(spellData, spellTiles,castInfo,efficiency);
        }
    }
}