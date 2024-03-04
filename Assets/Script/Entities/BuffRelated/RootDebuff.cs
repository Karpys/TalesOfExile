namespace KarpysDev.Script.Entities.BuffRelated
{
    public class RootDebuff : Buff
    {
        public RootDebuff(BoardEntity caster, BoardEntity receiver, BuffType buffType, BuffGroup buffGroup, int cooldown, float buffValue) : base(caster, receiver, buffType, buffGroup, cooldown, buffValue)
        {
        }

        public override void Apply()
        {
            m_Receiver.EntityStats.RootLockCount += 1;
        }

        protected override void UnApply()
        {
            m_Receiver.EntityStats.RootLockCount -= 1;
        }
    }
}