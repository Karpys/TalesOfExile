using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpellData
{
    public SpellDataScriptable Data = null;
    [HideInInspector]public BoardEntity AttachedEntity = null;


    public virtual SpellInfo Initialize(int priority)
    {
        //TODO:SAME with support Spells
        switch (Data.SpellType)
        {
            case SpellType.Trigger:
                TriggerSpellData triggerSpellData = new TriggerSpellData(this);
                return triggerSpellData.Initialize(priority);
            case SpellType.Buff:
                TriggerSpellData buffSpellData = new TriggerSpellData(this);
                return buffSpellData.Initialize(priority);
            default:
                Debug.LogError("Missing SpellType Implementation");
                return new SpellInfo(this,priority);
        }
    }

    public SpellData(SpellData baseSpellData)
    {
        Data = baseSpellData.Data;
        AttachedEntity = baseSpellData.AttachedEntity;
    }
    
    public virtual object Clone()
    {
        return MemberwiseClone();
    }
}