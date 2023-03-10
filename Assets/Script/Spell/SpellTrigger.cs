using UnityEngine;

public abstract class BaseSpellTriggerScriptable : ScriptableObject
{
    [Header("Spell Animation")]
    public SpellAnimation OnTileHitAnimation = null;
    public SpellAnimation OnHitAnimation = null;
    public bool AdditionalAnimDatas = false;
    public abstract BaseSpellTrigger SetUpTrigger();
}