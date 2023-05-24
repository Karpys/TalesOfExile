using KarpysDev.Script.Entities.BuffRelated;
using KarpysDev.Script.Manager.Library;

namespace KarpysDev.Script.Spell.ParameterLessSpell
{
    public class SkeletonCurseBuffGiverTrigger : BuffGiverTrigger
    {
        public SkeletonCurseBuffGiverTrigger(BaseSpellTriggerScriptable baseScriptable, BuffGroup buffGroup,BuffType buffType, int buffDuration, float skeletonLifeTurn,int skeletonSpawnCount) : base(baseScriptable, buffGroup, buffType, buffDuration, skeletonLifeTurn)
        {
            m_BuffArgs = new object[1];
            m_BuffArgs[0] = skeletonSpawnCount;
        }
    }
}