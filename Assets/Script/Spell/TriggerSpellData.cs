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
    
        //Intern Params//
        private bool m_HasBeenUsed = false;
        private SpellLearnType m_SpellLearnType = SpellLearnType.Learned;
        private bool m_IsBuffToggle = false;

        public bool IsBuffToggle => m_IsBuffToggle;
        public SpellLearnType SpellLearnType => m_SpellLearnType;
        public override SpellData Initialize(SpellInfo spellInfo, BoardEntity attachedEntity)
        {
            m_TriggerData = (TriggerSpellDataScriptable)m_Data;
            m_BaseCooldown = TriggerData.m_BaseCooldown;
            m_SpellLearnType = spellInfo.m_SpellLearnType;
            SpellTrigger = TriggerData.m_SpellTrigger.SetUpTrigger();
            SpellTrigger.SetAttachedSpell(this,spellInfo.m_SpellPriority);
            SpellTrigger.ComputeSpellData(AttachedEntity);
            m_IsBuffToggle = TriggerData.SpellGroups.Contains(SpellGroup.BuffToggle);
            return this;
        }

        public override object Clone()
        {
            return MemberwiseClone();
        }

        public ZoneSelection GetMainSelection()
        {
            if (TriggerData.m_Selection.Length == 0)
                return null;
        
            return TriggerData.m_Selection[TriggerData.MainSelection];
        }

        //Use and Cooldown Part
        //TODO: Need to Implemente the CooldownSystem
        public void Cast(TriggerSpellData spellData, SpellTiles spellTiles,float efficiency = 1,bool freeCast = false)
        {
            SpellTrigger.CastSpell(spellData,spellTiles,efficiency);
        
            if(freeCast)
                return;
        
            m_HasBeenUsed = true;
            LaunchCooldown();
        }

        private void LaunchCooldown()
        {
            m_CurrentCooldown = m_BaseCooldown;
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
            if (m_BaseCooldown == 0)
                return 0;
        
            return (float)m_CurrentCooldown / m_BaseCooldown;
        }

        public override string GetSpellDescription()
        {
            string description = StringUtils.GetDescription(m_Data.BaseDescription,SpellTrigger.GetDescriptionParts());
            return description;
        }

        public TriggerSpellData(SpellInfo baseSpellInfo, BoardEntity attachedEntity) : base(baseSpellInfo, attachedEntity) {}
    }

    public class SupportSpellData:SpellData
    {
        public SupportSpellData(SpellInfo baseSpellInfo, BoardEntity attachedEntity) : base(baseSpellInfo, attachedEntity)
        {
        }
    }
}