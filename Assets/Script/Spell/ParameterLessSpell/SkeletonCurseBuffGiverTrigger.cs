using KarpysDev.Script.Entities.BuffRelated;
using KarpysDev.Script.Manager.Library;

namespace KarpysDev.Script.Spell.ParameterLessSpell
{
    using Entities;

    public class SkeletonCurseBuffGiverTrigger : BuffGiverTrigger
    {
        private int m_SkeletonSpawnCount = 0;
        public SkeletonCurseBuffGiverTrigger(BaseSpellTriggerScriptable baseScriptable, BuffGroup buffGroup,BuffType buffType, BuffCooldown buffCooldown,int buffDuration, float skeletonLifeTurn,int skeletonSpawnCount) : base(baseScriptable, buffGroup, buffType,buffCooldown, buffDuration, skeletonLifeTurn)
        {
            m_SkeletonSpawnCount = skeletonSpawnCount;
        }

        protected override Buff BuffToAdd(BoardEntity caster, BoardEntity receiver)
        {
            return new SpawnSkeletonCurse(caster, receiver, BuffType.SkeletonCurse,m_BuffDuration, m_BuffValue, m_SkeletonSpawnCount);
        }
    }
}