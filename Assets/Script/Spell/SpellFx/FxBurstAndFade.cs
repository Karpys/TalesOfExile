using KarpysDev.Script.Utils;
using UnityEngine;

namespace KarpysDev.Script.Spell.SpellFx
{
    using KarpysUtils.TweenCustom;

    public class FxBurstAndFade : FxBurstAnimation
    {
        [Header("Fade Parameter")]
        [SerializeField] protected SpriteRenderer m_Visual = null;
        [SerializeField] protected float m_FadeDelay = 0.2f;
        [SerializeField] protected Color m_FadeColor;
        [SerializeField] protected float m_FadeDuration = 0.2f;
        protected override void Animate()
        {
            m_Visual.FadeAndDestroy(m_FadeColor,m_FadeDuration,gameObject).SetDelay(m_FadeDelay);
        }
    }
}