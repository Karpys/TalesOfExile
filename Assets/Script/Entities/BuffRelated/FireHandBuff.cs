using System;
using System.Linq;
using KarpysDev.Script.Manager;
using KarpysDev.Script.Spell;
using KarpysDev.Script.Spell.DamageSpell;
using UnityEngine;

namespace KarpysDev.Script.Entities.BuffRelated
{
    public class FireHandBuff : Buff
    {
        [Header("Fire Hand Buff")]
        [SerializeField] private SubDamageType m_BaseDamageType;
        [SerializeField] private SubDamageType[] m_SubDamageType = Array.Empty<SubDamageType>();

        private DamageSource m_BaseDamageSource = null;
        private DamageSource m_ScaleDamageSource = null;
        protected override void Apply()
        {
            m_BaseDamageSource = new DamageSource(m_BuffValue, m_BaseDamageType);
            m_ScaleDamageSource = new DamageSource(m_BaseDamageSource);
            
            m_Receiver.EntityEvent.OnRequestAdditionalSpellSource += AddFireDamage;
            m_Receiver.ComputeAllSpells();
        }

        private void AddFireDamage(DamageSpellTrigger spellTrigger)
        {
            if (spellTrigger.SpellData.Data.SpellGroups.Contains(SpellGroup.AutoAttack))
            {
                m_ScaleDamageSource.Damage = m_BaseDamageSource.Damage * DamageManager.GetDamageModifier(m_SubDamageType, m_Receiver.EntityStats);
                spellTrigger.AddDamageSource(m_ScaleDamageSource);
            }
        }

        protected override void UnApply()
        {
            m_Receiver.EntityEvent.OnRequestAdditionalSpellSource -= AddFireDamage;
            m_Receiver.ComputeAllSpells();
        }
    }
}