using System;
using UnityEngine;

namespace KarpysDev.Script.Spell
{
    public abstract class SpellDataScriptable : ScriptableObject
    {
        [Header("Base Spell Data")]
        [SerializeField] private string m_SpellName = String.Empty;
        [SerializeField] private SpellType m_SpellType = SpellType.Trigger;
        [SerializeField] private SpellGroup[] m_SpellGroups = Array.Empty<SpellGroup>();
        
        [Header("Spell Description (&0..&1) => place holder")]
        [SerializeField] private string m_BaseDescription = String.Empty;

        public SpellGroup[] SpellGroups => m_SpellGroups;
        public SpellType SpellType => m_SpellType;
        public string SpellName => m_SpellName;
        public string BaseDescription => m_BaseDescription;
    }

    public enum SpellType
    {
        Trigger,
        Support,
        Buff,
    }
}