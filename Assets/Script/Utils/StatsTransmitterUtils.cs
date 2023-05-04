using System;
using System.Collections.Generic;

public static class StatsTransmitterUtils
{
    public static readonly Dictionary<StatType, Action<EntityStats, Modifier,float>> transmitterAction;

    static StatsTransmitterUtils()
    {
        transmitterAction = new Dictionary<StatType, Action<EntityStats, Modifier, float>>()
        {
            {StatType.PhysicalDamage, (s, m, v) => m.Transmut(ModifierType.UpPhysical, s.PhysicalDamageModifier * v)},
            //{StatType.ElementalDamage, (s, m, v) => m.Transmut(ModifierType.UpElemental, s.ElementalDamageModifier * v)},
            {StatType.FireDamage, (s, m, v) => m.Transmut(ModifierType.UpFire, s.FireDamageModifier * v)},
            {StatType.ColdDamage, (s, m, v) => m.Transmut(ModifierType.UpCold, s.ColdDamageModifier * v)},
            //{StatType.HolyDamage, (s, m, v) => m.Transmut(ModifierType.UpHoly, s.HolyDamageModifier * v)},
        };
    }
}