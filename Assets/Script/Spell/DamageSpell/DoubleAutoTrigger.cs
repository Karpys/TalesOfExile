using UnityEngine;

namespace KarpysDev.Script.Spell.DamageSpell
{
    public class DoubleAutoTrigger : AutoAttackTrigger
    {
        private bool m_TriggerAnim = false;
        public DoubleAutoTrigger(DamageSpellScriptable damageSpellData, float baseWeaponDamageConvertion) : base(damageSpellData, baseWeaponDamageConvertion)
        {}

        public override void CastSpell(TriggerSpellData spellData, SpellTiles spellTiles,bool mainCast = true, float efficiency = 1)
        {
            m_TriggerAnim = false;
            base.CastSpell(spellData, spellTiles,mainCast,efficiency);
            base.CastSpell(spellData, spellTiles,mainCast,efficiency);
        }

        protected override void TriggerOnHitFx(Vector3 entityPosition, Transform transform, params object[] args)
        {
            if (m_TriggerAnim == false)
            {
                base.TriggerOnHitFx(entityPosition, transform, args);
                m_TriggerAnim = true;
            }
        }
    }
}