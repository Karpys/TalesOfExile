using UnityEngine;

namespace KarpysDev.Script.UI
{
    public class ViewportAdaptativeSize : MonoBehaviour
    {
        [SerializeField] private float m_MinSize = 0;
        [SerializeField] private Transform m_TargetContainer = null;
        [SerializeField] private RectTransform m_ContentSize = null;

        public void AdaptSize()
        {
            float contentSize = 0;

            for (int i = 0; i < m_TargetContainer.childCount; i++)
            {
                RectTransform rectTransform = (RectTransform) m_TargetContainer.GetChild(i).transform;
                if(rectTransform.gameObject.activeSelf)
                    contentSize += rectTransform.rect.height;
            }
        
            m_ContentSize.sizeDelta = new Vector2 (m_ContentSize.sizeDelta.x,Mathf.Max(contentSize,m_MinSize));
        }
    }
}
