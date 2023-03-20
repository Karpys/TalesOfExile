public class RegenerationBuff : Buff
{
    protected override void Apply()
    {
        m_AttachedEntity.Life.AddRegeneration(m_BuffValue);
    }

    protected override void UnApply()
    {
        m_AttachedEntity.Life.AddRegeneration(-m_BuffValue);
    }
}