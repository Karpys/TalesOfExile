using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TweenCustom
{
    public class TweenManager : SingletonMonoBehavior<TweenManager>
    {
        public List<BaseTween> m_Tweeners = new List<BaseTween>();

        private void FixedUpdate()
        {
            for (int i = 0; i < m_Tweeners.Count; i++)
            {
                m_Tweeners[i].Step();
            }
        }

        public void AddTween(BaseTween tween)
        {
            m_Tweeners.Add(tween);
        }

        public void RemoveTween(BaseTween tween)
        {
            m_Tweeners.Remove(tween);
        }

        public void KillTween(Transform trans)
        {
            for (int i = 0; i < m_Tweeners.Count; i++)
            {
                if (m_Tweeners[i].Target == trans)
                {
                    RemoveTween(m_Tweeners[i]);
                }
            }
        }
    }
}