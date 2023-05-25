using KarpysDev.Script.Spell;
using KarpysDev.Script.UI.Pointer;
using UnityEngine;
using UnityEngine.UI;

namespace KarpysDev.Script.UI
{
    public class SpellSelectionUIHolder:UIPointer
    {
        [SerializeField] private Image m_HolderVisual = null;

        private TriggerSpellData m_CurrentSpellData = null;
        private SpellSelectionUI m_SelectionController = null;

        public TriggerSpellData TriggerSpellData => m_CurrentSpellData;
        public void Initialize(TriggerSpellData triggerSpellData,SpellSelectionUI selectionController)
        {
            m_SelectionController = selectionController;
            m_CurrentSpellData = triggerSpellData;

            if (triggerSpellData != null)
            {
                m_HolderVisual.sprite = m_CurrentSpellData.TriggerData.m_SpellIcon;
            }
        }

        protected override void OnEnter()
        {
            m_SelectionController.SetCurrentPointer(this);
        }

        protected override void OnExit(){ }
    }
}