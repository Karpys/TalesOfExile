namespace KarpysDev.Script.Entities.BuffRelated
{
    public class StunDebuff : Buff
    {
        public StunDebuff(BoardEntity caster, BoardEntity receiver, BuffType buffType,int cooldown, float buffValue) : base(caster, receiver, buffType,cooldown, buffValue)
        {
        }

        public override void Apply()
        {
            m_Receiver.EntityStats.AddStunLock(1);
        }

        protected override void UnApply()
        {
            m_Receiver.EntityStats.AddStunLock(-1);
        }
    }
}