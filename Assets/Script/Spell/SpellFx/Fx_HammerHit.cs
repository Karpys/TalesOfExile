using TweenCustom;
using UnityEngine;

namespace KarpysDev.Script.Spell.SpellFx
{
    public class Fx_HammerHit : BurstAnimation
    {
        [SerializeField] private SpriteRenderer m_Sprite = null;
        [SerializeField] private BaseTweenData m_ScaleTween;
        [SerializeField] private float m_FadeDuration = 0.2f;
        protected override float GetAnimationDuration()
        {
            return m_ScaleTween.Duration * 2;
        }
        protected override void Animate()
        {
            m_ScaleTween.TargetTransform.DoScale(m_ScaleTween).OnComplete((() => m_ScaleTween.TargetTransform.DoScale(new Vector3(1,1,1),m_ScaleTween.Duration)));
        }

        protected override void DestroySelf(float time)
        {
            m_Sprite.DoColor(new Color(1, 1, 1, 0), m_FadeDuration).SetDelay(time);
            Destroy(gameObject,time + m_FadeDuration + 0.1f);
        }
    }
}