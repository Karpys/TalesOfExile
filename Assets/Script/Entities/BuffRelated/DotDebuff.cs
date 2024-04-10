using System;
using KarpysDev.Script.Manager;
using KarpysDev.Script.Spell;
using KarpysDev.Script.Spell.DamageSpell;
using UnityEngine;

namespace KarpysDev.Script.Entities.BuffRelated
{
    using Manager.Library;

    public class DotDebuff : Buff
    {
        private SubDamageType m_BaseDamageType;
        private DamageSource m_BaseDamageSource = null;
        private DamageSource m_ComputedDamageSource = null;
        
        public DotDebuff(BoardEntity caster, BoardEntity receiver,BuffType buffType,BuffGroup buffGroup,int cooldown, float buffValue,BuffCategory[] categories,bool ignoreFirstBehaveTurn,SubDamageType dotDamageType) : base(caster, receiver,buffType, buffGroup, cooldown, buffValue, categories,ignoreFirstBehaveTurn)
        {
            m_BaseDamageType = dotDamageType;
        }

        public override void Apply()
        {
            Debug.Log("On Apply");
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
            Debug.Log("Direct Damage");
            DamageManager.DirectDamage(m_Receiver,m_ComputedDamageSource,m_Caster);
        }

        protected override void UnApply()
        {
            m_Receiver.EntityEvent.OnBehave -= TakeDamage;
        }
    }
}