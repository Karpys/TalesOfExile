public abstract class EntityBehaviour
{
    protected BoardEntity m_AttachedEntity = null;
    
    protected EntityBehaviour(BoardEntity entity)
    {
        m_AttachedEntity = entity;
    }
    public abstract void Behave();
}