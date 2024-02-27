using UnityEngine;

namespace KarpysDev.Script.Spell
{
    public abstract class BaseSpellLevelScalerScriptable : ScriptableObject
    {
        public abstract ILevelScaler GetBaseSpellLevelScaler();
    }
}