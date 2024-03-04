using KarpysDev.Script.Utils;
using UnityEngine;

namespace KarpysDev.Script.Spell.SpellFx
{
    using KarpysUtils;
    using KarpysUtils.TweenCustom;

    public class FxScaleFade : FxBurstAnimation
    {
        [SerializeField] private SpriteRenderer m_Renderer = null;
        [SerializeField] private float m_FadeDuration = 0.2f;
        [SerializeField] private Ease m_Ease = Ease.LINEAR;
        [SerializeField] protected BaseTweenData m_TweenData;
        protected override void Animate()
        {
            transform.DoScale(m_TweenData).OnComplete(() =>
            {
                m_Renderer.FadeAndDestroy(new Color(1, 1, 1, 0), m_FadeDuration, gameObject);
            }).SetEase(m_Ease);
        }
    }
}