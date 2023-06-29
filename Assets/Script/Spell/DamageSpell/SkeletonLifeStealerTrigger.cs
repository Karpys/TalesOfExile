using KarpysDev.Script.Entities;
using KarpysDev.Script.Manager;

namespace KarpysDev.Script.Spell.DamageSpell
{
    public class SkeletonLifeStealerTrigger : ProjectileDamageTrigger
    {
        private float m_PercentDamageHealed = 0;
        public SkeletonLifeStealerTrigger(DamageSpellScriptable damageSpellData, OriginType originType,float ratioDamageHealed) : base(
            damageSpellData,originType)
        {
            m_PercentDamageHealed = ratioDamageHealed;
        }

        protected override float DamageEntityStep(BoardEntity entity, TriggerSpellData spellData)
        {
            float healValue = base.DamageEntityStep(entity, spellData) * m_PercentDamageHealed;
            DamageManager.HealTarget(m_AttachedSpell.AttachedEntity, healValue, true, m_SpellAnimDelay);
            return healValue;
        }
    }
}