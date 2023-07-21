using KarpysDev.Script.Manager;
using KarpysDev.Script.Map_Related.MapGeneration;
using UnityEngine;

namespace KarpysDev.Script.Map_Related.QuestRelated
{
    public class Quest
    {
        private QuestScriptable m_BaseQuestScriptableData;
        private QuestDifficulty m_QuestDifficulty = QuestDifficulty.Easy;
        private float m_QuestGoldAmount = 0;
        private float m_QuestExpAmount = 0;

        public string QuestName => m_BaseQuestScriptableData.QuestName;
        public float QuestDifficultyPercent => QuestLibrary.Instance.GetDifficultyPercennt(m_QuestDifficulty);
        public QuestDifficulty QuestDifficulty => m_QuestDifficulty;
        public float QuestGoldAmount => m_QuestGoldAmount;
        public float QuestExpAmount => m_QuestExpAmount;
        public MapGroup MapGroup => m_BaseQuestScriptableData.MapGroup;
        public Sprite QuestPortalIcon => m_BaseQuestScriptableData.QuestPortalIcon;

        public Quest(QuestScriptable questScriptable, QuestDifficulty difficulty)
        {
            m_BaseQuestScriptableData = questScriptable;
            m_QuestDifficulty = difficulty;

            m_QuestGoldAmount = m_BaseQuestScriptableData.BaseGoldAmount * QuestDifficultyPercent / 100;
            m_QuestExpAmount = m_BaseQuestScriptableData.BaseExpAmount * QuestDifficultyPercent / 100;
        }

        public void PopLoot()
        {
            GoldManager.Instance.SpawnGoldAmount(MapData.Instance.GetTilePosition(GameManager.Instance.PlayerEntity.EntityPosition)
                ,GameManager.Instance.PlayerEntity.transform,10,m_QuestGoldAmount);
        }
    }
}