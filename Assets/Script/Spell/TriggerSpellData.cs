using System.Linq;
using KarpysDev.Script.Entities;
using KarpysDev.Script.Utils;
using UnityEngine;

namespace KarpysDev.Script.Spell
{
    public class TriggerSpellData:SpellData
    {
        private TriggerSpellDataScriptable m_TriggerData = null;
        public TriggerSpellDataScriptable TriggerData => m_TriggerData;
        //SpellTrigger Class//
        public BaseSpellTrigger SpellTrigger = null;
        //Spell Variables//
        private int m_BaseCooldown = 0;
        private int m_CurrentCooldown = 0;
        private int m_CooldownReduction = 0;
        //Spell Level//
        private int m_SpellLevel = 1;
        private ILevelScaler[] m_LevelScalers = null;
    
        //Intern Params//
        private bool m_HasBeenUsed = false;
        private SpellLearnType m_SpellLearnType = SpellLearnType.Learned;
        private bool m_IsBuffToggle = false;

        public int EffectiveCooldown => Mathf.Max(0,m_BaseCooldown - m_CooldownReduction);
        public bool IsBuffToggle => m_IsBuffToggle;
        public SpellLearnType SpellLearnType => m_SpellLearnType;
        public int SpellLevel => m_SpellLevel;
        public float LevelRatio => (float)m_SpellLevel / (m_TriggerData.LevelMax);
        
        public TriggerSpellData(SpellInfo baseSpellInfo, BoardEntity attachedEntity) : base(baseSpellInfo, attachedEntity) {}

        public override SpellData Initialize(SpellInfo spellInfo, BoardEntity attachedEntity)
        {
            m_SpellLevel = spellInfo.InitialSpellLevel;
            m_TriggerData = (TriggerSpellDataScriptable)m_Data;
            m_SpellLearnType = spellInfo.SpellLearnType;
            AssignLevelScaler();
            SpellTrigger = TriggerData.SpellTrigger.SetUpTrigger();
            SpellTrigger.SetAttachedSpell(this,spellInfo.SpellPriority);
            m_IsBuffToggle = TriggerData.SpellGroups.Contains(SpellGroup.BuffToggle);
            ApplyLevel();
            m_BaseCooldown = m_TriggerData.BaseCooldown;
            SpellTrigger.ComputeSpellData(AttachedEntity);
            return this;
        }

        private void AssignLevelScaler()
        {
            m_LevelScalers = new ILevelScaler[m_TriggerData.LevelScalerScriptables.Length];
            
            for (int i = 0; i < m_LevelScalers.Length; i++)
            {
                m_LevelScalers[i] = m_TriggerData.LevelScalerScriptables[i].GetBaseSpellLevelScaler();
            }
        }

        private void ApplyLevel()
        {
            foreach (ILevelScaler levelScaler in m_LevelScalers)
            {
                levelScaler.Apply(this);
            }
        }
        
        public override object Clone()
        {
            return MemberwiseClone();
        }

        public ZoneSelection GetMainSelection()
        {
            if (TriggerData.Selection.Length == 0)
                return null;
        
            return TriggerData.Selection[TriggerData.MainSelection];
        }
        
        public void Cast(TriggerSpellData spellData, SpellTiles spellTiles,bool mainCast = true,float efficiency = 1,bool freeCast = false)
        {
            SpellTrigger.CastSpell(spellData,spellTiles,mainCast,efficiency);
        
            if(freeCast)
                return;
        
            m_HasBeenUsed = true;
            LaunchCooldown();
        }

        private void LaunchCooldown()
        {
            m_CurrentCooldown = EffectiveCooldown;
        }

        public void ReduceCooldown()
        {
            if (m_HasBeenUsed)
            {
                m_HasBeenUsed = false;
                return;
            }
        
            if(m_CurrentCooldown == 0)
                return;
        
            m_CurrentCooldown -= 1;
        }

        public bool IsCooldownReady()
        {
            return m_CurrentCooldown <= 0;
        }

        public float GetCooldownRatio()
        {
            if (EffectiveCooldown == 0)
                return 0;
        
            return (float)m_CurrentCooldown / EffectiveCooldown;
        }

        public override string GetSpellDescription()
        {
            string description = StringUtils.GetDescription(m_Data.BaseDescription,SpellTrigger.GetDescriptionParts());
            return description;
        }

        public void ChangeCooldownReduction(int value)
        {
            m_CooldownReduction += value;
        }
    }

    public class SupportSpellData:SpellData
    {
        public SupportSpellData(SpellInfo baseSpellInfo, BoardEntity attachedEntity) : base(baseSpellInfo, attachedEntity)
        {
        }
    }
}