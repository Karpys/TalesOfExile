﻿using KarpysDev.Script.Entities;
using KarpysDev.Script.Manager;
using UnityEngine;

namespace KarpysDev.Script.Spell
{
    public class SpellBuyable:IUIBuyable
    {
        private TriggerSpellData m_TriggerSpellData = null;
        public float Price { get; set; }
        public Transform UIParent { get; set; }

        public Sprite GetIcon()
        {
            return m_TriggerSpellData.TriggerData.m_SpellIcon;
        }

        public void OnBuy()
        {
            Debug.Log("Youpiii");
        }
        
        public void DisplayBuyable()
        {
            m_TriggerSpellData.SpellTrigger.ComputeSpellData(GameManager.Instance.PlayerEntity);
            GlobalCanvas.Instance.GetSpellUIDisplayer().DisplaySpell(m_TriggerSpellData,UIParent);
        }

        public void HideBuyable()
        {
            GlobalCanvas.Instance.GetSpellUIDisplayer().HideSpell();
        }

        public void InitializeSpell(SpellInfo spellInfo)
        {
            m_TriggerSpellData = GameManager.Instance.PlayerEntity.RegisterSpell(spellInfo);
        }
    }
}