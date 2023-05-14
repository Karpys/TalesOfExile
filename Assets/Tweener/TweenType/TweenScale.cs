using UnityEngine;

namespace TweenCustom
{
    public class TweenScale: BaseTween
    {
        public TweenScale(Transform target,Vector3 endValue,float duration)
        {
            m_Target = target;
            m_Duration = duration;
            m_EndValue = endValue;
            m_StartValue = target.localScale;
        }

        protected override void Update()
        {
            m_Target.localScale = NewScale();
        }
        
        private Vector3 NewScale()
        {
            Vector3 newScale = Vector3.zero;
            newScale = Vector3.LerpUnclamped(m_StartValue, m_EndValue, (float)Evaluate());
            return newScale;
        }

        public override void TweenRefreshStartValue()
        {
            m_StartValue = m_Target.localScale;
        }
    }
}