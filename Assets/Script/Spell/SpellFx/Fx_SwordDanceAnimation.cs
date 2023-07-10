using KarpysDev.Script.Utils;
using TweenCustom;
using UnityEngine;

namespace KarpysDev.Script.Spell.SpellFx
{
    public class Fx_SwordDanceAnimation : Fx_BurstAnimation
    {
        [SerializeField] private Transform m_RotationContainer = null;
        [SerializeField] private float m_RotationForce = 0;
        [SerializeField] private float m_RotationDuration = 0;
        [SerializeField] private AnimationCurve m_RotationCurve = null;
        [SerializeField] private SpriteRenderer[] m_Renderers = null;
        protected override void Animate()
        {
            m_RotationContainer.DoRotate(new Vector3(0, 0, m_RotationForce), m_RotationDuration).SetCurve(m_RotationCurve).
                OnComplete(FadeOut);
        }

        private void FadeOut()
        {
            foreach (SpriteRenderer r in m_Renderers)
            {
                r.FadeAndDestroy(new Color(0, 0, 0, 0), 0.2f, gameObject);
            }
        }
    }
}