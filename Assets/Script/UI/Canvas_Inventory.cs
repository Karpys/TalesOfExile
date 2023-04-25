using System;
using System.Collections.Generic;
using System.Linq;
using TweenCustom;
using UnityEngine;

public class Canvas_Inventory : MonoBehaviour
{
    [SerializeField] private Transform m_InventoryContainer = null;
    [SerializeField] private ItemButtonOptionController m_ButtonOptionController = null;
    [SerializeField] private PlayerInventoryUI m_PlayerInventoryUI = null;
    
    private bool m_IsShown = false;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (m_IsShown)
            {
                Hide();
            }
            else
            {
                Show();
            }

            m_IsShown = !m_IsShown;
        }
    }
    
    private void Show()
    {
        m_InventoryContainer.transform.DoKill();
        m_InventoryContainer.gameObject.SetActive(true);
        m_InventoryContainer.transform.localScale = Vector3.zero;
        m_InventoryContainer.transform.DoScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.EASE_OUT_SIN);
        m_PlayerInventoryUI.DisplayInventory();
    }

    private void Hide()
    {
        m_InventoryContainer.transform.DoKill();
        m_InventoryContainer.transform.DoScale(Vector3.zero, 0.5f).SetEase(Ease.EASE_OUT_SIN).OnComplete(() =>
        {
            m_InventoryContainer.gameObject.SetActive(false);
        });
        
        m_ButtonOptionController.Clear();
    }
}