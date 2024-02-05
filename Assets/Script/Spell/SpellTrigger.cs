using KarpysDev.Script.Spell.SpellFx;
using UnityEngine;

namespace KarpysDev.Script.Spell
{
    public abstract class BaseSpellTriggerScriptable : ScriptableObject
    {
        [Header("Spell Animation")]
        public SpellAnimation OnTileHitAnimation = null;
        public SpellAnimation OnHitAnimation = null;
        public SpellAnimation CenterOriginAnimation = null;
        public abstract BaseSpellTrigger SetUpTrigger();
    }
}