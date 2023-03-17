using System;
using TweenCustom;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerBoardEntity : BoardEntity
{
    [Header("Player")]

    [SerializeField] private Transform m_JumpTweenContainer = null;
    [SerializeField] private float m_MovementDuration = 0.1f;

    protected override void Start()
    {
        base.Start();
        GameManager.Instance.RegisterPlayer(this);
        GameManager.Instance.SetControlledEntity(this);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            EquipementUtils.Unequip(m_Equipement.EquipementSockets[9],this);
        }
    }

    public override void EntityAction()
    {
        ReduceAllCooldown();
        //TODO:Act as a Base IA/
        return;
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
        BaseTween tween = m_JumpTweenContainer.transform.DoLocalMove(new Vector3(0, 0.2f, 0), m_MovementDuration / 2f);
        tween.m_onComplete += () => {ReleaseJumpTween();};
    }

    private void ReleaseJumpTween()
    {
        m_JumpTweenContainer.transform.DoLocalMove(new Vector3(0, 0, 0), m_MovementDuration / 2f);
    }
}