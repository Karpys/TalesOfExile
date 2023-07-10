using System;
using KarpysDev.Script.UI.Pointer;
using UnityEngine;

namespace KarpysDev.Script.Manager
{
    public class ButtonUIPointerManager : SingletonMonoBehavior<ButtonUIPointerManager>
    {
        private ButtonPointer m_CurrentPointer = null;

        public void SetButtonPointer(ButtonPointer button)
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