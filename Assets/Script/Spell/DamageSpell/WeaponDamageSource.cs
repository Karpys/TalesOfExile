namespace KarpysDev.Script.Spell.DamageSpell
{
    using System.Collections.Generic;
    using Entities;
    using Items;
    using UnityEngine;

    [System.Serializable]
    public class WeaponDamageSource : DamageSource
    {
        [Header("Weapon Damage Specifics")]
        public float ConversionPercentage = 0;
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
            switch (WeaponTarget)
            {
                case WeaponTarget.AllWeapons:
                    List<WeaponItem> weaponItems = entity.EntityStats.WeaponItems;

                    foreach (WeaponItem weaponItem in weaponItems)
                    {
                        DamageSource weaponDamageSource = weaponItem.GetWeaponDamage(entity);
                        if (DamageType != SubDamageType.None)
                        {
                            weaponDamageSource.DamageType = DamageType;
                        }
                        weaponDamageSource.Damage *= ConversionPercentage;
                        source.Add(weaponDamageSource);
                    }
                    return;
                case WeaponTarget.MainWeapon:
                    DamageSource mainHandDamageSource = entity.EntityStats.MainHandWeapon.GetWeaponDamage(entity);
                    if (DamageType != SubDamageType.None)
                    {
                        mainHandDamageSource.DamageType = DamageType;
                    }
                    mainHandDamageSource.Damage *= ConversionPercentage;
                    source.Add(mainHandDamageSource);
                    return;
                case WeaponTarget.OffWeapon:
                    DamageSource offHandDamageSource = entity.EntityStats.OffHandWeapon.GetWeaponDamage(entity);
                    if (DamageType != SubDamageType.None)
                    {
                        offHandDamageSource.DamageType = DamageType;
                    }
                    offHandDamageSource.Damage *= ConversionPercentage;
                    source.Add(offHandDamageSource);
                    return;
                default:
                    return;
            }
        }
    }
}