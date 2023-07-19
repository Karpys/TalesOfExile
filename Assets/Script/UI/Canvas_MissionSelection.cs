using System;
using System.Collections.Generic;
using System.Linq;
using KarpysDev.Script.Manager.Library;
using KarpysDev.Script.Map_Related.Quest;
using KarpysDev.Script.UI.Pointer;
using KarpysDev.Script.Utils;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace KarpysDev.Script.UI
{
    public class Canvas_MissionSelection : MonoBehaviour,IUIPointerController
    {
        [SerializeField] private Transform m_Container = null;
        [Header("Map Tier Banner")]
        [SerializeField] private Tier[] m_MapTiers = null;
        [SerializeField] private MapTierUIPointer m_MapTierPointerPrefab = null;
        [SerializeField] private Transform m_MapTierLayout = null;
        [Header("Quest Diffuculty Draw")]
        [SerializeField] private StaticWeightElementDraw<QuestDifficulty> m_MapDifficulty0 = null;
        [SerializeField] private StaticWeightElementDraw<QuestDifficulty> m_MapDifficulty1 = null;
        [SerializeField] private StaticWeightElementDraw<QuestDifficulty> m_MapDifficulty2 = null;
        [Header("Quest Displayer")]
        [SerializeField] private Transform m_QuestDisplayerContainer = null;
        [SerializeField] private QuestDisplayer m_EasyQuestDisplayerPrefab = null;
        [SerializeField] private QuestDisplayer m_QuestDisplayerPrefab = null;
        //Todo: Create Map Modifier Icon with SetIcon and percentage if is float percentage value
        //Todo:and  on pointer up show description
        //[SerializeField] private MapModifierIcon m_BonusMapModifierIcon = null;
        //[SerializeField] private MapModifierIcon m_MalusMapModifierIcon = null;
        [Header("References")]
        [SerializeField] private QuestLibrary m_QuestLibrary = null;

        private bool m_IsOpen = false;

        private MapTierUIPointer m_CurrentPointer = null;

        private Dictionary<Tier, Quest[]> m_ExistentQuest = new Dictionary<Tier, Quest[]>();
        private List<GameObject> m_ExistentQuestDisplayer = new List<GameObject>();

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
        private void Open()
        {
            m_Container.gameObject.SetActive(true);
        }

        private void Close()
        {
            m_Container.gameObject.SetActive(false);
        }

        private void DisplayCurrentTier(Tier tier)
        {
            if(m_ExistentQuest.Count > 0)
                ClearExistentDisplayer();    

            if (!m_ExistentQuest.TryGetValue(tier, out Quest[] quests))
            {
                quests = CreateQuest(tier);
            }
            
            DisplayQuest(quests);
            
            if(!m_ExistentQuest.ContainsKey(tier))
                m_ExistentQuest.Add(tier,quests);
        }

        private Quest[] CreateQuest(Tier tier)
        {
            QuestScriptable[] questsData = m_QuestLibrary.GetQuest(tier,3);
            QuestDifficulty[] mapDifficulties = GetDifficulties();

            Quest[] quests = new Quest[3];
            for (int i = 0; i < quests.Length; i++)
            {
                quests[i] = new Quest(questsData[i], mapDifficulties[i]);
            }

            return quests;
        }

        private void DisplayQuest(Quest[] quests)
        {
            foreach (Quest quest in quests)
            {
                QuestDisplayer displayer = null;
                if (quest.QuestDifficulty == QuestDifficulty.Easy)
                {
                    displayer = Instantiate(m_EasyQuestDisplayerPrefab, m_QuestDisplayerContainer);
                }
                else
                {
                    displayer = Instantiate(m_QuestDisplayerPrefab, m_QuestDisplayerContainer);
                }
                displayer.AssignQuest(quest);
                m_ExistentQuestDisplayer.Add(displayer.gameObject);
            }
        }

        private void ClearExistentDisplayer()
        {
            foreach (GameObject displayer in m_ExistentQuestDisplayer)
            {
                Destroy(displayer);
            }
            
            m_ExistentQuestDisplayer.Clear();
        }

        private QuestDifficulty[] GetDifficulties()
        {
            QuestDifficulty[] mapDifficulties = new QuestDifficulty[3];
            mapDifficulties[0] = m_MapDifficulty0.Draw();
            mapDifficulties[1] = m_MapDifficulty1.Draw();
            mapDifficulties[2] = m_MapDifficulty2.Draw();
            return mapDifficulties;
        }

        public void SetCurrentPointer(UIPointerController pointerController)
        {
            m_CurrentPointer = (MapTierUIPointer)pointerController;
        }
    }
}