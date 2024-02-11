using System;
using KarpysDev.Script.Spell;
using UnityEngine;

namespace KarpysDev.Script.Entities
{
    [CreateAssetMenu(fileName = "BoardEntity", menuName = "BoardEntity data", order = 0)]
    public class BoardEntityDataScriptable : ScriptableObject
    {
        [Header("Base Entity Data")]
        public BoardEntityData m_EntityBaseData;
    }

    [Serializable]
    public class BoardEntityData
    {
        public EntityGroup m_EntityGroup = EntityGroup.Neutral;
        public EntityGroup m_TargetEntityGroup = EntityGroup.Neutral;
        public EntityStats m_Stats = null;
        public SpellInfo[] m_BaseSpellInfos = null;
        public BoardEntityData(BoardEntityData data)
        {
            m_EntityGroup = data.m_EntityGroup;
            m_Stats = new EntityStats(data.m_Stats);
            m_BaseSpellInfos = data.m_BaseSpellInfos;
        }
    }

    [Serializable]
    public class SpellInfo
    {
        public SpellDataScriptable m_SpellData = null;
        public int m_SpellPriority = 0;
        [HideInInspector] public SpellLearnType m_SpellLearnType = SpellLearnType.Learned;
    
        public SpellInfo(SpellDataScriptable spellData, int priority,SpellLearnType spellLearnType)
        {
            m_SpellData = spellData;
            m_SpellPriority = priority;
            m_SpellLearnType = spellLearnType;
        }
    }
    
    public enum SpellLearnType
    {
        Learned,
        Equipement,
    }
}