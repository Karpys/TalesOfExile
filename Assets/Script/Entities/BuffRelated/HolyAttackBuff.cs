using System.Linq;
using KarpysDev.Script.Spell;
using UnityEngine;

namespace KarpysDev.Script.Entities.BuffRelated
{
    public class HolyAttackBuff : Buff
    {
        [SerializeField] private SpellInfo m_OnAutoTrigger = null;

        private TriggerSpellData m_Trigger = null;
        protected override void Apply()
        {
            m_Trigger = m_Receiver.RegisterSpell(m_OnAutoTrigger);

            if (m_Trigger.SpellTrigger is HealSelectionSpellTrigger healTrigger)
            {
                healTrigger.SetHealValue(m_BuffValue);
            }
            
            m_Receiver.EntityEvent.OnRequestCastEvent += AddAutoCast;
            m_Receiver.ComputeAllSpells();
        }
        
        private void AddAutoCast(BaseSpellTrigger spell)
        {
            if (spell.SpellData.Data.SpellGroups.Contains(SpellGroup.AutoAttack))
            {
                spell.OnCastSpell += TriggerOnAutoAttack;
            }
        }

        private void TriggerOnAutoAttack(CastInfo castInfo)
        {
            SpellCastUtils.TriggerSpellAt(m_Trigger,m_Receiver.EntityPosition,m_Receiver.EntityPosition);
        }
        
        protected override void UnApply()
        {
            m_Receiver.EntityEvent.OnRequestCastEvent -= AddAutoCast;
            m_Receiver.ComputeAllSpells();
        }
    }
}