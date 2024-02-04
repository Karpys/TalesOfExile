using KarpysDev.Script.Map_Related;
using KarpysDev.Script.Spell.SpellFx;
using UnityEngine;

namespace KarpysDev.Script.Spell.DamageSpell
{
    public class SwordDanceTrigger : WeaponDamageTrigger
    {
        public SwordDanceTrigger(DamageSpellScriptable damageSpellData, float baseWeaponDamageConvertion) : base(damageSpellData, baseWeaponDamageConvertion)
        {}

        protected override void Trigger(TriggerSpellData spellData, SpellTiles spellTiles, CastInfo castInfo, float efficiency = 1)
        {
            base.CreateOnHitFx(MapData.Instance.GetTilePosition(spellTiles.CenterOrigin),null);
            base.Trigger(spellData, spellTiles, castInfo, efficiency);
        }

        protected override SpellAnimation CreateOnHitFx(Vector3 entityPosition, Transform transform)
        {
            return null;
        }
    }
}