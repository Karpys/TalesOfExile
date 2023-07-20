using KarpysDev.Script.Map_Related.QuestRelated;
using TMPro;
using UnityEngine;

namespace KarpysDev.Script.UI
{
    public class QuestDisplayer : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_QuestName = null;
        [SerializeField] private TMP_Text m_QuestDifficulty = null;
        [SerializeField] private Transform m_BonusMapModifierTransform = null;
        [SerializeField] private Transform m_MalusMapModifierTransform = null;
        [SerializeField] private TMP_Text m_GoldAmmount= null;
        [SerializeField] private TMP_Text m_ExpAmmount = null;
        
        private Quest m_Quest = null;
        public Quest Quest => m_Quest;
        public void AssignQuest(Quest quest)
        {
            m_Quest = quest;
            m_QuestDifficulty.color = QuestLibrary.Instance.GetDifficultyColor(m_Quest.QuestDifficulty);
            m_QuestName.text = m_Quest.QuestName;
            m_QuestDifficulty.text = m_Quest.QuestDifficulty+"\n"+m_Quest.QuestDifficultyPercent+"%";

            m_GoldAmmount.text = m_Quest.QuestGoldAmmount.ToString("0");
            m_ExpAmmount.text = m_Quest.QuestExpAmmount.ToString("0");
        }
    }
}