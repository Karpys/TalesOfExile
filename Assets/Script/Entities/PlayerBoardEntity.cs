﻿using System;
using System.Collections.Generic;
using System.Linq;
using KarpysDev.Script.Entities.EntitiesBehaviour;
using KarpysDev.Script.Items;
using KarpysDev.Script.Manager;
using KarpysDev.Script.Spell;
using KarpysDev.Script.UI;
using TweenCustom;
using UnityEngine;

namespace KarpysDev.Script.Entities
{
    public class PlayerBoardEntity : BoardEntity,ISpellSet
    {
        [Header("Player")]
        [SerializeField] private PlayerInventory m_PlayerInventory = null;
        [SerializeField] private SpellDisplaySaver m_SpellDisplaySave = null;
        [SerializeField] private Transform m_JumpTweenContainer = null;
        [SerializeField] private float m_MovementDuration = 0.1f;

        public PlayerInventory PlayerInventory => m_PlayerInventory;

        private TriggerSpellData[] m_DisplaySpell = new TriggerSpellData[SpellInterfaceController.SPELL_DISPLAY_COUNT];

        public TriggerSpellData[] DisplaySpell => m_DisplaySpell;
        public Action A_OnSpellCollectionChanged = null;
        protected override void RegisterEntity()
        {
            base.RegisterEntity();
           
            GameManager.Instance.RegisterPlayer(this);
            GameManager.Instance.SetControlledEntity(this);
        }

        public override void EntityInitialization(EntityBehaviour entityIa, EntityGroup entityGroup,
            EntityGroup targetEntityGroup = EntityGroup.None)
        {
            base.EntityInitialization(entityIa, entityGroup, targetEntityGroup);
            m_PlayerInventory.Init();
            //Init Skill Tree
            InitDisplaySpell();
            
            if(this == GameManager.Instance.ControlledEntity)
                GameManager.Instance.RefreshTargetEntitySkills();
            
            ComputeAllSpells();
            UpdateSpellPriority();
        }

        public void InitDisplaySpell()
        {
            m_DisplaySpell = m_SpellDisplaySave.LoadSpellDisplay();
            //Todo: Use Save System//
            /*int maxDisplay = m_DisplaySpell.Length;
            int spellId = 0;

            for (int i = 0; i < Spells.Count; i++)
            {
                m_DisplaySpell[spellId] = Spells[i];
                spellId++;
                
                if(spellId == maxDisplay)
                    break;
            }*/
        }

        protected override List<TriggerSpellData> GetUsableSpells()
        {
            List<TriggerSpellData> triggerSpellDatas = m_DisplaySpell.Where(s => s != null).ToList();
            return triggerSpellDatas;
        }

        public override void ComputeAllSpells()
        {
            foreach (TriggerSpellData triggerSpellData in UsableSpells)
            {
                triggerSpellData.SpellTrigger.ComputeSpellData(this);
            }

            m_EntityEvent.OnSpellRecompute?.Invoke();
        }

        public override void EntityAction()
        {
            m_EntityBehaviour.Behave();
        }

        public override void TriggerDeath()
        {
            //Trigger Lose ?//
            return;
        }

        public override TriggerSpellData AddSpellToSpellList(SpellInfo spell)
        {
            TriggerSpellData spellAdded = base.AddSpellToSpellList(spell);
            AddInDisplay(spellAdded);
            A_OnSpellCollectionChanged?.Invoke();
            GameManager.Instance.RefreshTargetEntitySkills();
            return spellAdded;
        }

        public override void RemoveSpellToSpellList(TriggerSpellData spell)
        {
            base.RemoveSpellToSpellList(spell);
            PopFromDisplay(spell);
            A_OnSpellCollectionChanged?.Invoke();
            GameManager.Instance.RefreshTargetEntitySkills();

        }
        
        private void AddInDisplay(TriggerSpellData spellAdded)
        {
            if(m_DisplaySpell.Contains(spellAdded)) return;
            
            for (int i = 0; i < m_DisplaySpell.Length; i++)
            {
                if (m_DisplaySpell[i] == null)
                {
                    m_DisplaySpell[i] = spellAdded;
                    return;
                }
            }
        }

        private void PopFromDisplay(TriggerSpellData spell)
        {
            for (int i = 0; i < m_DisplaySpell.Length; i++)
            {
                TriggerSpellData spellData = m_DisplaySpell[i];
                if (spellData == spell)
                {
                    m_DisplaySpell[i] = null;
                }
            }
        }

        #region Movement

        

        protected override void Movement()
        {
            JumpAnimation();
        }

        

        void JumpAnimation()
        {
            transform.DoKill();
            transform.DoMove( m_TargetMap.GetTilePosition(m_XPosition, m_YPosition),m_MovementDuration);
            JumpTween();
        }

        private void JumpTween()
        {
            m_JumpTweenContainer.transform.DoLocalMove(new Vector3(0, 0.2f, 0), m_MovementDuration / 2f).OnComplete(ReleaseJumpTween);
        }

        private void ReleaseJumpTween()
        {
            m_JumpTweenContainer.transform.DoLocalMove(new Vector3(0, 0, 0), m_MovementDuration / 2f);
        }

        #endregion
        
        public override TriggerSpellData[] GetDisplaySpells()
        {
            return m_DisplaySpell;
        }

        public void InsertSpell(TriggerSpellData spellData,int id)
        {
            TriggerSpellData inPlaceSpell = m_DisplaySpell[id];

            if (spellData != null)
            {
                for (int i = 0; i < m_DisplaySpell.Length; i++)
                {
                    if (m_DisplaySpell[i] == spellData)
                    {
                        m_DisplaySpell[i] = inPlaceSpell;
                        break;
                    }
                }
            }

            m_DisplaySpell[id] = spellData;
            spellData?.SpellTrigger.ComputeSpellData(this);
            UpdateSpellPriority();
            
        }
    }

    public interface ISpellSet
    {
        void InitDisplaySpell();
        void InsertSpell(TriggerSpellData spellData,int id);
        TriggerSpellData[] GetDisplaySpells();
    }
}