public class RockThrowBuffTrigger : SelectionSpellTrigger
{
    private int m_Duration = 5;
    public RockThrowBuffTrigger(BaseSpellTriggerScriptable baseScriptable) : base(baseScriptable)
    {
    }
    
    public RockThrowBuffTrigger(BaseSpellTriggerScriptable baseScriptable,string duration) : base(baseScriptable)
    {
        m_Duration = int.Parse(duration);
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
        Buff buff = BuffLibrary.Instance.GetBuffViaKey(BuffType.RockThrowBuff,spellData.AttachedEntity);
        buff.InitializeBuff(spellData.AttachedEntity,spellData.AttachedEntity,m_Duration,0);
        base.Trigger(spellData, spellTiles);
    }
}