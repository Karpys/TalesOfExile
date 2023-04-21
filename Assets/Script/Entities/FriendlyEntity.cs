﻿using TweenCustom;

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
                
        GameManager.Instance.UnRegisterEntity(this);
        RemoveFromBoard();
        Destroy(gameObject);
        base.TriggerDeath();
    }
    
    protected override void Movement()
    {
        transform.DoKill();
        transform.DoMove( m_TargetMap.GetTilePosition(m_XPosition, m_YPosition),0.1f);
    }
}