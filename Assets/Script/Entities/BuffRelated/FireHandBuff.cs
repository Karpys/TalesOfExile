using System;
using System.Linq;
using KarpysDev.Script.Manager;
using KarpysDev.Script.Spell;
using KarpysDev.Script.Spell.DamageSpell;
using UnityEngine;

namespace KarpysDev.Script.Entities.BuffRelated
{
    using Manager.Library;

    public class FireHandBuff : Buff
    {
        [Header("Fire Hand Buff")]
        [SerializeField] private SubDamageType m_BaseDamageType;

        private DamageSource m_BaseDamageSource = null;
        private DamageSource m_ComputedDamage = null;
        
        public FireHandBuff(BoardEntity caster, BoardEntity receiver, BuffType buffType,int cooldown, float buffValue,SubDamageType baseDamageType) : base(caster, receiver, buffType,cooldown, buffValue)
        {
            m_BaseDamageType = baseDamageType;
        }

        public override void Apply()
        {
            m_BaseDamageSource = new DamageSource(m_BuffValue, m_BaseDamageType);
            m_ComputedDamage = new DamageSource(m_BaseDamageSource);
            
            m_Receiver.EntityEvent.OnRequestAdditionalSpellSource += AddFireDamage;
            m_Receiver.ComputeAllSpells();
        }

        private void AddFireDamage(DamageSpellTrigger spellTrigger)
        {
            if (spellTrigger.SpellData.Data.SpellGroups.Contains(SpellGroup.AutoAttack))
            {
                m_ComputedDamage.Damage = m_BaseDamageSource.Damage * DamageManager.GetDamageModifier(m_BaseDamageType, m_Receiver.EntityStats);
                spellTrigger.AddDamageSource(m_ComputedDamage);
            }
        }

        protected override void UnApply()
        {
            m_Receiver.EntityEvent.OnRequestAdditionalSpellSource -= AddFireDamage;
            m_Receiver.ComputeAllSpells();
        }
    }
}