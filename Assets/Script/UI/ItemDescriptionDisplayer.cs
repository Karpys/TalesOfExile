using System;
using Script.UI;
using UnityEngine;

public class ItemDescriptionDisplayer : MonoBehaviour
{
    [SerializeField] private MaskScroll m_MaskScroll = null;
    [SerializeField] private float m_ScrollTime = 0.5f;
    [SerializeField] private AdaptUISize m_SelfSize = null;
    [SerializeField] private AdaptUILayoutSize m_ContainerLayout = null;

    private void Update()
    {
        transform.position = Input.mousePosition;
    }

    public virtual void Initialize(InventoryObject inventoryObject)
    {
        m_ContainerLayout.AdaptSize();
        m_SelfSize.UpdateSize();
        m_MaskScroll.LaunchScroll(((RectTransform)transform).sizeDelta.y,m_ScrollTime);
    }
}