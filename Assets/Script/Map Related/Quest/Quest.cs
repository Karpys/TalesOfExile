using KarpysDev.Script.Map_Related.MapGeneration;

namespace KarpysDev.Script.Map_Related.Quest
{
    public class Quest
    {
        private QuestScriptable m_BaseQuestScriptableData;
        private QuestDifficulty m_QuestDifficulty = QuestDifficulty.Easy;
        private float m_QuestGoldAmmount = 0;
        private float m_QuestExpAmmount = 0;

        public string QuestName => m_BaseQuestScriptableData.QuestName;
        public float QuestDifficultyPercent => QuestLibrary.Instance.GetDifficultyPercennt(m_QuestDifficulty);
        public QuestDifficulty QuestDifficulty => m_QuestDifficulty;
        public float QuestGoldAmmount => m_QuestGoldAmmount;
        public float QuestExpAmmount => m_QuestExpAmmount;
        public MapGroup MapGroup => m_BaseQuestScriptableData.MapGroup;

        public Quest(QuestScriptable questScriptable, QuestDifficulty difficulty)
        {
            m_BaseQuestScriptableData = questScriptable;
            m_QuestDifficulty = difficulty;

            m_QuestGoldAmmount = m_BaseQuestScriptableData.BaseGoldAmmount * QuestDifficultyPercent / 100;
            m_QuestExpAmmount = m_BaseQuestScriptableData.BaseExpAmmount * QuestDifficultyPercent / 100;
        }
    }
}