namespace KarpysDev.Script.Spell.DamageSpell
{
    using System.Collections.Generic;
    using Entities;
    using UnityEngine;

    [System.Serializable]
    public class WeaponDamageSource : DamageSource
    {
        [Header("Weapon Damage Specifics")]
        [Range(0,1f)]public float ConversionPercentage = 0;
        public WeaponTarget WeaponTarget = WeaponTarget.AllWeapons;
        public WeaponDamageSource(float damage, SubDamageType damageType,float conversionPercentage,WeaponTarget weaponTarget) : base(damage, damageType)
        {
            ConversionPercentage = conversionPercentage;
            WeaponTarget = weaponTarget;
        }

        public WeaponDamageSource(WeaponDamageSource baseDamageSource) : base(baseDamageSource)
        {
            ConversionPercentage = baseDamageSource.ConversionPercentage;
            WeaponTarget = baseDamageSource.WeaponTarget;
        }

        public override void ToDamageSource(List<DamageSource> source,BoardEntity entity,float bonusDamage)
        {
            Debug.Log("To computed Damage source weapon");
            switch (WeaponTarget)
            {
                case WeaponTarget.AllWeapons:
                    //Todo: GetAllWeaponSource => foreach add//
                    SubDamageType allWeaponSource = DamageType == SubDamageType.None ? SubDamageType.Physical : DamageType;
                    source.Add(new DamageSource(50f *  ConversionPercentage,allWeaponSource));
                    return;
                case WeaponTarget.MainWeapon:
                    //Todo:GetMainWeapon and WeaponDamageType//
                    SubDamageType mainDamageType = DamageType == SubDamageType.None ? SubDamageType.Physical : DamageType;
                    source.Add(new DamageSource(50f *  ConversionPercentage,mainDamageType));
                    return;
                case WeaponTarget.OffWeapon:
                    //Todo:GetOffWeapon and DamageType//
                    SubDamageType offDamageType = DamageType == SubDamageType.None ? SubDamageType.Physical : DamageType;
                    source.Add(new DamageSource(50f *  ConversionPercentage,offDamageType));
                    return;
                default:
                    return;
            }
        }
    }
}