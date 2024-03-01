namespace KarpysDev.Script.Spell
{
    using System.Collections.Generic;
    using DamageSpell;
    using UnityEngine;

    public interface ILevelScaler
    {
        public void Apply(TriggerSpellData triggerSpellData);
    }

    //Todo: Change Weapon DamageSource Conversion
    public class WeaponConversionDamageAmplifier : ILevelScaler
    {
        public void Apply(TriggerSpellData triggerSpellData)
        {
            
        }
    }

    public interface IDamageProvider
    {
        public List<DamageSource> DamageSources { get ;}
    }
}