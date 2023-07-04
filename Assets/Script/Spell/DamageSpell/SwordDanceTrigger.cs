using KarpysDev.Script.Map_Related;
using UnityEngine;

namespace KarpysDev.Script.Spell.DamageSpell
{
    public class SwordDanceTrigger : WeaponDamageTrigger
    {
        public SwordDanceTrigger(DamageSpellScriptable damageSpellData, float baseWeaponDamageConvertion) : base(damageSpellData, baseWeaponDamageConvertion)
        {}

        protected override void Trigger(TriggerSpellData spellData, SpellTiles spellTiles, CastInfo castInfo, float efficiency = 1)
        {
            base.TriggerOnHitFx(MapData.Instance.GetTilePosition(spellTiles.CenterOrigin), null, null);
            base.Trigger(spellData, spellTiles, castInfo, efficiency);
        }

        protected override void TriggerOnHitFx(Vector3 entityPosition, Transform transform, params object[] args)
        {
            return;
        }
    }
}