using System.Collections;
using KarpysDev.KarpysUtils.TweenCustom;
using UnityEngine;
using UnityEngine.UI;

namespace KarpysDev.Script.ClassTree
{
    public class CanvasClassTree : MonoBehaviour
    {
        [SerializeField] private Transform m_ClassContainer = null;
        [SerializeField] private ClassTreeController m_Controller = null;
        [SerializeField] private float m_OpenTime = 0f;
        [SerializeField] private ScrollRect m_ScrollRect = null;
        [SerializeField] private RectTransform m_SpellTreeContainer = null;

        private bool m_IsOpen = false;
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (!m_IsOpen)
                {
                    Open();
                }
                else
                {
                    Close();
                }
            }
        }

        private void Open()
        {
            m_ClassContainer.DoScale(Vector3.one,m_OpenTime).SetEase(Ease.EASE_OUT_BACK).OnComplete(() => m_ScrollRect.enabled = true);
            m_Controller.Initialize();
            m_IsOpen = true;
        }

        public void Close()
        {
            m_ClassContainer.DoScale(Vector3.zero,m_OpenTime).SetEase(Ease.EASE_IN_BACK);
            m_ScrollRect.enabled = false;
            m_IsOpen = false;
        }
    }
}