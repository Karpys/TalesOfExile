using System;
using UnityEngine;

public abstract class SpellDataScriptable : ScriptableObject
{
    public string SpellKey = String.Empty;
    public SpellType SpellType = SpellType.Trigger;
}

public enum SpellType
{
    Trigger,
    Support,
    Buff,
}