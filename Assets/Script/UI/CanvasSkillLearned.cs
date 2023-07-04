using System;
using System.Collections.Generic;
using System.Linq;
using KarpysDev.Script.Entities;
using KarpysDev.Script.Spell;
using KarpysDev.Script.Widget;
using UnityEngine;
using UnityEngine.UI;

namespace KarpysDev.Script.UI
{
    public class CanvasSkillLearned : MonoBehaviour
    {
        [SerializeField] private SpellInterfaceController m_SpellInterfaceController = null;
        [SerializeField] private Canvas_Skills m_CanvasSkill = null;
        [SerializeField] private Transform m_Container = null;
        [SerializeField] private Transform m_LearnedHolder = null;
        [SerializeField] private Transform m_EquipementSpellHolder = null;
        [SerializeField] private SpellLearnedUIHolder m_SpellUIHolderPrefab = null;
        [SerializeField] private Transform[] m_DisableIfNoEquipementSpells = null;
        [SerializeField] private UISelectionFade m_SpellItemFade = null;

        [Header("Spell Description Display")] 
        [SerializeField] private SpellUIDisplayer m_SpellDisplayer = null;
        
        private PlayerBoardEntity m_Player = null;

        private SpellUIHolder m_CurrentHolder = null;
        private TriggerSpellData m_SelectedSpell = null;
        private bool m_IsActive = false;


        private Clock m_DisplaySpellClock = null; 
        public void Initialize(PlayerBoardEntity player)
        {
            m_Player = player;
            m_Player.A_OnSpellCollectionChanged += OnSpellCollectionChanged;
        }

        public void SetCurrentHolder(SpellUIHolder holder)
        {
            m_CurrentHolder = holder;
            m_DisplaySpellClock = new Clock(0.05f, TryDisplaySpell);
        }

        private void TryDisplaySpell()
        {
            if (m_CurrentHolder.PointerUp)
            {
                DisplaySpell();
            }
        }

        private void DisplaySpell()
        {
            m_CurrentHolder.TriggerSpellData.SpellTrigger.ComputeSpellData(m_Player);
            m_SpellDisplayer.DisplaySpell(m_CurrentHolder.TriggerSpellData,m_CurrentHolder.transform);
        }

        public void HideDisplaySpell()
        {
            m_SpellDisplayer.HideSpell();
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                if(!m_Player)return;
                
                Enable();
            }

            if (Input.GetMouseButtonDown(0))
            {
                if(!m_IsActive)
                    return;

                if (m_CurrentHolder && m_CurrentHolder.PointerUp)
                {
                    m_SpellItemFade.Initialize(m_CurrentHolder.TriggerSpellData.TriggerData.m_SpellIcon);
                    m_SelectedSpell = m_CurrentHolder.TriggerSpellData;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if(!m_IsActive)
                    return;

                if (m_SelectedSpell == null)
                {
                    return;
                }
                

                if (m_SpellInterfaceController.CurrentPointer.PointerUp)
                {
                    TryInsertSpell(m_SelectedSpell,m_SpellInterfaceController.CurrentPointer.SpellId);
                }
                
                m_SpellItemFade.Clear();
                m_SelectedSpell = null;
            }

            m_DisplaySpellClock?.UpdateClock();
        }

        private void TryInsertSpell(TriggerSpellData triggerSpellData,int id)
        {
            m_Player.InsertSpell(triggerSpellData,id);
            m_CanvasSkill.RefreshTargetSkills(m_Player);
        }

        private void Enable()
        {
            if (m_IsActive)
            {
                m_Container.gameObject.SetActive(false);
                Close();
            }
            else
            {
                m_Container.gameObject.SetActive(true);
                Open();
            }

            m_IsActive = !m_IsActive;
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
        
        private void OnSpellCollectionChanged()
        {
            if (m_IsActive)
            {
                Clear();
                UpdateDisplay();
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
                SpellLearnedUIHolder holder = Instantiate(m_SpellUIHolderPrefab,m_LearnedHolder);
                holder.Initialize(spellData);
                holder.SetController(this);
            }

            bool enableState = equipementSpellDatas.Count > 0;
            
            foreach (Transform t in m_DisableIfNoEquipementSpells)
            {
                t.gameObject.SetActive(enableState);
            }
            
            if(!enableState) return;
            
            foreach (TriggerSpellData spellData in equipementSpellDatas)
            {
                SpellLearnedUIHolder holder = Instantiate(m_SpellUIHolderPrefab,m_EquipementSpellHolder);
                holder.Initialize(spellData);
                holder.SetController(this);
            }
        }
    }
}