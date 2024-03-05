using KarpysDev.Script.Entities.BuffRelated;

namespace KarpysDev.Script.Spell.ParameterLessSpell
{
    using Entities;
    using Manager.Library;

    public class SkeletonCurseBuffGiverTrigger : BuffGiverTrigger
    {
        private int m_SkeletonSpawnCount = 0;
        public SkeletonCurseBuffGiverTrigger(BaseSpellTriggerScriptable baseScriptable, BuffGroup buffGroup,BuffType buffType, BuffCooldown buffCooldown,int buffDuration, float skeletonLifeTurn,VisualEffectType visualEffectType,int skeletonSpawnCount) : base(baseScriptable, buffGroup, buffType,buffCooldown, buffDuration,skeletonLifeTurn,visualEffectType)
        {
            m_SkeletonSpawnCount = skeletonSpawnCount;
        }

        protected override Buff BuffToAdd(BoardEntity caster, BoardEntity receiver)
        {
            return new SpawnSkeletonCurse(caster, receiver, BuffType.SkeletonCurse,m_BuffGroup,m_BuffDuration,m_BuffValue, m_SkeletonSpawnCount);
        }
    }
}