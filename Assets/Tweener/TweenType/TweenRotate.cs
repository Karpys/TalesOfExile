using System;
using UnityEngine;

namespace TweenCustom
{
    public class TweenRotate: BaseTween
    {
        public TweenRotate(Transform target,Vector3 endValue,float duration)
        {
            m_Target = target;
            m_Duration = duration;
            m_EndValue = endValue;
            m_StartValue = target.eulerAngles;
        }

        public override void Step()
        {
            if(base.IsDelay())return;
            base.Step();
            m_Target.eulerAngles = NewAngle();
            base.LateStep();
        }

        public Vector3 NewAngle()
        {
            Vector3 newEuleurs = Vector3.zero;
            newEuleurs = Vector3.LerpUnclamped(m_StartValue, m_EndValue, (float)Evaluate());
            return newEuleurs;
        }

        public override void TweenRefreshStartValue()
        {
            m_StartValue = m_Target.eulerAngles;
        }
    }
}

