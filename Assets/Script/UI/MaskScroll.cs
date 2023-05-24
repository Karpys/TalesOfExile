using UnityEngine;

namespace KarpysDev.Script.UI
{
    public class MaskScroll : MonoBehaviour
    {
        [SerializeField] private RectTransform m_TargetTransform = null;
        [SerializeField] private AnimationCurve m_DisplayCurve = null;

        private bool m_OnScroll = false;
        private float m_TargetSize = 0;

        private float m_OpenScrollTimer = 0;
        private float m_OpenScrollTime = 0;

        private void Update()
        {
            if (m_OnScroll)
            {
                if (m_OpenScrollTime == 0)
                {
                    m_TargetTransform.sizeDelta = new Vector2(m_TargetTransform.sizeDelta.x, m_TargetSize);
                    m_OnScroll = false;
                    return;
                }
            
                m_TargetTransform.sizeDelta = new Vector2(m_TargetTransform.sizeDelta.x,Mathf.Lerp(0,m_TargetSize , m_DisplayCurve.Evaluate(m_OpenScrollTimer / m_OpenScrollTime)));
                m_OpenScrollTimer += Time.deltaTime;

                if (m_OpenScrollTimer >= m_OpenScrollTime)
                {
                    m_OnScroll = false;
                }
            }
        }

        public void LaunchScroll(float targetSize,float openTime)
        {
            m_OpenScrollTimer = 0;
            m_OpenScrollTime = openTime;
            m_TargetTransform.sizeDelta = new Vector2(m_TargetTransform.sizeDelta.x,0);
            m_OnScroll = true;
            m_TargetSize = targetSize;
        }
    }
}
