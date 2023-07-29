using System.Collections.Generic;
using KarpysDev.Script.Manager.Library;
using KarpysDev.Script.Map_Related.MapGeneration;
using KarpysDev.Script.Map_Related.QuestRelated;
using KarpysDev.Script.UI.Pointer;
using KarpysDev.Script.Utils;
using UnityEngine;

namespace KarpysDev.Script.UI
{
    public class MissionSelectionManager : SingletonMonoBehavior<MissionSelectionManager>
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
        [Header("References")]
        [SerializeField] private QuestLibrary m_QuestLibrary = null;
        [SerializeField] private Vector2Int m_SpawnPortalOffset = Vector2Int.zero;

        private bool m_IsOpen = false;
        private Vector2Int m_OpenPosition = Vector2Int.zero;

        private MapTierUIPointer m_CurrentPointer = null;
        private Tier m_LastTier = Tier.Tier0;

        private Dictionary<Tier, Quest[]> m_ExistentQuest = new Dictionary<Tier, Quest[]>();
        private List<GameObject> m_ExistentQuestDisplayer = new List<GameObject>();
        private MissionLauncherPortalMap m_ExistentPortal = null;
        private Quest m_CurrentQuest = null;

        public bool IsOpen => m_IsOpen;
        public Vector2Int GetSpawnPosition => m_OpenPosition + m_SpawnPortalOffset;
        private void Awake()
        {
            foreach (Tier tier in m_MapTiers)
            {
                MapTierUIPointer pointer = Instantiate(m_MapTierPointerPrefab, m_MapTierLayout);
                pointer.SetTier(tier);
            }    
        }

        public void Open(Vector2Int openPosition)
        {
            m_OpenPosition = openPosition;
            m_IsOpen = true;
            m_Container.gameObject.SetActive(true);
            
            if(m_LastTier != Tier.None)
                DisplayCurrentTier(m_LastTier);
        }

        public void Close()
        {
            m_IsOpen = false;
            m_Container.gameObject.SetActive(false);
        }

        public void DisplayCurrentTier(Tier tier)
        {
            m_LastTier = tier;
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

        public void SetPortal(MissionLauncherPortalMap portal)
        {
            m_ExistentPortal = portal;
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

        public void SetQuest(Quest quest)
        {
            m_CurrentQuest = quest;
        }

        public void TriggerQuestEnd()
        {
            Invoke("PopLoot",1f);
        }

        private void PopLoot()
        {
            m_CurrentQuest.PopLoot();
        }

        public void CloseAll()
        {
            Close();
            ClearExistentDisplayer();
            m_ExistentQuest.Clear();
        }

        public void ClearExistentPortal()
        {
            if(m_ExistentPortal)
                Destroy(m_ExistentPortal.gameObject);
        }
    }
}