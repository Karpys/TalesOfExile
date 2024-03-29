using UnityEngine;

[System.Serializable]
public class TriggerSpellData:SpellData
{
    public TriggerSpellDataScriptable TriggerData => Data as TriggerSpellDataScriptable;
    //SpellTrigger Class//
    public BaseSpellTrigger SpellTrigger = null;
    //Spell Variables//
    private int m_BaseCooldown = 0;
    private int m_CurrentCooldown = 0;
    
    //Intern Params//
    private bool m_HasBeenUsed = false;
    public TriggerSpellData(SpellData spellData) : base(spellData)
    {
        AttachedEntity = spellData.AttachedEntity;
        Data = spellData.Data;
        ConnectedSpellData = spellData.ConnectedSpellData;
    }

    public override SpellData Initialize()
    {
        m_BaseCooldown = TriggerData.m_BaseCooldown;
        SpellTrigger = TriggerData.m_SpellTrigger.SetUpTrigger();
        SpellTrigger.SetAttachedSpell(this);
        SpellTrigger.ComputeSpellData(AttachedEntity);
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
    public void Cast(TriggerSpellData spellData, SpellTiles spellTiles)
    {
        m_HasBeenUsed = true;
        SpellTrigger.Trigger(spellData,spellTiles);
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