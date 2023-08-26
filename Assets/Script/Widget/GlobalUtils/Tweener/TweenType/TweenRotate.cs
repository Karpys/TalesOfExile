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

        protected override void Update()
        {
            m_Target.eulerAngles = NewAngle();
        }

        private Vector3 NewAngle()
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

