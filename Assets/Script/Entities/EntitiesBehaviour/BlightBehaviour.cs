public class BlightBehaviour:EntityBehaviour
{
    private BlightSpawner m_BlightSpawner = null;
    private int m_PathId = 0;
    public BlightBehaviour(BoardEntity entity,BlightSpawner blightSpawner) : base(entity)
    {
        m_BlightSpawner = blightSpawner;
    }

    public override void Behave()
    {
        Tile nextTile = m_BlightSpawner.GetNextBranchPath(m_PathId);
        
        if(nextTile == null)
            return;
        
        m_AttachedEntity.MoveTo(nextTile.XPos, nextTile.YPos);
        m_PathId += 1;
    }
}