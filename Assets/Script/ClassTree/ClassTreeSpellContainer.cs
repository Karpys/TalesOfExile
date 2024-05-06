using KarpysDev.Script.Entities;
using KarpysDev.Script.Manager;
using KarpysDev.Script.Spell;
using KarpysDev.Script.UI.Pointer;
using TMPro;
using UnityEngine;

namespace KarpysDev.Script.ClassTree
{
    public class ClassTreeSpellContainer : UIPointer
    {
        [SerializeField] private SpellInfo m_SpellInfo = null;
        [SerializeField] private TMP_Text m_SpellLevel = null;

        private ClassTreeController m_Controller = null; 
        private TriggerSpellData m_TriggerSpellData = null;
        private bool m_SpellLearned = false;

        public void Initialize(ClassTreeController controller)
        {
            m_Controller = controller;
            m_TriggerSpellData = m_Controller.Player.RegisterSpell(m_SpellInfo);
            UpdateText();
        }

        protected override void OnEnter()
        {
            GlobalCanvas.Instance.GetSpellUIDisplayer().DisplaySpell(m_TriggerSpellData,transform,false);
        }

        protected override void OnExit()
        {
            GlobalCanvas.Instance.GetSpellUIDisplayer().HideSpell();
        }

        public void OnLevelUp()
        {
            if(m_TriggerSpellData.Level == m_TriggerSpellData.LevelMax)
                return;
            
            if (!m_SpellLearned)
            {
                m_SpellLearned = true;
                m_TriggerSpellData = m_Controller.Player.AddSpellToSpellList(m_SpellInfo);
            }
            else
            {
                m_TriggerSpellData.ChangeLevel(1);
            }
            
            UpdateText();
        }

        public void OnLevelDown()
        {
            if(!m_SpellLearned)
                return;

            if (m_TriggerSpellData.Level == 0)
            {
                m_Controller.Player.RemoveSpellToSpellList(m_TriggerSpellData);
                m_TriggerSpellData = m_Controller.Player.RegisterSpell(m_SpellInfo);
                m_SpellLearned = false;
            }
            else
            {
                m_TriggerSpellData.ChangeLevel(-1);
            }
            
            UpdateText();
        }
        
        private void UpdateText()
        {
            string currentSpellLevelShown = m_SpellLearned ? m_TriggerSpellData.SpellLevelShown.ToString() : "0";
            m_SpellLevel.text = currentSpellLevelShown + " / " + m_TriggerSpellData.MaxSpellLevelShown;
        }
    }
}