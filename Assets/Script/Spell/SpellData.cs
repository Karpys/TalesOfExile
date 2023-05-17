using System;
using System.Collections.Generic;
using UnityEngine;

public class SpellData
{
    protected SpellDataScriptable m_Data = null;
    protected BoardEntity m_AttachedEntity = null;

    public BoardEntity AttachedEntity => m_AttachedEntity;
    public SpellDataScriptable Data => m_Data;

    public virtual SpellData Initialize(SpellInfo spellInfo,BoardEntity attachedEntity)
    {
        //TODO:SAME with support Spells
        switch (m_Data.SpellType)
        {
            case SpellType.Trigger:
                TriggerSpellData triggerSpellData = new TriggerSpellData(spellInfo,attachedEntity);
                return triggerSpellData.Initialize(spellInfo, attachedEntity);
            case SpellType.Buff:
                TriggerSpellData buffSpellData = new TriggerSpellData(spellInfo,attachedEntity);
                return buffSpellData.Initialize(spellInfo, attachedEntity);
            default:
                Debug.LogError("Missing SpellType Implementation");
                return this;
        }
    }
    
    public SpellData(SpellInfo baseSpellInfo,BoardEntity attachedEntity)
    {
        m_Data = baseSpellInfo.m_SpellData;
        m_AttachedEntity = attachedEntity;
    }
    
    public virtual object Clone()
    {
        return MemberwiseClone();
    }
}