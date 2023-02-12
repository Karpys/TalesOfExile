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

        public static BaseTween DoScale(this Transform trans, Vector3 endValue, float duration)
        {
            TweenScale baseTween = new TweenScale(trans, endValue, duration);
            TweenManager.Instance.AddTween(baseTween);
            return baseTween;
        }
        
        public static BaseTween DoColor(this SpriteRenderer renderer, Color endValue,float duration)
        {
            TweenSpriteColor baseTween = new TweenSpriteColor(renderer, endValue.rgba(), duration);
            TweenManager.Instance.AddTween(baseTween);
            return baseTween;
        }
        
        public static void DoKill(this Transform trans)
        {
            TweenManager.Instance.KillTween(trans);
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
}
