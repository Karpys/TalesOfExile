using System;
using System.Collections.Generic;
using KarpysDev.Script.Manager.Library;
using KarpysDev.Script.Utils;
using UnityEngine;

namespace KarpysDev.Script.Map_Related.Quest
{
    public class QuestLibrary : MonoBehaviour
    {
        [SerializeField] private MultipleWeightElementDraw<QuestScriptable> m_Tier0Quest = null;

        private Dictionary<Tier, MultipleWeightElementDraw<QuestScriptable>> m_TierQuestCollection = null;

        public void Awake()
        {
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
    }
}