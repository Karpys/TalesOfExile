using System;
using System.Collections.Generic;
using KarpysDev.Script.Items;
using KarpysDev.Script.Manager.Library;
using KarpysDev.Script.Spell;
using KarpysDev.Script.Utils;
using Script.Data;
using UnityEngine;

namespace KarpysDev.Script.Entities
{
    public class SpellLearnedSaver : MonoBehaviour,ISaver
    {
        [SerializeField] private BoardEntity m_BoardEntity = null;
        [SerializeField] private string m_SaveName = String.Empty;
        public string GetSaveName => m_SaveName;

        private void Awake()
        {
            GlobalSaver.AddSaver(this);
        }

        public string[] FetchSaveData()
        {
            List<TriggerSpellData> spells = m_BoardEntity.Spells;

            List<string> spellSave = new List<string>();

            for (int i = 0; i < spells.Count; i++)
            {
                if(spells[i].SpellLearnType == SpellLearnType.Learned)
                    spellSave.Add(spells[i].TriggerData.SpellName);
            }

            return spellSave.ToArray();
        }

        public void WriteSaveData(string saveName, string[] data)
        {
            SaveUtils.WriteSave(saveName,data);
        }

        public bool SaveExist()
        {
            return SaveUtils.SaveExist(m_SaveName);
        }

        public SpellInfo[] LoadSpellLearned()
        {
            string[] spellLearned = SaveUtils.ReadData(GetSaveName,null);
            SpellInfo[] infos = new SpellInfo[spellLearned.Length];

            for (int i = 0; i < spellLearned.Length; i++)
            {
                infos[i] = SpellLibrary.Instance.GetSpellViaKey(spellLearned[i], SpellLearnType.Learned);
            }

            return infos;
        }
    }
}