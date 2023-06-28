using System.Collections.Generic;
using KarpysDev.Script.Items;
using KarpysDev.Script.Spell;
using KarpysDev.Script.UI;
using KarpysDev.Script.Utils;
using UnityEngine;
using UnityEngine.UIElements;

namespace KarpysDev.Script.Entities
{
    public class SpellDisplaySaver : MonoBehaviour,ISaver
    {
        [SerializeField] private PlayerBoardEntity m_Player = null;
        [SerializeField] private TextAsset m_BaseSave = null;
        [SerializeField] private string m_SaveName = string.Empty;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                WriteSaveData(m_SaveName,FetchSaveData());
            }
        }

        //Todo: Need to handle spell key collision//
        public TriggerSpellData[] LoadSpellDisplay()
        {
            string[] spellKeys = SaveUtils.ReadData(m_SaveName,m_BaseSave);

            List<TriggerSpellData> currentDisplaySave = new List<TriggerSpellData>(m_Player.Spells);
            TriggerSpellData[] triggerSpellDatas = new TriggerSpellData[SpellInterfaceController.SPELL_DISPLAY_COUNT];

            for (int i = 0; i < spellKeys.Length; i++)
            {
                if(spellKeys[i] == "none") continue;

                for (int y = 0; y < currentDisplaySave.Count; y++)
                {
                    TriggerSpellData spellData = currentDisplaySave[y];
                    if (spellData.TriggerData.SpellName == spellKeys[i])
                    {
                        triggerSpellDatas[i] = spellData;
                        currentDisplaySave.Remove(currentDisplaySave[y]);
                        break;
                    }
                }
            }

            return triggerSpellDatas;
        }
        public string[] FetchSaveData()
        {
            TriggerSpellData[] triggerSpellDatas = m_Player.DisplaySpell;
            string[] spellDisplaySave = new string[triggerSpellDatas.Length];

            for (int i = 0; i < triggerSpellDatas.Length; i++)
            {
                if (triggerSpellDatas[i] == null)
                {
                    spellDisplaySave[i] = "none";
                }
                else
                {
                    spellDisplaySave[i] = triggerSpellDatas[i].TriggerData.SpellName;
                }
            }
            return spellDisplaySave;
        }

        public void WriteSaveData(string saveName, string[] data)
        {
            SaveUtils.WriteSave(saveName,data);
        }
    }
}