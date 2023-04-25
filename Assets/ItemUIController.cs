using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUIController : SingletonMonoBehavior<ItemUIController>
{
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
                    SwapHolder(m_OnMouseHolder,m_OnClickHolder);
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
    
    private void SwapHolder(ItemUIHolder holder1, ItemUIHolder holder2)
    {
        Item itemTemp = holder1.Item;
        holder1.SetItem(holder2.Item);
        holder2.SetItem(itemTemp);
    }
}
