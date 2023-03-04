using System;
using System.Collections.Generic;
using UnityEngine;

public class SpellLibrary : SingletonMonoBehavior<SpellLibrary>
{
    [SerializeField] private List<SpellData> m_SpellList = new List<SpellData>();

    public Dictionary<string, SpellData> Spells = new Dictionary<string, SpellData>();

    private void Awake()
    {
        foreach (SpellData spellData in m_SpellList)
        {
            Spells.Add(spellData.Data.SpellKey,spellData);
        }
    }

    public SpellData GetSpellViaKey(string spellKey)
    {
        SpellData spellData = null;
        Spells.TryGetValue(spellKey,out spellData);

        if (spellData == null)
        {
            Debug.LogError("Spell Key not found");
            return null;
        }
        
        return new SpellData(spellData);
    }
}
