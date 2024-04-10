namespace KarpysDev.Script.Spell.ParameterLessSpell
{
    using Entities;
    using Entities.BuffRelated;
    using Manager.Library;

    public class OnStunAddShieldBuffGiverTrigger : BuffGiverTrigger
    {
        private int m_TimeShieldDuration = 0;
        public OnStunAddShieldBuffGiverTrigger(BaseSpellTriggerScriptable baseScriptable, BuffGroup buffGroup, BuffType buffType, BuffCooldown buffCooldown, int buffDuration, float buffValue, VisualEffectType visualEffectType,int timeShieldDuration) : base(baseScriptable, buffGroup, buffType, buffCooldown, buffDuration, buffValue, visualEffectType)
        {
            m_TimeShieldDuration = timeShieldDuration;
        }

        protected override Buff BuffToAdd(BoardEntity caster, BoardEntity receiver)
        {
            return new AddShieldOnBuffApplied(caster,receiver,BuffType.MonkShieldStun,BuffGroup.Buff,m_BuffDuration,m_BuffValue,BuffConst.NotImplemented,false,BuffCategory.Stun,m_TimeShieldDuration);
        }
    }
}