namespace KarpysDev.Script.Entities.BuffRelated
{
    public class RegenerationBuff : Buff
    {
        protected override void Apply()
        {
            m_Receiver.Life.AddRegeneration(m_BuffValue);
        }

        protected override void UnApply()
        {
            m_Receiver.Life.AddRegeneration(-m_BuffValue);
        }
    }
}