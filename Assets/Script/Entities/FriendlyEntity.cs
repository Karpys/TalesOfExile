public class FriendlyEntity : BoardEntity
{
    protected override void InitalizeEntityBehaviour()
    {
        SetEntityBehaviour(new BaseEntityIA(this));
    }

    protected override void RegisterEntity()
    {
        GameManager.Instance.RegisterEntity(this);
    }

    public override void EntityAction()
    {
        m_EntityBehaviour.Behave();
    }

    protected override void TriggerDeath()
    {
        if (m_IsDead)
            return;
                
        base.TriggerDeath();
        GameManager.Instance.UnRegisterEntity(this);
        RemoveFromBoard();
        Destroy(gameObject);
    }
}