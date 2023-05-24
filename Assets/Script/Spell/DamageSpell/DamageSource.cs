using System;

namespace KarpysDev.Script.Spell.DamageSpell
{
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
    }
}