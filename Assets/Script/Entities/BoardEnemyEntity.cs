using System.Collections.Generic;
using System.Linq;
using TweenCustom;
using UnityEngine;

public class BoardEnemyEntity : BoardEntity
{
    protected override void InitalizeEntityBehaviour()
    {
        SetEntityBehaviour(new EnemyEntityBehaviour(this));
    }

    protected override void RegisterEntity()
    {
        GameManager.Instance.RegisterEntity(this);
    }

    public override void EntityAction()
    {
        m_EntityBehaviour.Behave();
    }
    

    protected override void Movement()
    {
        transform.DoKill();
        transform.DoMove( m_TargetMap.GetTilePosition(m_XPosition, m_YPosition),0.1f);
    }
    
    //Damage Related
    protected override void TriggerDeath()
    {
        if(m_IsDead)
            return;
        
        GameManager.Instance.UnRegisterActiveEnemy(this);
        GameManager.Instance.UnRegisterEntity(this);
        RemoveFromBoard();
        Destroy(gameObject);
        base.TriggerDeath();
    }
    public override float GetMainWeaponDamage()
    {
        return 35;
    }
}