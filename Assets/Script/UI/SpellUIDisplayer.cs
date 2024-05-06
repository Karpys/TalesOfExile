using KarpysDev.Script.Manager;
using KarpysDev.Script.Spell;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KarpysDev.Script.UI
{
    using Spell.SpellConfig;

    public class SpellUIDisplayer : MonoBehaviour
    {
        [SerializeField] private RectTransform m_Container = null;
        [SerializeField] private RectTransform m_DisplayContainer = null;
        [Header("All except vertical layout")]
        [SerializeField] private float m_BaseHeight = 0;
        [SerializeField] private RectTransform m_LayoutTransform = null;

        [Header("References")] 
        [SerializeField] private Image m_SpellIcon = null;
        [SerializeField] private TMP_Text m_SpellName = null;
        [SerializeField] private TMP_Text m_SpellGroups = null;
        [SerializeField] private TMP_Text m_SpellDescription = null;
        [SerializeField] private TMP_Text m_CooldownValue = null;

        [Header("Config")] [SerializeField]
        private ConfigMonitor m_ConfigMonitor = null;

        private TriggerSpellData m_CurrentTriggerSpellData = null;
        private TriggerSpellData m_LastDisplaySpellData = null;
        private bool m_InDisplay = false;
        private bool m_CanDisplayConfig = false;
        
        private const string NO_COOLDOWN_VALUE = "X";

        #if UNITY_EDITOR
        private void OnValidate()
        {
            AdaptSize();
        }
        #endif

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                if (m_ConfigMonitor.IsInDisplay && m_LastDisplaySpellData == m_CurrentTriggerSpellData)
                {
                    m_ConfigMonitor.Hide();
                }else if (m_InDisplay && m_CurrentTriggerSpellData is {SpellTrigger: IConfig {CanDisplayConfig: true} config} && m_CanDisplayConfig)
                {
                    m_ConfigMonitor.Display(config);
                    m_LastDisplaySpellData = m_CurrentTriggerSpellData;
                }
            }
        }

        private void AdaptSize()
        {
            m_Container.sizeDelta = new Vector2(m_Container.sizeDelta.x, m_BaseHeight + m_LayoutTransform.sizeDelta.y);
        }

        public void DisplaySpell(TriggerSpellData spellData,Transform targetTransform,bool canDisplayConfig)
        {
            m_InDisplay = true;
            m_CanDisplayConfig = canDisplayConfig;
            transform.position = targetTransform.position;
            m_DisplayContainer.gameObject.SetActive(true);
            m_SpellIcon.sprite = spellData.TriggerData.SpellIcon;
            m_SpellName.text = spellData.TriggerData.SpellName;
            m_SpellGroups.text = GetSpellGroups(spellData.Data.SpellGroups);
            m_SpellDescription.text = spellData.GetSpellDescription();
            m_CooldownValue.text = spellData.EffectiveCooldown <= 0 ? NO_COOLDOWN_VALUE : spellData.EffectiveCooldown.ToString();
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(m_LayoutTransform);
            AdaptSize();
            GlobalCanvas.Instance.ClampX((RectTransform)transform);

            m_CurrentTriggerSpellData = spellData;
        }

        public void HideSpell()
        {
            m_InDisplay = false;
            m_CurrentTriggerSpellData = null;
            m_LastDisplaySpellData = null;
            m_DisplayContainer.gameObject.SetActive(false);
        }

        private string GetSpellGroups(SpellGroup[] groups)
        {
            string groupDescription = string.Empty;

            for (int i = 0; i < groups.Length; i++)
            {
                if (i == groups.Length - 1)
                {
                    groupDescription += groups[i].ToDescription();
                }
                else
                {
                    groupDescription += groups[i].ToDescription() + " / ";
                }
            }

            return groupDescription;
        }
    }
}