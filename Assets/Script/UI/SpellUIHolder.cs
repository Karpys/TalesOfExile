using KarpysDev.Script.Spell;
using KarpysDev.Script.UI.Pointer;
using UnityEngine;
using UnityEngine.UI;

namespace KarpysDev.Script.UI
{
    public class SpellUIHolder:UIPointer
    {
        [SerializeField] private Image m_HolderVisual = null;

        private TriggerSpellData m_CurrentSpellData = null;
        

        public TriggerSpellData TriggerSpellData => m_CurrentSpellData;
        public void Initialize(TriggerSpellData triggerSpellData)
        {
            m_CurrentSpellData = triggerSpellData;

            if (triggerSpellData != null)
            {
                m_HolderVisual.sprite = m_CurrentSpellData.TriggerData.SpellIcon;
            }
        }

        protected override void OnEnter() {}

        protected override void OnExit(){ }
    }
}