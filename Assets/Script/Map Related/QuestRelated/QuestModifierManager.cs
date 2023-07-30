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
                QuestUtils.ApplyMapModifier(quest.MalusModifier[i],this);
            }

            for (i = 0; i < quest.BonusModifier.Length; i++)
            {
                QuestUtils.ApplyMapModifier(quest.BonusModifier[i],this);
            }
        }
    }
}