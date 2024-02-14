using System.Linq;
using KarpysDev.Script.Spell;
using UnityEngine;

namespace KarpysDev.Script.Entities.BuffRelated
{
    using Manager.Library;

    public class HolyAttackBuff : Buff
    {
        private SpellInfo m_OnAutoTrigger = null;
        private TriggerSpellData m_Trigger = null;
        
        public HolyAttackBuff(BoardEntity caster, BoardEntity receiver,BuffType buffType,BuffGroup buffGroup, int cooldown, float buffValue,SpellInfo onAutoTrigger) : base(caster, receiver,buffType, buffGroup, cooldown, buffValue)
        {
            m_OnAutoTrigger = onAutoTrigger;
        }

        public override void Apply()
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
            if (castInfo is DamageCastInfo damageCastInfo)
            {
                if(damageCastInfo.HitEntity.Count > 0)
                    SpellCastUtils.TriggerSpellAt(m_Trigger,m_Receiver.EntityPosition,m_Receiver.EntityPosition);
            }
        }

        protected override void UnApply()
        {
            m_Receiver.EntityEvent.OnRequestCastEvent -= AddAutoCast;
            m_Receiver.ComputeAllSpells();
        }
    }
}