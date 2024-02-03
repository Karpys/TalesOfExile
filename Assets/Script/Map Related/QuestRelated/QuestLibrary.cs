using System;
using System.Collections.Generic;
using KarpysDev.Script.Manager.Library;
using KarpysDev.Script.Utils;
using UnityEngine;

namespace KarpysDev.Script.Map_Related.QuestRelated
{
    using KarpysUtils;

    public class QuestLibrary : SingletonMonoBehavior<QuestLibrary>
    {
        [SerializeField] private Widget.GenericLibrary<Sprite, QuestModifierType> m_MapModifierIconLibrary = null;
        [SerializeField] private Widget.GenericLibrary<float, QuestDifficulty> m_MapDifficultyLibrary = null;
        [SerializeField] private Widget.GenericLibrary<Vector2Int, QuestDifficulty> m_MapModifierDrawerLibrary = null;
        [SerializeField] private Widget.GenericLibrary<Color, QuestDifficulty> m_MapDifficultyColor = null;
        [Header("Tier Quest")]
        [SerializeField] private List<MultipleWeightElementDraw<QuestScriptable>> m_TierQuest = null;

        [Header("Default Value")] 
        [SerializeField] private Sprite m_DefaultIcon = null;
        
        private Dictionary<Tier, MultipleWeightElementDraw<QuestScriptable>> m_TierQuestCollection = null;

        public void Awake()
        {
            m_MapModifierIconLibrary.InitializeDictionary();
            m_MapDifficultyLibrary.InitializeDictionary();
            m_MapModifierDrawerLibrary.InitializeDictionary();
            m_MapDifficultyColor.InitializeDictionary();
            m_TierQuestCollection = new Dictionary<Tier, MultipleWeightElementDraw<QuestScriptable>>();

            for (int i = 0; i < m_TierQuest.Count; i++)
            {
                m_TierQuestCollection.Add((Tier)i,m_TierQuest[i]);
            }
        }

        public QuestScriptable[] GetQuest(Tier tier,int questCount)
        {
            if (m_TierQuestCollection.TryGetValue(tier, out MultipleWeightElementDraw<QuestScriptable> tierQuest))
            {
                return tierQuest.MultipleDraw(questCount).ToArray();
            }

            Debug.LogError("Target Quest has not been found : " + tier);
            return null;
        }

        public float GetDifficultyPercennt(QuestDifficulty difficulty)
        {
            return m_MapDifficultyLibrary.GetViaKey(difficulty);
        }

        public Vector2Int GetModifierCount(QuestDifficulty difficulty)
        {
            return m_MapModifierDrawerLibrary.GetViaKey(difficulty);
        }
        public Sprite GetIcon(QuestModifierType type)
        {
            Sprite icon = m_MapModifierIconLibrary.GetViaKey(type);
            if (icon)
                return icon;
            return m_DefaultIcon;
        }

        public Color GetDifficultyColor(QuestDifficulty questDifficulty)
        {
            return m_MapDifficultyColor.GetViaKey(questDifficulty);
        }
    }
}