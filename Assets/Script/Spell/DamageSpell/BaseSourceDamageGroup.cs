namespace KarpysDev.Script.Spell.DamageSpell
{
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class BaseSourceDamageGroup : ScriptableObject
    {
        public abstract List<DamageSource> Init();
    }
}