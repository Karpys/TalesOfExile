public class SkeletonCurseBuffGiverTrigger : BuffGiverTrigger
{
    public SkeletonCurseBuffGiverTrigger(BaseSpellTriggerScriptable baseScriptable, BuffType buffType, int buffDuration, float skeletonLifeTurn,int skeletonSpawnCount) : base(baseScriptable, buffType, buffDuration, skeletonLifeTurn)
    {
        m_BuffArgs = new object[1];
        m_BuffArgs[0] = skeletonSpawnCount;
    }
}