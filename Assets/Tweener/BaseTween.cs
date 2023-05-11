using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TweenCustom
{
    [Serializable]
    public abstract class BaseTween
    {
        //Set Values//
        protected Transform m_Target = null;
        protected float m_Duration = 1; protected Vector3 m_EndValue = Vector3.zero;
        protected Ease m_Ease = Ease.LINEAR;
        protected AnimationCurve m_AnimCurve = null;
        protected float m_Delay = 0; 
        protected TweenMode m_Mode = TweenMode.NORMAL;
        
        //Action//
        public TweenAction m_onComplete = null;
        public TweenAction m_onStart = null;

        //Static Values//
        protected Vector3 m_StartValue = Vector3.zero;
        protected float m_Ratio = 0;
        protected float m_Timer = 0;
        protected bool m_HastStart = false;
        
        //Getter//
        public Transform Target => m_Target;
        //Setter//
        public Ease ease
        {
            set => m_Ease = value;
        }

        public float delay
        {
            set => m_Delay = value;
        }

        public AnimationCurve curve
        {
            set => m_AnimCurve = value;
        }

        
        public BaseTween()
        {
        }

        public virtual bool IsDelay()
        {
            if (m_HastStart) return false;

            m_Delay -= Time.deltaTime;
            if (m_Delay < 0)
            {
                m_HastStart = true;
                TweenStart();
            }
            return true;
        }

        private void TweenStart()
        {
            m_onStart?.Invoke();
        }

        public virtual void Step()
        {
            UpdateTimerAndRatio();
        }

        protected void UpdateTimerAndRatio()
        {
            m_Timer += Time.deltaTime;
            m_Ratio = m_Timer / m_Duration;
            if (m_Ratio > 1)
            {
                m_Ratio = 1;
            }
        }

        private protected void LateStep()
        {
            CheckForDestroy();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public virtual bool ReferenceCheck()
        {
            if (m_Target == null)
                return false;
            return true;
        }
        
        protected void CheckForDestroy()
        {
            if (m_Ratio >= 1)
            {
                Complete();
                RemoveTween();
            }
        }

        private void Complete()
        {
            m_onComplete?.Invoke();
        }

        protected void RemoveTween()
        {
            TweenManager.Instance.RemoveTween(this);
        }

        //Ease Evaluate
        protected double Evaluate()
        {
            if (m_AnimCurve != null)
            {
                return m_AnimCurve.Evaluate(m_Ratio);
            }
            
            switch (m_Ease)
            {
                case Ease.LINEAR:
                    return m_Ratio;
                case Ease.EASE_IN_SIN:
                    return 1 - Math.Cos((m_Ratio * Math.PI) / 2);
                case Ease.EASE_OUT_SIN:
                    return Math.Sin((m_Ratio * Math.PI) / 2);
                case Ease.EASE_OUT_ELASTIC:
                    double c4 = (2 * Math.PI) / 3;
                    if (m_Ratio == 1)
                        return 1;
                    return Math.Pow(2, -10 * m_Ratio) * Math.Sin((m_Ratio * 10 - 0.75) * c4) + 1;
                default:
                    return 0;
            }
            
        }

        //Set Tween Mode Parameters
        //TODO:Make this method virtual : ex : this wont work with DoColor//
        public void tweenMode(TweenMode mode)
        {
            switch (mode)
            {
                case TweenMode.ADDITIVE:
                    m_EndValue = m_StartValue + m_EndValue;
                    return;
                default:
                    return;
            }
        }

        //TweenUtils//

        public abstract void TweenRefreshStartValue();
    }
}
