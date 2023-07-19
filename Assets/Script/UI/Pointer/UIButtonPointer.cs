using System;
using KarpysDev.Script.Manager;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KarpysDev.Script.UI.Pointer
{
    public abstract class UIButtonPointer : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
    {
        private bool m_PointerUp = false;

        public bool PointerUp => m_PointerUp;

        public void OnPointerEnter(PointerEventData eventData)
        {
            m_PointerUp = true;
            ButtonUIPointerManager.Instance.SetButtonPointer(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            m_PointerUp = false;
        }

        public abstract void Trigger();
    }
}