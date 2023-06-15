using System;
using System.Collections.Generic;
using System.Linq;
using KarpysDev.Script.Entities;
using KarpysDev.Script.Spell;
using UnityEngine;

namespace KarpysDev.Script.UI
{
    public class CanvasSkillLearned : MonoBehaviour
    {
        [SerializeField] private Transform m_Container = null;
        [SerializeField] private Transform m_LearnedHolder = null;
        [SerializeField] private Transform m_EquipementSpellHolder = null;
        [SerializeField] private SpellUIHolder m_SpellUIHolderPrefab = null;

        [SerializeField] private Transform[] m_DisableIfNoEquipementSpells = null;
        private PlayerBoardEntity m_Player = null;

        public void Initialize(PlayerBoardEntity player)
        {
            m_Player = player;
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                if(!m_Player)return;
                
                Enable();
            }
        }

        private void Enable()
        {
            if (m_Container.gameObject.activeSelf)
            {
                m_Container.gameObject.SetActive(false);
                Close();
            }
            else
            {
                m_Container.gameObject.SetActive(true);
                Open();
            }
        }

        private void Open()
        {
            UpdateDisplay();
        }

        private void Close()
        {
            Clear();
        }

        private void Clear()
        {
            int childCount = m_LearnedHolder.childCount;

            for (int i = 0; i < childCount; i++)
            {
                Destroy(m_LearnedHolder.GetChild(i).gameObject);
            }
            
            childCount = m_EquipementSpellHolder.childCount;

            for (int i = 0; i < childCount; i++)
            {
                Destroy(m_EquipementSpellHolder.GetChild(i).gameObject);
            }
            
        }

        private void UpdateDisplay()
        {
            List<TriggerSpellData> equipementSpellDatas =
                m_Player.Spells.Where(s => s.SpellLearnType == SpellLearnType.Equipement).ToList();
            
            List<TriggerSpellData> learnSpellDatas =
                m_Player.Spells.Where(s => s.SpellLearnType == SpellLearnType.Learned).ToList();
            
            foreach (TriggerSpellData spellData in learnSpellDatas)
            {
                SpellUIHolder holder = Instantiate(m_SpellUIHolderPrefab,m_LearnedHolder);
                holder.Initialize(spellData);
            }

            bool enableState = equipementSpellDatas.Count > 0;
            
            foreach (Transform t in m_DisableIfNoEquipementSpells)
            {
                t.gameObject.SetActive(enableState);
            }
            
            if(!enableState) return;
            
            foreach (TriggerSpellData spellData in equipementSpellDatas)
            {
                SpellUIHolder holder = Instantiate(m_SpellUIHolderPrefab,m_EquipementSpellHolder);
                holder.Initialize(spellData);
            }
        }
    }
}