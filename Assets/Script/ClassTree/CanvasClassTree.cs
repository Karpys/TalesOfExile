using UnityEngine;

namespace KarpysDev.Script.ClassTree
{
    public class CanvasClassTree : MonoBehaviour
    {
        [SerializeField] private Transform m_ClassContainer = null;
        [SerializeField] private ClassTreeController m_Controller = null;

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
            m_ClassContainer.localScale = Vector3.one;
            m_Controller.Initialize();
            m_IsOpen = true;
        }

        public void Close()
        {
            m_ClassContainer.localScale = Vector3.zero;
            m_IsOpen = false;
        }
    }
}