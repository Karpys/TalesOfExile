using System.Collections.Generic;
using System.Linq;
using TweenCustom;
using UnityEngine;

public class BoardEnemyEntity : BoardEntity
{
    [SerializeField] private int m_Range = 1;

    private EnemyEntityBehaviour Behaviour => m_EntityBehaviour as EnemyEntityBehaviour;

    public int Range => m_Range;

    protected override void Awake()
    {
        base.Awake();
        SetEntityBehaviour(new EnemyEntityBehaviour(this));
    }
    protected override void Start()
    {
        base.Start();
        if (Behaviour != null)
        {
            Behaviour.ComputeSpellPriority();
            Behaviour.SelfBuffCount();
        }
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
        GameManager.Instance.UnRegisterEntity(this);
        RemoveFromBoard();
        Destroy(gameObject);
    }
    

    public override float GetMainWeaponDamage()
    {
        return 35;
    }
}