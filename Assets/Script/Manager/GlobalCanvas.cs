using System;
using KarpysDev.Script.UI.Pointer;
using UnityEngine;

namespace KarpysDev.Script.Manager
{
    public class GlobalCanvas : SingletonMonoBehavior<GlobalCanvas>
    {
        private Vector2 m_ScreenSize = Vector2.zero;
        private UIPointer m_CurrentPointer = null;
        private UICanvasType m_CurrentCanvasType = UICanvasType.None;
    
        public UIPointer CurrentPointer => m_CurrentPointer;
        public UICanvasType CanvasType => m_CurrentCanvasType;
        public Vector2 ScreenSize => m_ScreenSize;

        private void Start()
        {
            m_ScreenSize = new Vector2(Screen.width,Screen.height);
        }

        public void SetCanvasPointer(UIPointer pointer, UICanvasType canvasType)
        {
            m_CurrentPointer = pointer;
            m_CurrentCanvasType = canvasType;
        }

        public bool IsOnCanvas(UICanvasType canvasType)
        {
            if (canvasType == m_CurrentCanvasType && m_CurrentPointer.PointerUp)
                return true;

            return false;
        }

        //Todo : Add Pivot type => Left / Middle / Right / Custom ?

        public void ClampX(RectTransform uiTransform)
        {
            float uiSize = uiTransform.rect.width;
            float posX = uiTransform.position.x;
            
            posX = Mathf.Clamp(posX, uiSize / 2, Screen.width - uiSize / 2);
            Transform trans = uiTransform.transform;
            trans.position = new Vector3(posX, trans.position.y);
        }
    }

    public enum UICanvasType
    {
        None,
        Inventory,
        SpellIcons,
    }
}