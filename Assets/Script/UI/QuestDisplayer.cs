using KarpysDev.Script.Map_Related.QuestRelated;
using TMPro;
using UnityEngine;

namespace KarpysDev.Script.UI
{
    public class QuestDisplayer : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_QuestName = null;
        [SerializeField] private TMP_Text m_QuestDifficulty = null;
        [SerializeField] private TMP_Text m_GoldAmount= null;
        [SerializeField] private TMP_Text m_ExpAmount = null;
        
        [Header("Header Map Modifier Part")]
        [SerializeField] private Transform m_BonusMapModifierTransform = null;
        [SerializeField] private Transform m_MalusMapModifierTransform = null;
        [SerializeField] private QuestModifierUIHolder m_BonusMapModifierPrefab = null;
        [SerializeField] private QuestModifierUIHolder m_MalusMapModifierPrefab = null;
        
        private Quest m_Quest = null;
        public Quest Quest => m_Quest;
        public void AssignQuest(Quest quest)
        {
            m_Quest = quest;
            m_QuestDifficulty.color = QuestLibrary.Instance.GetDifficultyColor(m_Quest.QuestDifficulty);
            m_QuestName.text = m_Quest.QuestName;
            m_QuestDifficulty.text = m_Quest.QuestDifficulty+"\n"+m_Quest.QuestDifficultyPercent+"%";

            m_GoldAmount.text = m_Quest.QuestGoldAmount.ToString("0");
            m_ExpAmount.text = m_Quest.QuestExpAmount.ToString("0");
            
            //Modifier Part//
            int i = 0;
            for (; i < quest.BonusModifier.Length; i++)
            {
                QuestModifierUIHolder questModifierUI = Instantiate(m_BonusMapModifierPrefab, m_BonusMapModifierTransform);
                questModifierUI.Init(quest.BonusModifier[i]);
            }

            for (i = 0; i < quest.MalusModifier.Length; i++)
            {
                QuestModifierUIHolder questModifierUI = Instantiate(m_MalusMapModifierPrefab, m_MalusMapModifierTransform);
                questModifierUI.Init(quest.MalusModifier[i]);
            }
        }
    }
}