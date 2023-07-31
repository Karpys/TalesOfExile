using System;
using KarpysDev.Script.Entities;
using UnityEngine;

namespace KarpysDev.Script.Map_Related.QuestRelated
{
    public class QuestModifierManager : SingletonMonoBehavior<QuestModifierManager>
    {
        public Action<BoardEntity> OnMapEntitySpawn = null;
        public void ApplyQuestModifier(Quest quest)
        {
            int i = 0;
            for (; i < quest.MalusModifier.Length; i++)
            {
                QuestUtils.ApplyQuestModifier(quest.MalusModifier[i],this);
            }

            for (i = 0; i < quest.BonusModifier.Length; i++)
            {
                QuestUtils.ApplyQuestModifier(quest.BonusModifier[i],this);
            }
        }

        public void Clear()
        {
            OnMapEntitySpawn = null;
        }
    }
}