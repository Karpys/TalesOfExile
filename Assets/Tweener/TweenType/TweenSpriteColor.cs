using UnityEngine;

namespace TweenCustom
{
    public class TweenSpriteColor: BaseTween
    {
        private SpriteRenderer m_Renderer = null;
        private Vector4 m_StartColor = new Vector4(0,0,0,1);
        private Vector4 m_EndColor = new Vector4(0,0,0,1);
        public TweenSpriteColor(SpriteRenderer target,Vector4 endValue,float duration)
        {
            // m_Target = target;
            m_Renderer = target;
            m_Duration = duration;
            m_StartColor = target.color.rgba();
            m_EndColor = endValue;
            // m_StartValue = target.color.rgb();
            // m_EndValue = endValue;
        }

        protected override void Update()
        {
            m_Renderer.color = NewColor();
        }
        
        private Color NewColor()
        {
            Vector4 newColor = Vector4.zero;
            newColor = Vector4.LerpUnclamped(m_StartColor, m_EndColor, (float)Evaluate());
            return newColor;
        }

        public override void TweenRefreshStartValue()
        {
            m_StartColor = m_Renderer.color.rgba();
        }

        public override bool ReferenceCheck()
        {
            if (m_Renderer == null)
                return false;
            return true;
        }
    }
}