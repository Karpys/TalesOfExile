using KarpysDev.Script.Spell.SpellFx;
using UnityEngine;

namespace KarpysDev.Script.Spell.DamageSpell
{
    public class DoubleAutoTrigger : AutoAttackTrigger
    {
        private bool m_TriggerAnim = false;
        public DoubleAutoTrigger(DamageSpellScriptable damageSpellData) : base(damageSpellData)
        {}

        public override void CastSpell(TriggerSpellData spellData, SpellTiles spellTiles,bool mainCast = true, float efficiency = 1)
        {
            m_TriggerAnim = false;
            base.CastSpell(spellData, spellTiles,mainCast,efficiency);
            base.CastSpell(spellData, spellTiles,mainCast,efficiency);
        }

        protected override SpellAnimation CreateOnHitFx(Vector3 entityPosition, Transform transform)
        {
            if (m_TriggerAnim == false)
            {
                m_TriggerAnim = true;
                return base.CreateOnHitFx(entityPosition, transform);
            }

            return null;
        }
    }
}