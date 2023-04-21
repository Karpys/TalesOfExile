public class RockThrowBuffTrigger : SelectionSpellTrigger
{
    private int m_Duration = 5;
    public RockThrowBuffTrigger(BaseSpellTriggerScriptable baseScriptable) : base(baseScriptable)
    {
    }
    
    public RockThrowBuffTrigger(BaseSpellTriggerScriptable baseScriptable,int duration) : base(baseScriptable)
    {
        m_Duration = duration;
    }

    protected override int GetSpellPriority()
    {
        return base.GetSpellPriority() + 1;
    }

    public override void ComputeSpellPriority()
    {
        m_SpellPriority = 0;
    }
    
    public override void Trigger(TriggerSpellData spellData, SpellTiles spellTiles)
    {
        Buff buff = BuffLibrary.Instance.AddBuffToViaKey(BuffType.RockThrowBuff,spellData.AttachedEntity);
        buff.InitializeBuff(spellData.AttachedEntity,spellData.AttachedEntity,m_Duration,0);
        base.Trigger(spellData, spellTiles);
    }
}