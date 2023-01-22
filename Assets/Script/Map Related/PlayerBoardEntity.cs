using TweenCustom;
using UnityEngine;

public class PlayerBoardEntity : BoardEntity
{
    [SerializeField] private Transform m_JumpTweenContainer = null;
    public override void MoveTo(int x, int y)
    {
        base.MoveTo(x, y);
        PlayMovementTween(x,y);
    }

    private void PlayMovementTween(int x,int y)
    {
        transform.DoKill();
        transform.DoMove(0.1f, m_TargetMap.GetTilePosition(x, y));
        JumpTween();
    }

    private void JumpTween()
    {
        BaseTween tween = m_JumpTweenContainer.transform.DoLocalMove(0.05f, new Vector3(0, 0.2f, 0));
        tween.m_onComplete += () => {ReleaseJumpTween();};
    }

    private void ReleaseJumpTween()
    {
        m_JumpTweenContainer.transform.DoLocalMove(0.05f, new Vector3(0, 0, 0));
    }
}