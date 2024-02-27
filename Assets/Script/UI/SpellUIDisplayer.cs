using System;
using System.Collections;
using System.Collections.Generic;
using KarpysDev.Script.Manager;
using KarpysDev.Script.Spell;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KarpysDev.Script.UI
{
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

        private const string NO_COOLDOWN_VALUE = "X";
        
        private void AdaptSize()
        {
            m_Container.sizeDelta = new Vector2(m_Container.sizeDelta.x, m_BaseHeight + m_LayoutTransform.sizeDelta.y);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                AdaptSize();
            }
        }

        public void DisplaySpell(TriggerSpellData spellData,Transform targetTransform)
        {
            transform.position = targetTransform.position;
            m_DisplayContainer.gameObject.SetActive(true);
            m_SpellIcon.sprite = spellData.TriggerData.SpellIcon;
            m_SpellName.text = spellData.TriggerData.SpellName;
            m_SpellGroups.text = GetSpellGroups(spellData.Data.SpellGroups);
            m_SpellDescription.text = spellData.GetSpellDescription();
            m_CooldownValue.text = spellData.TriggerData.BaseCooldown <= 0 ? NO_COOLDOWN_VALUE : spellData.TriggerData.BaseCooldown.ToString();
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(m_LayoutTransform);
            AdaptSize();
            GlobalCanvas.Instance.ClampX((RectTransform)transform);
        }

        public void HideSpell()
        {
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