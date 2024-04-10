namespace KarpysDev.Script.Entities.BuffRelated
{
    public abstract class OnBuffCategoryApplied : Buff
    {
        private BuffCategory m_TargetCategory = BuffCategory.None;
        protected OnBuffCategoryApplied(BoardEntity caster, BoardEntity receiver, BuffType buffType, BuffGroup buffGroup, int cooldown, float buffValue, BuffCategory[] categories, bool ignoreFirstBehaveTurn,
        BuffCategory targetCategory) : base(caster, receiver, buffType, buffGroup, cooldown, buffValue, categories, ignoreFirstBehaveTurn)
        {
            m_TargetCategory = targetCategory;
        }

        public override void Apply()
        {
            m_Receiver.EntityEvent.AddBuffAppliedCategoryModification(m_TargetCategory,OnBuffApplied);
        }

        protected abstract void OnBuffApplied(Buff buff);

        protected override void UnApply()
        {
            m_Receiver.EntityEvent.RemoveBuffAppliedCategoryModification(m_TargetCategory,OnBuffApplied);
        }
    }
}