using UnityEngine;

public abstract class BaseSpellTriggerScriptable : ScriptableObject
{
    [Header("Spell Animation")]
    public SpellAnimation OnTileHitAnimation = null;
    public SpellAnimation OnHitAnimation = null;
    public abstract BaseSpellTrigger SetUpTrigger();
}