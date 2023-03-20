public class RegenerationBuffTrigger : SelectionSpellTrigger
{
    private int m_RegenerationDuration = 0;
    private float m_RegenerationValue = 0;
    public RegenerationBuffTrigger(BaseSpellTriggerScriptable baseScriptable) : base(baseScriptable)
    {
    }
    
    public RegenerationBuffTrigger(BaseSpellTriggerScriptable baseScriptable, string regenerationCooldown,string regenerationValue) : base(baseScriptable)
    {
        m_RegenerationDuration = int.Parse(regenerationCooldown);
        m_RegenerationValue = float.Parse(regenerationValue);
    }

    public override void Trigger(TriggerSpellData spellData, SpellTiles spellTiles)
    {
        Buff buff = BuffLibrary.Instance.GetBuffViaKey(BuffType.RegenerationBuff,spellData.AttachedEntity);
        buff.InitializeBuff(spellData.AttachedEntity,m_RegenerationDuration,m_RegenerationValue);
        
        m_SpellAnimDelay = 0.1f;
        base.Trigger(spellData, spellTiles);
    }

    protected override int GetSpellPriority()
    {
        BoardEntity entity = m_AttachedSpell.AttachedEntity;
        if (entity.Life.Life < entity.Life.MaxLife)
            return 1;
        
        return 0;
    }

    public override void ComputeSpellPriority()
    {
        m_SpellPriority = 0;
    }
}