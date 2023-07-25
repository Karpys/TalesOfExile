namespace KarpysDev.Script.Entities.BuffRelated
{
    public class SilenceBuff : Buff
    {
        protected override void Apply()
        {
            m_Receiver.EntityStats.SpellLockCount += 1;
        }

        protected override void UnApply()
        {
            m_Receiver.EntityStats.SpellLockCount -= 1;
        }
    }
}