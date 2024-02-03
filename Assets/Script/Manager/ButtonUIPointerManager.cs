using System;
using KarpysDev.Script.UI.Pointer;
using UnityEngine;

namespace KarpysDev.Script.Manager
{
    using KarpysUtils;

    public class ButtonUIPointerManager : SingletonMonoBehavior<ButtonUIPointerManager>
    {
        private UIButtonPointer m_CurrentPointer = null;

        public void SetButtonPointer(UIButtonPointer button)
        {
            m_CurrentPointer = button;
        }
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(m_CurrentPointer != null && m_CurrentPointer.PointerUp)
                    m_CurrentPointer.Trigger();
            }
        }
    }
}