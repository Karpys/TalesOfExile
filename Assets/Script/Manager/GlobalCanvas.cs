using System;
using KarpysDev.Script.UI;
using KarpysDev.Script.UI.Pointer;
using UnityEngine;

namespace KarpysDev.Script.Manager
{
    public class GlobalCanvas : SingletonMonoBehavior<GlobalCanvas>
    {
        [SerializeField] private SpellUIDisplayer m_SpellUIDisplayer = null;
        [SerializeField] private Vector2 m_ReferenceSize = Vector2.zero;
        private Vector2 m_ScreenSize = Vector2.zero;
        private UIPointer m_CurrentPointer = null;
        private UICanvasType m_CurrentCanvasType = UICanvasType.None;
    
        public UIPointer CurrentPointer => m_CurrentPointer;
        public UICanvasType CanvasType => m_CurrentCanvasType;
        public Vector2 ScreenSize => m_ScreenSize;

        private float m_WidthRatio = 0;

        private void Start()
        {
            m_ScreenSize = new Vector2(Screen.width,Screen.height);
            m_WidthRatio = m_ScreenSize.x / m_ReferenceSize.x;
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
            
            posX = Mathf.Clamp(posX, (uiSize / 2 ) * m_WidthRatio, Screen.width - uiSize / 2*m_WidthRatio);
            Transform trans = uiTransform.transform;
            trans.position = new Vector3(posX, trans.position.y);
        }

        public SpellUIDisplayer GetSpellUIDisplayer()
        {
            return m_SpellUIDisplayer;
        }
    }

    public enum UICanvasType
    {
        None,
        Inventory,
        SpellIcons,
    }
}