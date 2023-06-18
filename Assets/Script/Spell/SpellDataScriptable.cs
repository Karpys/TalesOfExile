using System;
using UnityEngine;

namespace KarpysDev.Script.Spell
{
    public abstract class SpellDataScriptable : ScriptableObject
    {
        [Header("Base Spell Data")]
        public string SpellKey = String.Empty;
        public SpellType SpellType = SpellType.Trigger;
        public SpellGroup[] SpellGroups = Array.Empty<SpellGroup>();
        
        [Header("Spell Description (&0..&1) => place holder")]
        public string BaseDescription = String.Empty;
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
        BuffToggle,
        //Ect ect everything that can fit in a spellGroup//
    }
}