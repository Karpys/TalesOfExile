using System;
using UnityEngine;
namespace TweenCustom
{
    public class TweenLocalMove : BaseTween
    {
        public TweenLocalMove(Transform target, float duration, Vector3 endValue)
        {
            m_Target = target;
            m_Duration = duration;
            m_EndValue = endValue;
            m_StartValue = target.localPosition;
        }

        public override void Step()
        {
            if (base.IsDelay()) return;
            base.Step();
            m_Target.localPosition = NextPosition();
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
            m_StartValue = m_Target.localPosition;
        }
    }
}


