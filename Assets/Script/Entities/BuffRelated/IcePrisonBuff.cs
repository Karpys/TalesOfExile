using KarpysDev.Script.Manager;
using KarpysDev.Script.Spell;
using KarpysDev.Script.Spell.DamageSpell;

namespace KarpysDev.Script.Entities.BuffRelated
{
    public class IcePrisonBuff : Buff
    {
        protected override void Apply()
        {
            m_Receiver.EntityStats.AddStunLock(1);
            m_Receiver.EntityEvent.OnGetDamageFromSpell += ImmunePhysicalDamage;
        }

        private void ImmunePhysicalDamage(BoardEntity entity, DamageSource mitigiedDamageSource, TriggerSpellData spellData)
        {
            if (mitigiedDamageSource.DamageType == SubDamageType.Physical)
            {
                mitigiedDamageSource.Damage = 0;
            }
        }

        protected override void UnApply()
        {
            DamageManager.HealTarget(m_Receiver, m_BuffValue, true);
            m_Receiver.EntityStats.AddStunLock(-1);
            m_Receiver.EntityEvent.OnGetDamageFromSpell -= ImmunePhysicalDamage;
        }
    }
}