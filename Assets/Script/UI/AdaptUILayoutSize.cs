using UnityEngine;
using UnityEngine.UI;

namespace KarpysDev.Script.UI
{
    using System;

    public enum LayoutType
    {
        Vertical,
        Horizontal,
        Both,
    }
    public class AdaptUILayoutSize : MonoBehaviour
    {
        [SerializeField] private bool m_AdaptOnStart = true;
        [SerializeField] private LayoutGroup m_LayoutGroup = null;
        [SerializeField] private LayoutType m_Type = LayoutType.Vertical;
        [SerializeField] private float m_Spacing = 0;
        [SerializeField] private float m_AdditionalSize = 0;

        private RectTransform m_Content = null;

        private void Awake()
        {
            m_Content = GetComponent<RectTransform>();
        }

        private void Start()
        {
            if(m_AdaptOnStart)
                AdaptSize();
        }

        private void OnValidate()
        {
            m_Content = GetComponent<RectTransform>();
            AdaptSize();
        }

        public void AdaptSize()
        {
            if (m_Type == LayoutType.Vertical)
            {
                float newHeight = 0;
                for (int i = 0; i < m_LayoutGroup.transform.childCount; i++)
                {
                    newHeight += m_LayoutGroup.transform.GetChild(i).GetComponent<RectTransform>().sizeDelta.y + m_Spacing;
                }

                m_Content.sizeDelta = new Vector2(m_Content.rect.width, newHeight + m_AdditionalSize);
            }
            else if(m_Type == LayoutType.Horizontal)
            {
                float newWidth = 0;
                for (int i = 0; i < m_LayoutGroup.transform.childCount; i++)
                {
                    newWidth += m_LayoutGroup.transform.GetChild(i).GetComponent<RectTransform>().sizeDelta.x + m_Spacing;
                }

                m_Content.sizeDelta = new Vector2(newWidth + m_AdditionalSize,m_Content.rect.height);
            }
        }
    }
}