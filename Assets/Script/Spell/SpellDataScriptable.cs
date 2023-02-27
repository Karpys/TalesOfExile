using UnityEngine;

public abstract class SpellDataScriptable : ScriptableObject
{
    public SpellType SpellType = SpellType.Trigger;
}

public enum SpellType
{
    Trigger,
    Support,
}