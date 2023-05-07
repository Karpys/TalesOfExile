using System;
using UnityEngine;

public abstract class SpellDataScriptable : ScriptableObject
{
    [Header("Base Spell Data")]
    public string SpellKey = String.Empty;
    public SpellType SpellType = SpellType.Trigger;
    public SpellGroup[] SpellGroups = Array.Empty<SpellGroup>();
}

public enum SpellType
{
    Trigger,
    Support,
    Buff,
}


public enum SpellGroup
{
    Projectile,
    Spell,
    AutoAttack,
    Magic,
    Lightning,
    Rock,
    Ice,
    //Ect ect everything that can fit in a spellGroup//
}