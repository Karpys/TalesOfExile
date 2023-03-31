using System.Collections.Generic;
using System.Linq;
using TweenCustom;
using UnityEngine;

public class BoardEnemyEntity : BoardEntity
{
    protected override void Awake()
    {
        base.Awake();
        SetEntityBehaviour(new EnemyEntityBehaviour(this));
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
        
        base.TriggerDeath();
        GameManager.Instance.UnRegisterActiveEnemy(this);
        GameManager.Instance.UnRegisterEntity(this);
        RemoveFromBoard();
        Destroy(gameObject);
    }
    

    public override float GetMainWeaponDamage()
    {
        return 35;
    }
}