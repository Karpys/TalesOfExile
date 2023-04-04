using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButtonOptionController : SingletonMonoBehavior<ItemButtonOptionController>
{
    [SerializeField] private ItemButtonUI m_ButtonPrefab = null;
    [SerializeField] private Transform m_ButtonHolder = null;
    [SerializeField] private VerticalLayoutGroup m_Layout = null;
    [SerializeField] private AnimationCurve DisplayCurve = null;
    [SerializeField] private float m_OpenLayoutTime = 0.25f;

    private List<ItemButtonUI> m_PreviousButton = new List<ItemButtonUI>();

    private bool m_OpenLayout = false;
    private float m_OpenLayoutTimer = 0;

    private void Update()
    {
        if (m_OpenLayout)
        {
            m_Layout.spacing = Mathf.Lerp(-30, 0, DisplayCurve.Evaluate(m_OpenLayoutTimer / m_OpenLayoutTime));
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
        m_Layout.spacing = -30;
        m_OpenLayout = true;
    }

    private void StopLayoutScroll()
    {
        m_OpenLayout = false;
    }

    public void DisplayButtonOption(InventoryObject inventoryObject)
    {
        StartLayoutScroll();
        Clear();
        Place();
       
        
        List<ItemButtonUIParameters> buttonParameters = inventoryObject.ButtonRequestOptionButton();

        foreach (ItemButtonUIParameters buttonUIParameters in buttonParameters)
        {
            ItemButtonUI button = Instantiate(m_ButtonPrefab, m_ButtonHolder);
            button.InitalizeButton(inventoryObject,buttonUIParameters.OnClickAction,buttonUIParameters.ButtonText);
            m_PreviousButton.Add(button);
        }
    }

    private void Place()
    {
        transform.position = Input.mousePosition;
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
