using KarpysDev.Script.UI.Pointer;

namespace KarpysDev.Script.Manager
{
    public class GlobalCanvas : SingletonMonoBehavior<GlobalCanvas>
    {
        private UIPointer m_CurrentPointer = null;
        private UICanvasType m_CurrentCanvasType = UICanvasType.None;
    
        public UIPointer CurrentPointer => m_CurrentPointer;
        public UICanvasType CanvasType => m_CurrentCanvasType;
    
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
    }

    public enum UICanvasType
    {
        None,
        Inventory,
        SpellIcons,
    }
}