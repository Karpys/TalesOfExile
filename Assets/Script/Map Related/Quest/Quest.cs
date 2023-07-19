namespace KarpysDev.Script.Map_Related.Quest
{
    public class Quest
    {
        private QuestScriptable m_BaseQuestScriptableData;
        public QuestDifficulty QuestDifficulty = QuestDifficulty.Easy;
        public float QuestGoldAmmount = 0;
        public float QuestExpAmmount = 0;

        public string QuestName => m_BaseQuestScriptableData.QuestName;
        public float QuestDifficultyPercent => QuestLibrary.Instance.GetDifficultyPercennt(QuestDifficulty);

        public Quest(QuestScriptable questScriptable, QuestDifficulty difficulty)
        {
            m_BaseQuestScriptableData = questScriptable;
            QuestDifficulty = difficulty;

            QuestGoldAmmount = m_BaseQuestScriptableData.BaseGoldAmmount * QuestDifficultyPercent / 100;
            QuestExpAmmount = m_BaseQuestScriptableData.BaseExpAmmount * QuestDifficultyPercent / 100;
        }
    }
}