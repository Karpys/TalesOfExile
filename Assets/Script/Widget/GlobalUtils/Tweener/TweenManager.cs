using System.Collections.Generic;
using UnityEngine;

namespace TweenCustom
{
    public class TweenManager : SingletonMonoBehavior<TweenManager>
    {
        public LinkedList<BaseTween> m_Tweeners = new LinkedList<BaseTween>();
        //public List<BaseTween> m_Tweeners = new List<BaseTween>();

        private void FixedUpdate()
        {
            LinkedListNode<BaseTween> current = m_Tweeners.First;
            while (current != null)
            {
                current.Value.Step();
                if (current.Value.IsComplete)
                {
                    LinkedListNode<BaseTween> next = current.Next;
                    m_Tweeners.Remove(current);
                    current = next;
                }
                else
                {
                    current = current.Next;
                }
            }
        }

        public void AddTween(BaseTween tween)
        {
            m_Tweeners.AddLast(tween);
        }

        public void RemoveTween(BaseTween tween)
        {
            m_Tweeners.Remove(tween);
        }

        public void KillTween(Transform trans)
        {
            LinkedListNode<BaseTween> current = m_Tweeners.First;
            while (current != null)
            {
                if (current.Value.Target == trans)
                {
                    LinkedListNode<BaseTween> next = current.Next;
                    m_Tweeners.Remove(current);
                    current = next;
                }
                else
                {
                    current = current.Next;
                }
            }
        }
    }
}