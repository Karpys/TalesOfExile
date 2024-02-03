using KarpysDev.Script.Utils;
using UnityEngine;

namespace KarpysDev.Script.Spell.SpellFx
{
    using KarpysUtils.TweenCustom;

    public class Fx_HolyAttackAnimation : Fx_BurstAnimation
    {
        [SerializeField] private SpriteRenderer m_Renderer = null;
        [SerializeField] private BaseTweenData m_TweenData;
        protected override void Animate()
        {
            transform.DoScale(m_TweenData).OnComplete(() =>
            {
                m_Renderer.FadeAndDestroy(new Color(1, 1, 1, 0), 0.2f, gameObject);
            });
        }
    }
}