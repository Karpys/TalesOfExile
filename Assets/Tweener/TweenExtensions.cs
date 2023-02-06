using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TweenCustom
{
    public static class TweenExtensions
    {
        // Start is called before the first frame update

        #region DoType



        public static BaseTween DoMove(this Transform trans, Vector3 endValue,float duration)
        {
            TweenMove baseTween = new TweenMove(trans, endValue,duration);
            TweenManager.Instance.AddTween(baseTween);
            return baseTween;
        }
        
        public static BaseTween DoMoveX(this Transform trans, float endValue,float duration)
        {
            TweenMoveX baseTween = new TweenMoveX(trans, endValue,duration);
            TweenManager.Instance.AddTween(baseTween);
            return baseTween;
        }
        
        public static BaseTween DoMoveY(this Transform trans, float endValue,float duration)
        {
            TweenMoveY baseTween = new TweenMoveY(trans, endValue,duration);
            TweenManager.Instance.AddTween(baseTween);
            return baseTween;
        }

        public static void DoKill(this Transform trans)
        {
            TweenManager.Instance.KillTween(trans);
        }

        public static BaseTween DoLocalMove(this Transform trans, Vector3 endValue,float duration)
        {
            TweenLocalMove baseTween = new TweenLocalMove(trans, endValue, duration);
            TweenManager.Instance.AddTween(baseTween);
            return baseTween;
        }
        
        public static BaseTween DoRotate(this Transform trans, Vector3 endValue,float duration)
        {
            TweenRotate baseTween = new TweenRotate(trans, endValue, duration);
            TweenManager.Instance.AddTween(baseTween);
            return baseTween;
        }

        /*public static BaseTween DoMove(this Transform trans, float duration, Vector3 endValue,Ease ease)
        {
            TweenMove baseTween = new TweenMove(trans, duration, endValue,ease);
            TweenManager.Instance.AddTween(baseTween);
            return baseTween;
        }*/

        #endregion

        #region DoParameters

        public static BaseTween SetEase(this BaseTween tween,Ease ease)
        {
            tween.ease = ease;
            return tween;
        }
        
        public static BaseTween SetCurve(this BaseTween tween,AnimationCurve curve)
        {
            tween.curve = curve;
            return tween;
        }

        public static BaseTween SetDelay(this BaseTween tween, float delay)
        {
            tween.delay = delay;
            return tween;
        }

        public static BaseTween SetMode(this BaseTween tween, TweenMode mode)
        {
            tween.tweenMode(mode);
            return tween;
        }

        #endregion

        #region TweenCall

        public static BaseTween OnComplete(this BaseTween tween,TweenAction action)
        {
            tween.m_onComplete = action;
            return tween;
        }
        public static BaseTween OnStart(this BaseTween tween, TweenAction action)
        {
            tween.m_onStart = action;
            return tween;
        }

        #endregion
    }
    
    public class TweenMoveX: BaseTween
    {
        public TweenMoveX(Transform target,float endValue,float duration)
        {
            m_Target = target;
            m_Duration = duration;
            m_EndValue = new Vector3(endValue,0,0);
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
            newPosition.x = Mathf.LerpUnclamped(m_StartValue.x,m_EndValue.x,(float)Evaluate());
            return newPosition;
        }

        public override void TweenRefreshStartValue()
        {
            m_StartValue = m_Target.position;
        }
    }
    
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
