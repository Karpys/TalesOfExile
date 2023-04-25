using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUIController : SingletonMonoBehavior<ItemUIController>
{
    [SerializeField] private PlayerInventoryUI m_PlayerInventoryUI = null;
    private ItemUIHolder m_OnClickHolder = null;
    private ItemUIHolder m_OnMouseHolder = null;

    private bool m_DragBegin = false;
    private void Update()
    {
        if (!m_DragBegin)
        {
            if(m_OnMouseHolder == null)
                return;
            
            if (Input.GetMouseButtonDown(0) && m_OnMouseHolder.MouseOn && m_OnMouseHolder.Item != null)
            {
                Debug.Log("Select item : " + m_OnMouseHolder.Item.Data.ObjectName);
                m_OnClickHolder = m_OnMouseHolder;
                m_DragBegin = true;
            }
        }
        else
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (m_OnMouseHolder != m_OnClickHolder && m_OnMouseHolder.MouseOn)
                {
                    m_PlayerInventoryUI.SwapItem(m_OnMouseHolder.Id,m_OnClickHolder.Id);
                }
                else
                {
                    //Cancel Swap / Reset selected visual state//
                }

                m_DragBegin = false;
            }
        }
    }

    public void SetCurrentMouseHolder(ItemUIHolder itemHolder)
    {
        m_OnMouseHolder = itemHolder;
    }
}
