using TweenCustom;
using UnityEngine;

namespace KarpysDev.Script.UI.Pointer
{
    public class GoldPopupButtonPointer : ButtonPointer
    {
        [Header("Open/Close")]
        [SerializeField] private RectTransform m_RectContainer = null;
        [SerializeField] private Vector2 m_XPositionn = Vector2.zero;
        [SerializeField] private float m_AppearDuration = 0;
        [SerializeField] private AnimationCurve m_Curve = null;
        [Header("Arrow")] 
        [SerializeField] private RectTransform m_ArrowTransform = null;

        private bool m_IsOpen = false;
        public override void Trigger()
        {
            if (m_IsOpen)
            {
                Close();
            }
            else
            {
                Open();
            }

            m_IsOpen = !m_IsOpen;
        }
        private void Open()
        {
            m_RectContainer.DoUIPosition(new Vector3(m_XPositionn.x, 0, 0), m_AppearDuration).SetCurve(m_Curve);
            m_ArrowTransform.DoRotate(new Vector3(0, 0, 180), m_AppearDuration).SetCurve(m_Curve);
        }
        
        private void Close()
        {
            m_RectContainer.DoUIPosition(new Vector3(m_XPositionn.y, 0, 0), m_AppearDuration).SetCurve(m_Curve);
            m_ArrowTransform.DoRotate(new Vector3(0, 0, 0), m_AppearDuration).SetCurve(m_Curve);
        }
    }
}