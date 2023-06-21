using KarpysDev.Script.Utils;
using TweenCustom;
using UnityEngine;

namespace KarpysDev.Script.Spell.SpellFx
{
    public class BurstAndFade : BurstAnimation
    {
        [SerializeField] private float m_FadeDelay = 0.2f;
        [SerializeField] private SpriteRenderer m_Visual = null;
        [SerializeField] private Color m_FadeColor;
        [SerializeField] private float m_FadeDuration = 0.2f;
        protected override void Animate()
        {
            m_Visual.FadeAndDestroy(m_FadeColor,m_FadeDuration,gameObject).SetDelay(m_FadeDelay);
        }
    }
}