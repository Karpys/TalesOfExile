using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpellData
{
    public SpellDataScriptable Data = null;
    [HideInInspector]public BoardEntity AttachedEntity = null;
    //Mainly used for support spells
    public SpellList ConnectedSpellData = null;


    public virtual SpellData Initialize()
    {
        ConnectedSpellData = ConnectedSpellData.Clone();
        //TODO:SAME with support Spells
        switch (Data.SpellType)
        {
            case SpellType.Trigger:
                TriggerSpellData triggerSpellData = new TriggerSpellData(this);
                return triggerSpellData.Initialize();
            default:
                return this;
        }
    }
    public virtual object Clone()
    {
        return MemberwiseClone();
    }
}