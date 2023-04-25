using UnityEngine;

namespace TweenCustom
{
    public class TweenUIPosition: BaseTween
    {
        private RectTransform m_RectTarget = null;
        public TweenUIPosition(Transform target,Vector3 endValue,float duration)
        {
            m_Target = target;
            m_RectTarget = target as RectTransform;
            m_Duration = duration;
            m_EndValue = endValue;
            
            if(m_RectTarget)
                m_StartValue = m_RectTarget.anchoredPosition;
        }

        public override void Step()
        {
            if(base.IsDelay())return;
            base.Step();
            m_RectTarget.anchoredPosition = NewPosition();
            base.LateStep();
        }

        public Vector3 NewPosition()
        {
            Vector3 newSize = Vector3.zero;
            newSize = Vector3.LerpUnclamped(m_StartValue, m_EndValue, (float)Evaluate());
            return newSize;
        }

        public override void TweenRefreshStartValue()
        {
            m_StartValue = m_RectTarget.sizeDelta;
        }
        
        public override bool ReferenceCheck()
        {
            if (m_RectTarget == null)
                return false;
            return true;
        }
    }
}