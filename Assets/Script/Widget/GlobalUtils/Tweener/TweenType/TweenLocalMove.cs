using UnityEngine;
namespace TweenCustom
{
    public class TweenLocalMove : BaseTween
    {
        public TweenLocalMove(Transform target, Vector3 endValue,float duration)
        {
            m_Target = target;
            m_Duration = duration;
            m_EndValue = endValue;
            m_StartValue = target.localPosition;
        }

        protected override void Update()
        {
            m_Target.localPosition = NextPosition();
        }

        private Vector3 NextPosition()
        {
            Vector3 newPosition = Vector3.zero;
            newPosition = Vector3.LerpUnclamped(m_StartValue, m_EndValue, (float)Evaluate());
            return newPosition;
        }

        public override void TweenRefreshStartValue()
        {
            m_StartValue = m_Target.localPosition;
        }
    }
}


