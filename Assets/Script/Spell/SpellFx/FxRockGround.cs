using KarpysDev.Script.Utils;
using UnityEngine;

namespace KarpysDev.Script.Spell.SpellFx
{
    using KarpysUtils;
    using KarpysUtils.TweenCustom;

    public class FxRockGround : FxBurstAnimation
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
            m_ScaleParams.TargetTransform.DoScale(m_ScaleParams).SetEase(Ease.EASE_OUT_SIN).OnComplete(() =>
            {
                m_Sprite.FadeAndDestroy(new Color(1,1,1,0),m_FadeDuration,gameObject);
            });
        }
    }
}