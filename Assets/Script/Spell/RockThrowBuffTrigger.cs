public class RockThrowBuffTrigger : SelectionSpellTrigger
{
    public RockThrowBuffTrigger(BaseSpellTriggerScriptable baseScriptable) : base(baseScriptable)
    {
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
        buff.InitializeBuff(spellData.AttachedEntity,spellData.AttachedEntity,10,0);
        base.Trigger(spellData, spellTiles);
    }
}