using UnityEngine;

[System.Serializable]
public class TriggerSpellData:SpellData
{
    public TriggerSpellDataScriptable TriggerData => Data as TriggerSpellDataScriptable;
    //SpellTrigger Class//
    public BaseSpellTrigger SpellTrigger = null;
    //Spell Variables//

    public TriggerSpellData(SpellData spellData) : base(spellData)
    {
        AttachedEntity = spellData.AttachedEntity;
        Data = spellData.Data;
        ConnectedSpellData = spellData.ConnectedSpellData;
    }

    public override SpellData Initialize()
    {
        SpellTrigger = TriggerData.m_SpellTrigger.SetUpTrigger();
        SpellTrigger.ComputeSpellData(AttachedEntity);
        return this;
    }
    public override object Clone()
    {
        return MemberwiseClone();
    }

    public ZoneSelection GetMainSelection()
    {
        return TriggerData.m_Selection[TriggerData.MainSelection];
    }
}

public class SupportSpellData:SpellData
{
    public override SpellData Initialize()
    {
        Debug.Log("Init Support Spell");
        return this;
    }

    public SupportSpellData(SpellData baseSpellData) : base(baseSpellData)
    {
    }
}