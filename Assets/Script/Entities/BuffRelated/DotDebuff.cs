using System;
using KarpysDev.Script.Manager;
using KarpysDev.Script.Spell;
using KarpysDev.Script.Spell.DamageSpell;
using UnityEngine;

namespace KarpysDev.Script.Entities.BuffRelated
{
    public class DotDebuff : Buff
    {
        [Header("Dot Debuff")]
        [SerializeField] private SubDamageType m_BaseDamageType;
        
        private DamageSource m_BaseDamageSource = null;
        private DamageSource m_ComputedDamageSource = null;
        protected override void Apply()
        {
            m_BaseDamageSource = new DamageSource(m_BuffValue, m_BaseDamageType);
            m_ComputedDamageSource = new DamageSource(GetDamage(),m_BaseDamageSource.DamageType);

            //Need recompute damage on m_caster.OnRecomputeSpell ?//
            m_Receiver.EntityEvent.OnBehave += TakeDamage;
        }

        private float GetDamage()
        {
            float damage = m_BaseDamageSource.Damage * DamageManager.GetDamageModifier(m_BaseDamageType, m_Caster.EntityStats); 
            return damage;
        }

        private void TakeDamage()
        {
            DamageManager.DirectDamage(m_Receiver,m_ComputedDamageSource,m_Caster);
        }

        protected override void UnApply()
        {
            m_Receiver.EntityEvent.OnBehave -= TakeDamage;
        }
    }
}