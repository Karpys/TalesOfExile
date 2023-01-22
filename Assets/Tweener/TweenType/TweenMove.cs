using System;
using UnityEngine;
namespace TweenCustom
{
    public class TweenMove: BaseTween
    {
        public TweenMove(Transform target,float duration,Vector3 endValue)
        {
            m_Target = target;
            m_Duration = duration;
            m_EndValue = endValue;
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

