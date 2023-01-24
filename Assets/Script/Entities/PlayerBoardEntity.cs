using TweenCustom;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerBoardEntity : BoardEntity
{
    [SerializeField] private Transform m_JumpTweenContainer = null;
    [SerializeField] private float m_MovementDuration = 0.1f;
    
    protected override void Movement()
    {
        JumpAnimation();
    }

    void JumpAnimation()
    {
        transform.DoKill();
        transform.DoMove(m_MovementDuration, m_TargetMap.GetTilePosition(m_XPosition, m_YPosition));
        JumpTween();
    }

    private void JumpTween()
    {
        BaseTween tween = m_JumpTweenContainer.transform.DoLocalMove(m_MovementDuration / 2f, new Vector3(0, 0.2f, 0));
        tween.m_onComplete += () => {ReleaseJumpTween();};
    }

    private void ReleaseJumpTween()
    {
        m_JumpTweenContainer.transform.DoLocalMove(m_MovementDuration/2f, new Vector3(0, 0, 0));
    }
}