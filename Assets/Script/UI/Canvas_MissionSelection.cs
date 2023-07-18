using System;
using KarpysDev.Script.Manager.Library;
using KarpysDev.Script.Map_Related.Quest;
using KarpysDev.Script.UI.Pointer;
using UnityEngine;

namespace KarpysDev.Script.UI
{
    public class Canvas_MissionSelection : MonoBehaviour,IUIPointerController
    {
        [SerializeField] private Transform m_Container = null;
        [Header("Map Tier Banner")]
        [SerializeField] private Tier[] m_MapTiers = null;
        [SerializeField] private MapTierUIPointer m_MapTierPointerPrefab = null;
        [SerializeField] private Transform m_MapTierLayout = null;
        [Header("Quest Displayer")]
        [SerializeField] private Transform m_QuestDisplayerContainer = null;
        [Header("References")]
        [SerializeField] private QuestLibrary m_QuestLibrary = null;

        private bool m_IsOpen = false;

        private MapTierUIPointer m_CurrentPointer = null;

        private void Awake()
        {
            foreach (Tier tier in m_MapTiers)
            {
                MapTierUIPointer pointer = Instantiate(m_MapTierPointerPrefab, m_MapTierLayout);
                pointer.SetTier(tier);
                pointer.AssignController(this);
            }    
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (m_IsOpen)
                {
                    Close();
                }
                else
                {
                    Open();
                }

                m_IsOpen = !m_IsOpen;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (m_CurrentPointer && m_CurrentPointer.PointerUp)
                {
                    DisplayCurrentTier(m_CurrentPointer.Tier);
                }
            }
        }

        private void DisplayCurrentTier(Tier tier)
        {
            QuestScriptable[] quests = m_QuestLibrary.GetQuest(tier,3);
        }

        private void Open()
        {
            m_Container.gameObject.SetActive(true);
        }

        private void Close()
        {
            m_Container.gameObject.SetActive(false);
        }

        public void SetCurrentPointer(UIPointerController pointerController)
        {
            m_CurrentPointer = (MapTierUIPointer)pointerController;
        }
    }
}