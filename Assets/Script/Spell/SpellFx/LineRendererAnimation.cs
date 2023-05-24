using TweenCustom;
using UnityEngine;

namespace KarpysDev.Script.Spell.SpellFx
{
    public class LineRendererAnimation : SpellAnimation
    {
        [SerializeField] private float m_AnimDuration = 0f;
        [SerializeField] private SpriteRenderer m_Renderer = null;
    
        private float m_AppearDelay = 0f;
        private float m_AppearDuration = 0f;
        private float m_LineAngle = 0f;
    
        protected override float GetAnimationDuration()
        {
            return m_AnimDuration;
        }

        public void LaunchLineRendererAnim(float appearDelay,float appearDuration,float lineAngle)
        {
            m_AppearDelay = appearDelay;
            m_AppearDuration = appearDuration;
            m_LineAngle = lineAngle;
        
            transform.eulerAngles = new Vector3(0, 0, m_LineAngle);
            Animate();
            DestroySelf(m_AppearDuration + m_AppearDelay);
        }
    
        protected override void Animate()
        {
            m_Renderer.DoColor(Color.white, 0.1f).SetDelay(m_AppearDelay);
            m_Renderer.DoColor(Color.white.setAlpha(0), 0.1f).SetDelay(m_AppearDelay + m_AppearDuration);
        }

        private void DestroySelf(float time)
        {
            Destroy(gameObject,time);
        }
    }
}