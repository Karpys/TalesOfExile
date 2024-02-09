using System;
using System.Collections.Generic;
using KarpysDev.Script.Entities;
using KarpysDev.Script.Entities.EquipementRelated;
using KarpysDev.Script.Spell;

namespace KarpysDev.Script.Utils
{
    public static class StatsTransmitterUtils
    {
        public static readonly Dictionary<StatType, Action<EntityStats, Modifier,float>> transmitterAction;

        static StatsTransmitterUtils()
        {
            transmitterAction = new Dictionary<StatType, Action<EntityStats, Modifier, float>>()
            {
                {StatType.PhysicalDamage, (s, m, v) => m.Transmut(ModifierType.UpPhysical, s.DamageTypeModifier.GetTypeValue(SubDamageType.Physical) * v)},
                {StatType.FireDamage, (s, m, v) => m.Transmut(ModifierType.UpFire, s.DamageTypeModifier.GetTypeValue(SubDamageType.Fire) * v)},
                {StatType.ColdDamage, (s, m, v) => m.Transmut(ModifierType.UpCold, s.DamageTypeModifier.GetTypeValue(SubDamageType.Cold) * v)},
                {StatType.WeaponForce, (s, m, v) => m.Transmut(ModifierType.IncreaseWeaponForce, s.WeaponForce * v)},
            };
        }
    }
}