using UnityEngine;

namespace TweenCustom
{
    public class TweenMoveY: BaseTween
    {
        public TweenMoveY(Transform target,float endValue,float duration)
        {
            m_Target = target;
            m_Duration = duration;
            m_EndValue = new Vector3(0,endValue,0);
            m_StartValue = target.position;
        }

        public override void Step()
        {
            if(base.IsDelay())return;
            base.Step();
            m_Target.position = NextPosition();
            base.LateStep();
        }

        public Vector3 NextPosition()
        {
            Vector3 newPosition = m_Target.transform.position;
            newPosition.y = Mathf.LerpUnclamped(m_StartValue.y,m_EndValue.y,(float)Evaluate());
            return newPosition;
        }

        public override void TweenRefreshStartValue()
        {
            m_StartValue = m_Target.position;
        }
    }
}