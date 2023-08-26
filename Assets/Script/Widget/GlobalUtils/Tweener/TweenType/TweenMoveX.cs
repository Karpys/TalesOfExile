using UnityEngine;

namespace TweenCustom
{
    public class TweenMoveX: BaseTween
    {
        public TweenMoveX(Transform target,float endValue,float duration)
        {
            m_Target = target;
            m_Duration = duration;
            m_EndValue = new Vector3(endValue,0,0);
            m_StartValue = target.position;
        }

        protected override void Update()
        {
            m_Target.position = NextPosition();
        }

        private Vector3 NextPosition()
        {
            Vector3 newPosition = m_Target.transform.position;
            newPosition.x = Mathf.LerpUnclamped(m_StartValue.x,m_EndValue.x,(float)Evaluate());
            return newPosition;
        }

        public override void TweenRefreshStartValue()
        {
            m_StartValue = m_Target.position;
        }
    }
}