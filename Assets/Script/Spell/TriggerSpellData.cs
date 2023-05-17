using UnityEngine;

public class TriggerSpellData:SpellData
{
    public TriggerSpellDataScriptable TriggerData => m_Data as TriggerSpellDataScriptable;
    //SpellTrigger Class//
    public BaseSpellTrigger SpellTrigger = null;
    //Spell Variables//
    private int m_BaseCooldown = 0;
    private int m_CurrentCooldown = 0;
    
    //Intern Params//
    private bool m_HasBeenUsed = false;

    public override SpellData Initialize(SpellInfo spellInfo, BoardEntity attachedEntity)
    {
        m_BaseCooldown = TriggerData.m_BaseCooldown;
        SpellTrigger = TriggerData.m_SpellTrigger.SetUpTrigger();
        SpellTrigger.SetAttachedSpell(this,spellInfo.m_SpellPriority);
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
    public void Cast(TriggerSpellData spellData, SpellTiles spellTiles,bool freeCast = false)
    {
        SpellTrigger.CastSpell(spellData,spellTiles);
        
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
    
    public TriggerSpellData(SpellInfo baseSpellInfo, BoardEntity attachedEntity) : base(baseSpellInfo, attachedEntity)
    {
    }
}

public class SupportSpellData:SpellData
{
    public SupportSpellData(SpellInfo baseSpellInfo, BoardEntity attachedEntity) : base(baseSpellInfo, attachedEntity)
    {
    }
}