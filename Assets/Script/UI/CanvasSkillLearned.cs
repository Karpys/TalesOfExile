using System;
using KarpysDev.Script.Entities;
using KarpysDev.Script.Spell;
using UnityEngine;

namespace KarpysDev.Script.UI
{
    public class CanvasSkillLearned : MonoBehaviour
    {
        [SerializeField] private Transform m_Container = null;
        [SerializeField] private Transform m_LayoutHolder = null;
        [SerializeField] private SpellUIHolder m_SpellUIHolderPrefab = null;

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
            int childCount = m_LayoutHolder.childCount;

            for (int i = 0; i < childCount; i++)
            {
                Destroy(m_LayoutHolder.GetChild(i).gameObject);
            }
            
            Debug.Log("Child count" + m_LayoutHolder.childCount);
        }

        private void UpdateDisplay()
        {
            foreach (TriggerSpellData spellData in m_Player.Spells)
            {
                SpellUIHolder holder = Instantiate(m_SpellUIHolderPrefab,m_LayoutHolder);
                holder.Initialize(spellData);
            }
        }
    }
}