using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButtonOptionController : SingletonMonoBehavior<ItemButtonOptionController>
{
    [SerializeField] private ItemButtonUI m_ButtonPrefab = null;
    [SerializeField] private Transform m_ButtonHolder = null;
    [SerializeField] private RectTransform m_LayoutMask = null;
    [SerializeField] private AnimationCurve DisplayCurve = null;
    [SerializeField] private float m_OpenLayoutTime = 0.25f;

    private List<ItemButtonUI> m_PreviousButton = new List<ItemButtonUI>();

    private bool m_OpenLayout = false;
    private float m_OpenLayoutTimer = 0;
    private float m_MaskLayoutTargetSize = 0;
    private Vector2 m_ButtonRectDimension = Vector2.zero;

    private void Start()
    {
        Rect buttonRect = ((RectTransform)m_ButtonPrefab.transform).rect;
        m_ButtonRectDimension = new Vector2(buttonRect.width, buttonRect.height);
    }

    private void Update()
    {
        if (m_OpenLayout)
        {
            m_LayoutMask.sizeDelta = new Vector2(75,Mathf.Lerp(0,m_MaskLayoutTargetSize , DisplayCurve.Evaluate(m_OpenLayoutTimer / m_OpenLayoutTime)));
            m_OpenLayoutTimer += Time.deltaTime;
            
            if(m_OpenLayoutTimer >= m_OpenLayoutTime)
                StopLayoutScroll();
        }
        
        if(Input.GetMouseButtonDown(1) || Input.mouseScrollDelta != Vector2.zero)
            Clear();
    }

    private void StartLayoutScroll()
    {
        m_OpenLayoutTimer = 0;
        m_LayoutMask.sizeDelta = new Vector2(75,0);
        m_OpenLayout = true;
        m_MaskLayoutTargetSize = ((RectTransform) m_ButtonPrefab.transform).rect.height * m_PreviousButton.Count;
    }

    private void StopLayoutScroll()
    {
        m_OpenLayout = false;
    }

    public void DisplayButtonOption(InventoryUIHolder inventoryUI)
    {
        Clear();
       
        
        List<ItemButtonUIParameters> buttonParameters = inventoryUI.InventoryObject.ButtonRequestOptionButton(inventoryUI);

        foreach (ItemButtonUIParameters buttonUIParameters in buttonParameters)
        {
            ItemButtonUI button = Instantiate(m_ButtonPrefab, m_ButtonHolder);
            button.InitalizeButton(buttonUIParameters.OnClickAction,buttonUIParameters.ButtonText);
            m_PreviousButton.Add(button);
        }
        
        StartLayoutScroll();
        Place((RectTransform)inventoryUI.transform);
    }

    private void Place(RectTransform inventoryUIObject)
    {
        transform.position = inventoryUIObject.position + new Vector3(inventoryUIObject.rect.width / 2, 0, 0) + new Vector3(m_ButtonRectDimension.x / 2, m_ButtonRectDimension.y / 2);
    }

    public void Clear()
    {
        int previousCount = m_PreviousButton.Count;
        for (int i = 0; i < previousCount; i++)
        {
            Destroy(m_PreviousButton[i].gameObject);
        }
        
        m_PreviousButton.Clear();
    }
}
