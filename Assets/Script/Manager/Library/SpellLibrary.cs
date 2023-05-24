﻿using System.Collections.Generic;
using KarpysDev.Script.Entities;
using UnityEngine;

namespace KarpysDev.Script.Manager.Library
{
    public class SpellLibrary : SingletonMonoBehavior<SpellLibrary>
    {
        [SerializeField] private List<SpellInfo> m_SpellList = new List<SpellInfo>();
        public Dictionary<string, SpellInfo> Spells = new Dictionary<string, SpellInfo>();

        private void Awake()
        {
            foreach (SpellInfo spellData in m_SpellList)
            {
                Spells.Add(spellData.m_SpellData.SpellKey,spellData);
            }
        }

        public SpellInfo GetSpellViaKey(string spellKey)
        {
            SpellInfo spellData = null;
            Spells.TryGetValue(spellKey,out spellData);

            if (spellData == null)
            {
                Debug.LogError("Spell Key not found");
                return null;
            }
        
            return new SpellInfo(spellData.m_SpellData,spellData.m_SpellPriority);
        }
    }
}
