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
        [SerializeField] private SpellDataScriptable m_SpellData = null;
        [SerializeField] private int m_SpellPriority = 0;
        [SerializeField] private int m_InitialSpellLevel = 0;
        [HideInInspector] [SerializeField] private SpellLearnType m_SpellLearnType = SpellLearnType.Learned;

        public SpellDataScriptable SpellData => m_SpellData;
        public int SpellPriority => m_SpellPriority;
        public int InitialSpellLevel => m_InitialSpellLevel;
        public SpellLearnType SpellLearnType => m_SpellLearnType;
    
        public SpellInfo(SpellDataScriptable spellData, int priority,int spellLevel,SpellLearnType spellLearnType)
        {
            m_SpellData = spellData;
            m_SpellPriority = priority;
            m_SpellLearnType = spellLearnType;
            m_InitialSpellLevel = spellLevel;
        }
    }
    
    public enum SpellLearnType
    {
        Learned,
        Equipement,
    }
}