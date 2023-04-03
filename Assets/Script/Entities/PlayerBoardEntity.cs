using System;
using TweenCustom;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerBoardEntity : BoardEntity
{
    [Header("Player")]
    [SerializeField] private PlayerInventory m_PlayerInventory = null;
    [SerializeField] private Transform m_JumpTweenContainer = null;
    [SerializeField] private float m_MovementDuration = 0.1f;

    public PlayerInventory PlayerInventory => m_PlayerInventory;
    protected override void InitalizeEntityBehaviour()
    {
        SetEntityBehaviour(new PLayerAutoPlayEntity(this));
    }

    protected override void RegisterEntity()
    {
        GameManager.Instance.RegisterEntity(this);
        GameManager.Instance.RegisterPlayer(this);
        GameManager.Instance.SetControlledEntity(this);
    }
    
    public override void EntityAction()
    {
        m_EntityBehaviour.Behave();
    }

    protected override void Movement()
    {
        JumpAnimation();
    }

    protected override void TriggerDeath()
    {
        //Trigger Lose ?//
        return;
    }

    void JumpAnimation()
    {
        transform.DoKill();
        transform.DoMove( m_TargetMap.GetTilePosition(m_XPosition, m_YPosition),m_MovementDuration);
        JumpTween();
    }

    private void JumpTween()
    {
        m_JumpTweenContainer.transform.DoLocalMove(new Vector3(0, 0.2f, 0), m_MovementDuration / 2f).OnComplete(ReleaseJumpTween);
    }

    private void ReleaseJumpTween()
    {
        m_JumpTweenContainer.transform.DoLocalMove(new Vector3(0, 0, 0), m_MovementDuration / 2f);
    }
}