using System;
using System.Collections.Generic;
using KarpysDev.Script.Manager.Library;
using KarpysDev.Script.Utils;
using KarpysDev.Script.Widget;
using UnityEngine;

namespace KarpysDev.Script.Map_Related.Quest
{
    public class QuestLibrary : MonoBehaviour
    {
        [SerializeField] private GenericLibrary<Sprite, MapModifierType> m_MapModifierIconLibrary = null;
        [SerializeField] private GenericLibrary<float, MapDifficulty> m_MapDifficultyLibrary = null;
        [SerializeField] private GenericLibrary<Vector2Int, MapDifficulty> m_MapModifierDrawerLibrary = null;
        [SerializeField] private MultipleWeightElementDraw<QuestScriptable> m_Tier0Quest = null;

        private Dictionary<Tier, MultipleWeightElementDraw<QuestScriptable>> m_TierQuestCollection = null;

        public void Awake()
        {
            m_MapModifierIconLibrary.InitializeDictionary();
            m_MapDifficultyLibrary.InitializeDictionary();
            m_MapModifierDrawerLibrary.InitializeDictionary();
            m_TierQuestCollection = new Dictionary<Tier, MultipleWeightElementDraw<QuestScriptable>>();
            m_TierQuestCollection.Add(Tier.Tier0,m_Tier0Quest);
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

        public float GetDifficultyPercennt(MapDifficulty difficulty)
        {
            return m_MapDifficultyLibrary.GetViaKey(difficulty);
        }

        public Vector2Int GetModifierCount(MapDifficulty difficulty)
        {
            return m_MapModifierDrawerLibrary.GetViaKey(difficulty);
        }
        public Sprite GetIcon(MapModifierType type)
        {
            return m_MapModifierIconLibrary.GetViaKey(type);
        }
    }
}