using TweenCustom;
using UnityEngine;

public class Fx_RockGround : BurstAnimation
{
    [SerializeField] protected BaseTweenData m_ScaleParams;
    [SerializeField] protected SpriteRenderer m_Sprite = null;
    [SerializeField] protected float m_FadeDuration = 0.2f;

    protected override float GetAnimationDuration()
    {
        return base.GetAnimationDuration() + m_ScaleParams.Duration;
    }

    protected override void Animate()
    {
        m_ScaleParams.TargetTransform.DoScale(m_ScaleParams).SetEase(Ease.EASE_OUT_SIN);
        base.Animate();
    }

    protected override void DestroySelf(float time)
    {
        m_Sprite.DoColor(new Color(1, 1, 1, 0), m_FadeDuration).SetDelay(time);
        Destroy(gameObject,m_FadeDuration + time + 0.1f);
    }
}