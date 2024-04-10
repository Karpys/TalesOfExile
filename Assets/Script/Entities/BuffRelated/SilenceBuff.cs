namespace KarpysDev.Script.Entities.BuffRelated
{

    public class SilenceBuff : Buff
    {
        public SilenceBuff(BoardEntity caster, BoardEntity receiver, BuffType buffType,BuffGroup buffGroup,int cooldown, float buffValue, BuffCategory[] categories, bool ignoreFirstBehaveTurn) : base(caster, receiver, buffType, buffGroup,cooldown, buffValue, categories,ignoreFirstBehaveTurn)
        {
        }

        public override void Apply()
        {
            m_Receiver.EntityStats.SpellLockCount += 1;
        }

        protected override void UnApply()
        {
            m_Receiver.EntityStats.SpellLockCount -= 1;
        }
    }
}