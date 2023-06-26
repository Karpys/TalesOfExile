using KarpysDev.Script.Manager;
using KarpysDev.Script.Spell;
using KarpysDev.Script.Spell.DamageSpell;
using UnityEngine;

namespace KarpysDev.Script.Entities.BuffRelated
{
    public class OnKillTriggerSpell : Buff
    {
        [SerializeField] private SpellInfo m_SpellInfo = null;
        [SerializeField] private int m_MaxTriggerPerTurn = 300;
    
        private TriggerSpellData m_TriggerSpellData = null;
        private int m_CurrentCount = 0;
        protected override void Apply()
        {
            m_TriggerSpellData = m_Receiver.RegisterSpell(m_SpellInfo);
        
            ((DamageSpellTrigger)m_TriggerSpellData.SpellTrigger).SetInitialDamageSource(m_BuffValue);
            m_TriggerSpellData.SpellTrigger.ComputeSpellData(m_Receiver);

            m_Receiver.EntityEvent.OnKill += TriggerSpell;
            m_Receiver.EntityEvent.OnSpellRecompute += Recompute;
            GameManager.Instance.A_OnEndTurn += Reset;
        }

        private void Reset()
        {
            m_CurrentCount = 0;
        }

        private void Recompute()
        {
            m_TriggerSpellData.SpellTrigger.ComputeSpellData(m_Receiver);
        }

        protected override void OnPassiveValueChanged()
        {
            ((DamageSpellTrigger)m_TriggerSpellData.SpellTrigger).SetInitialDamageSource(m_BuffValue);
        }

        private void TriggerSpell(BoardEntity entityKilled)
        {
            Debug.Log("Trigger");
            Debug.Log(m_CurrentCount);
            if(m_CurrentCount >= m_MaxTriggerPerTurn)
                return;
            m_CurrentCount += 1;
            SpellCastUtils.TriggerSpellAt(m_TriggerSpellData, entityKilled.EntityPosition, m_Caster.EntityPosition);
        }

        protected override void UnApply()
        {
            m_Receiver.EntityEvent.OnKill -= TriggerSpell;
            m_Receiver.EntityEvent.OnSpellRecompute -= Recompute;
            GameManager.Instance.A_OnEndTurn -= Reset;

        }
    }
}