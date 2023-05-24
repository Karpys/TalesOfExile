using System;

namespace KarpysDev.Script.Spell.DamageSpell
{
    [Serializable]
    public class DamageParameters
    {
        public DamageType DamageType = null;
        public DamageSource InitialSourceDamage;

        public DamageParameters(DamageParameters damageParameters)
        {
            DamageType = new DamageType(damageParameters.DamageType);
            InitialSourceDamage = new DamageSource(damageParameters.InitialSourceDamage);
        }
    }
}