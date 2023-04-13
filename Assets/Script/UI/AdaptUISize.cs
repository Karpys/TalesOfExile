﻿using Script.UI;
using UnityEngine;


public class AdaptUISize : MonoBehaviour
{
    [SerializeField] private bool m_AdaptOnStart = true;
    [SerializeField] private LayoutType m_AlignementType = LayoutType.Both;
    [SerializeField] private RectTransform m_RectTransform = null;
    [SerializeField] private float m_AdditionalSize = 0f;

    private void Start()
    {
        if(m_AdaptOnStart)
            UpdateSize();
    }

    public void UpdateSize()
    {
        RectTransform self = GetComponent<RectTransform>();

        switch (m_AlignementType)
        {
            case LayoutType.Both:
                self.sizeDelta = m_RectTransform.sizeDelta + new Vector2(m_AdditionalSize,m_AdditionalSize);
                break;
            case LayoutType.Vertical:
                self.sizeDelta = new Vector2(self.sizeDelta.x , m_RectTransform.sizeDelta.y + m_AdditionalSize);
                break;
            case LayoutType.Horizontal:
                self.sizeDelta = new Vector2(m_RectTransform.sizeDelta.x + m_AdditionalSize,self.sizeDelta.y);
                break;
        }
    }
}