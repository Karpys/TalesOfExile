using System;

namespace KarpysDev.Script.Spell.DamageSpell
{
    using System.Collections.Generic;
    using Entities;
    using KarpysUtils;
    using UnityEngine;

    [Serializable]
    public class DamageSource
    {
        [SerializeField] protected float m_Damage = 10f;
        [SerializeField] private SubDamageType m_DamageType = SubDamageType.Physical;

        public float Damage
        {
            get => m_Damage;
            set => m_Damage = value;
        }

        public SubDamageType DamageType
        {
            get => m_DamageType;
            set => m_DamageType = value;
        }


        public DamageSource(float damage, SubDamageType damageType)
        {
            m_Damage = damage;
            m_DamageType = damageType;
        }

        public DamageSource(DamageSource baseDamageSource)
        {
            m_Damage = baseDamageSource.Damage;
            m_DamageType = baseDamageSource.DamageType;
        }

        public virtual void ToDamageSource(List<DamageSource> source,BoardEntity entity,float bonusDamage)
        {
            source.Add(new DamageSource(this));
        }

        public void PercentAmplifyBy(float percentage)
        {
            m_Damage *= (percentage + 100) / 100;
        }
        
        public void AmplifyBy(float multi)
        {
            m_Damage *= multi + 1;
        }
    }

    public enum WeaponTarget
    {
        AllWeapons,
        MainWeapon,
        OffWeapon,
        Unarmed,
    }
}