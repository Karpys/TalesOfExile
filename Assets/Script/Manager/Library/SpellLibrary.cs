using System.Collections.Generic;
using KarpysDev.Script.Entities;
using KarpysDev.Script.Spell;
using UnityEngine;

namespace KarpysDev.Script.Manager.Library
{
    using KarpysUtils;

    public class SpellLibrary : SingletonMonoBehavior<SpellLibrary>
    {
        [SerializeField] private List<SpellDataScriptable> m_SpellList = new List<SpellDataScriptable>();
        public Dictionary<string, SpellDataScriptable> Spells = new Dictionary<string, SpellDataScriptable>();

        private void Awake()
        {
            foreach (SpellDataScriptable spellData in m_SpellList)
            {
                Spells.Add(spellData.SpellName,spellData);
            }
        }

        public SpellInfo GetSpellViaKey(string spellKey, SpellLearnType spellLearnType, int spellLevel = 1)
        {
            Spells.TryGetValue(spellKey, out SpellDataScriptable spellData);

            if (spellData == null)
            {
                Debug.LogError("Spell Key not found : " + spellKey);
                return null;
            }

            return new SpellInfo(spellData, 1, spellLevel, spellLearnType);
        }
    }
}
