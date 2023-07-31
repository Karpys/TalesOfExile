using System;
using System.Collections.Generic;
using KarpysDev.Script.Entities;
using UnityEngine;


namespace KarpysDev.Script.Map_Related.QuestRelated
{
    public static class QuestUtils
    {
        private static readonly Dictionary<QuestModifierType, Action<QuestModifier,QuestModifierManager>> applyActions;

        static QuestUtils()
        {
            applyActions = new Dictionary<QuestModifierType, Action<QuestModifier,QuestModifierManager>>()
            {
                {QuestModifierType.IncreaseMonsterMaxLife, (m,q) =>
                    {
                        MaxLifeQuestModifier questMaxLifeModifier = new MaxLifeQuestModifier(m.FloatValue);
                        q.OnMapEntitySpawn += questMaxLifeModifier.Trigger;
                    }
                },
                {QuestModifierType.IncreaseMonsterMonsterElementalResistance, (m,q) =>
                    {
                        IncreaseElementalResistanceQuestModifier questMaxLifeModifier = new IncreaseElementalResistanceQuestModifier(m.FloatValue);
                        q.OnMapEntitySpawn += questMaxLifeModifier.Trigger;
                    }
                },
                {QuestModifierType.IncreaseMonsterMonsterPhysicalResistance, (m,q) =>
                    {
                        IncreasePhysicalResistanceQuestModifier questMaxLifeModifier = new IncreasePhysicalResistanceQuestModifier(m.FloatValue);
                        q.OnMapEntitySpawn += questMaxLifeModifier.Trigger;
                    }
                },
            };
        }

        public static void ApplyQuestModifier(QuestModifier questModifier,QuestModifierManager modifierManager)
        {
            if(applyActions.TryGetValue(questModifier.QuestModifierType,out var action))
            {
                action.Invoke(questModifier,modifierManager);
            }else
            {
                Debug.LogError("Map Modifier has not been set up :" + questModifier.QuestModifierType);
            }
        }

        public static QuestModifier[] ToQuestModifier(this List<QuestModifier> questModifierCollection, float difficultyPercent)
        {
            int questModCount = questModifierCollection.Count;
            QuestModifier[] questModifiers = new QuestModifier[questModCount];

            for (int i = 0; i < questModCount; i++)
            {
                questModifiers[i] = new QuestModifier(questModifierCollection[i], difficultyPercent);
            }

            return questModifiers;
        }
    }
    
    public abstract class EntityQuestModifierHolder
    {
        public abstract void Trigger(BoardEntity entity);
    }

    public class MaxLifeQuestModifier:EntityQuestModifierHolder
    {
        private float m_MaxLifePercentage = 0;
        
        public MaxLifeQuestModifier(float maxLifePercentage)
        {
            m_MaxLifePercentage = maxLifePercentage;
        }
        
        public override void Trigger(BoardEntity entity)
        {
            float addLife = m_MaxLifePercentage * entity.Life.MaxLife / 100;
            entity.Life.ChangeMaxLifeValue(addLife);
            entity.Life.SetToMaxLife();
        }
    }
    
    public class IncreasePhysicalResistanceQuestModifier:EntityQuestModifierHolder
    {
        private float m_ResistancePercent = 0;
        
        public IncreasePhysicalResistanceQuestModifier(float resistancePercent)
        {
            m_ResistancePercent = resistancePercent;
        }
        
        public override void Trigger(BoardEntity entity)
        {
            entity.EntityStats.PhysicalDamageReduction += m_ResistancePercent;
        }
    }
    
    public class IncreaseElementalResistanceQuestModifier:EntityQuestModifierHolder
    {
        private float m_ResistancePercent = 0;
        
        public IncreaseElementalResistanceQuestModifier(float resistancePercent)
        {
            m_ResistancePercent = resistancePercent;
        }
        
        public override void Trigger(BoardEntity entity)
        {
            entity.EntityStats.ElementalDamageReduction += m_ResistancePercent;
        }
    }
}