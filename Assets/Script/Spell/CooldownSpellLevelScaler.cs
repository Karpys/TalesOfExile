﻿using KarpysDev.KarpysUtils;
using UnityEngine;

namespace KarpysDev.Script.Spell
{
    public class CooldownSpellLevelScaler : ILevelScaler
    {
        private CooldownSpellLevelScalerScriptable m_BaseData = null;
        private int m_LastCooldownValueApplied = 0;
        
        public CooldownSpellLevelScaler(CooldownSpellLevelScalerScriptable baseData)
        {
            m_BaseData = baseData;
        }
        
        public void Apply(TriggerSpellData triggerSpellData)
        {
            int cdReductionValue = m_BaseData.Evaluate(triggerSpellData.LevelRatio);
            if (cdReductionValue != m_LastCooldownValueApplied)
            {
                triggerSpellData.ChangeCooldownReduction(-m_LastCooldownValueApplied);
                m_LastCooldownValueApplied = cdReductionValue;
                triggerSpellData.ChangeCooldownReduction(cdReductionValue);
            }
        }
    }
}