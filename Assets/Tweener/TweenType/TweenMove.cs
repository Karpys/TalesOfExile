using UnityEngine;
namespace TweenCustom
{
    public class TweenMove: BaseTween
    {
        public TweenMove(Transform target,Vector3 endValue,float duration)
        {
            m_Target = target;
            m_Duration = duration;
            m_EndValue = endValue;
            m_StartValue = target.position;
        }

        protected override void Update()
        {
            m_Target.position = NextPosition();
        }

        private Vector3 NextPosition()
        {
            Vector3 newPosition = Vector3.zero;
            newPosition = Vector3.LerpUnclamped(m_StartValue, m_EndValue, (float)Evaluate());
            return newPosition;
        }
        
        public override void TweenRefreshStartValue()
        {
            m_StartValue = m_Target.position;
        }
    }
}

