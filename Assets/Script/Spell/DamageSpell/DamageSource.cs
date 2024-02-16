using System;

namespace KarpysDev.Script.Spell.DamageSpell
{
    using System.Collections.Generic;
    using Entities;

    [Serializable]
    public class DamageSource
    {
        public float Damage = 10;
        public SubDamageType DamageType = SubDamageType.Physical;

        public DamageSource(float damage, SubDamageType damageType)
        {
            Damage = damage;
            DamageType = damageType;
        }

        public DamageSource(DamageSource baseDamageSource)
        {
            Damage = baseDamageSource.Damage;
            DamageType = baseDamageSource.DamageType;
        }

        public virtual void ToDamageSource(List<DamageSource> source,BoardEntity entity,float bonusDamage)
        {
            source.Add(new DamageSource(this));
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