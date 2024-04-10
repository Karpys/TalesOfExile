namespace KarpysDev.Script.Entities.BuffRelated
{
    public class AddShieldOnBuffApplied : OnBuffCategoryApplied
    {
        private int m_TimeShieldDuration = 0;
        public AddShieldOnBuffApplied(BoardEntity caster, BoardEntity receiver, BuffType buffType, BuffGroup buffGroup, int cooldown, float buffValue, BuffCategory[] categories, bool ignoreFirstBehaveTurn, BuffCategory targetCategory,int timeShieldDuration) : base(caster, receiver, buffType, buffGroup, cooldown, buffValue, categories, ignoreFirstBehaveTurn, targetCategory)
        {
            m_TimeShieldDuration = timeShieldDuration;
        }

        protected override void OnBuffApplied(Buff buff)
        {
            m_Receiver.Life.AddShield(m_BuffValue,m_TimeShieldDuration);
        }
    }
}