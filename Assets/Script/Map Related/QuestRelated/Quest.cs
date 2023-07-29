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
        
        private QuestModifier[] m_BonusModifier = null;
        private QuestModifier[] m_MalusModifier = null;

        public string QuestName => m_BaseQuestScriptableData.QuestName;
        public float QuestDifficultyPercent => QuestLibrary.Instance.GetDifficultyPercennt(m_QuestDifficulty);
        public QuestDifficulty QuestDifficulty => m_QuestDifficulty;
        public float QuestGoldAmount => m_QuestGoldAmount;
        public float QuestExpAmount => m_QuestExpAmount;
        public MapGroup MapGroup => m_BaseQuestScriptableData.MapGroup;
        public Sprite QuestPortalIcon => m_BaseQuestScriptableData.QuestPortalIcon;

        public QuestModifier[] BonusModifier => m_BonusModifier;
        public QuestModifier[] MalusModifier => m_MalusModifier;

        public Quest(QuestScriptable questScriptable, QuestDifficulty difficulty)
        {
            m_BaseQuestScriptableData = questScriptable;
            m_QuestDifficulty = difficulty;

            m_QuestGoldAmount = m_BaseQuestScriptableData.BaseGoldAmount * QuestDifficultyPercent / 100;
            m_QuestExpAmount = m_BaseQuestScriptableData.BaseExpAmount * QuestDifficultyPercent / 100;

            Vector2Int modifierCount = QuestLibrary.Instance.GetModifierCount(difficulty);
            m_BonusModifier = questScriptable.BonusMapModifier.Draw(modifierCount.x).ToArray();
            m_MalusModifier = questScriptable.MalusMapModifier.Draw(modifierCount.y).ToArray();
        }

        public void PopLoot()
        {
            GoldManager.Instance.SpawnGoldAmount(MapData.Instance.GetTilePosition(GameManager.Instance.PlayerEntity.EntityPosition)
                ,GameManager.Instance.PlayerEntity.transform,10,m_QuestGoldAmount);
        }
    }
}