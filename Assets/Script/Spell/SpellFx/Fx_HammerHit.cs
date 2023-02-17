using TweenCustom;
using UnityEngine;

public class Fx_HammerHit : BurstAnimation
{
    [SerializeField] private SpriteRenderer m_Sprite = null;
    [SerializeField] private BaseTweenData m_RotateTween = null;
    [SerializeField] private float m_FadeDuration = 0.2f;
    protected override float GetAnimationDuration()
    {
        return m_RotateTween.Duration;
    }
    public override void Animate()
    {
        transform.DoRotate(m_RotateTween).SetMode(TweenMode.ADDITIVE);
    }

    public override void DestroySelf(float time)
    {
        m_Sprite.DoColor(new Color(1, 1, 1, 0), m_FadeDuration).SetDelay(time);
        Destroy(gameObject,time + m_FadeDuration + 0.1f);
    }
}