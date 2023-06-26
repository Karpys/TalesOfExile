using System;
using System.Collections.Generic;
using KarpysDev.Script.Entities;
using KarpysDev.Script.Entities.EquipementRelated;

namespace KarpysDev.Script.Utils
{
    public static class StatsTransmitterUtils
    {
        public static readonly Dictionary<StatType, Action<EntityStats, Modifier,float>> transmitterAction;

        static StatsTransmitterUtils()
        {
            transmitterAction = new Dictionary<StatType, Action<EntityStats, Modifier, float>>()
            {
                {StatType.PhysicalDamage, (s, m, v) => m.Transmut(ModifierType.UpPhysical, s.PhysicalDamageModifier * v)},
                {StatType.ElementalDamage, (s, m, v) => m.Transmut(ModifierType.UpElemental, s.ElementalDamageModifier * v)},
                {StatType.FireDamage, (s, m, v) => m.Transmut(ModifierType.UpFire, s.FireDamageModifier * v)},
                {StatType.ColdDamage, (s, m, v) => m.Transmut(ModifierType.UpCold, s.ColdDamageModifier * v)},
                {StatType.WeaponForce, (s, m, v) => m.Transmut(ModifierType.IncreaseWeaponForce, s.WeaponForce * v)},
            };
        }
    }
}