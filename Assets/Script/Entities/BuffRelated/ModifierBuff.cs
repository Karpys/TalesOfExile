public class ModifierBuff : Buff
{
    private Modifier m_Modifier = null;
    public override void InitializeBuff(BoardEntity entity, int cooldown, float buffValue, object[] args)
    {
        m_Modifier = args[0] as Modifier;
        base.InitializeBuff(entity, cooldown, buffValue, args);
    }

    protected override void Apply()
    {
        ModifierUtils.ApplyModifier(m_Modifier,m_AttachedEntity);
        m_AttachedEntity.ComputeAllSpells();
    }

    protected override void UnApply()
    {
        ModifierUtils.UnapplyModifier(m_Modifier,m_AttachedEntity);
        m_AttachedEntity.ComputeAllSpells();
    }
}