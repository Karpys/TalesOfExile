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
            
            if (Input.GetMouseButtonDown(0))
            {
                if (m_OnMouseHolder.MouseOn && m_OnMouseHolder.Item != null)
                {
                    Debug.Log("Select item : " + m_OnMouseHolder.Item.Data.ObjectName);
                    m_OnClickHolder = m_OnMouseHolder;
                    m_DragBegin = true;
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (m_OnMouseHolder != m_OnClickHolder && m_OnMouseHolder.MouseOn)
                {
                    PerformHolderAction(m_OnClickHolder,m_OnMouseHolder);
                }
                else
                {
                    //Cancel Swap / Reset selected visual state//
                }

                m_DragBegin = false;
            }
        }
    }

    private void PerformHolderAction(ItemUIHolder holder1,ItemUIHolder holder2)
    {
        int holderActionType = (int)holder1.HolderGroup + (int)holder2.HolderGroup;

        if (holderActionType == 2)
        {
            m_PlayerInventoryUI.SwapItem(m_OnMouseHolder.Id,m_OnClickHolder.Id);
        }
        else if (holderActionType == 3)
        {
            //Player equipement to inventory context//
            ItemUIHolder inventoryHolder = holder1.HolderGroup == ItemHolderGroup.PlayerInventory ? holder1 : holder2;
            ItemUIHolder equipementHolder = holder1.HolderGroup == ItemHolderGroup.PlayerEquipement ? holder1 : holder2;
            
            if (inventoryHolder.Item is EquipementItem itemToEquip)
            {
                //Equip the item and swap position with equipement existent One//
                EquipementItemUIHolder equipementItemUIHolder = equipementHolder as EquipementItemUIHolder;

                if (!equipementItemUIHolder)
                {
                    Debug.LogError("Equipement item holder cast failed");
                    return;
                }

                if(!CanPlaceEquipement(itemToEquip,equipementItemUIHolder.EquipementType))
                    return;
                
                //Add Conditional Check like equipement type to holder type//
                itemToEquip.Equip();

                if (equipementHolder.Item is EquipementItem itemEquiped)
                {
                    itemEquiped.UnEquip();
                }
                
                m_PlayerInventoryUI.EquipementInventorySwap(inventoryHolder,equipementHolder);
            }
            else if(inventoryHolder.Item == null)
            {
                //UnEquip item and place to blank space//
                ((EquipementItem)equipementHolder.Item).UnEquip(); 
                m_PlayerInventoryUI.EquipementInventorySwap(inventoryHolder,equipementHolder);
            }
        }
        else if(holderActionType == 5)
        {
            //Player inventory to stash context// 
        }
    }

    private bool CanPlaceEquipement(EquipementItem equipementItem, EquipementType equipementType)
    {
        //Todo:Add Conditional Check for Weapon / Two handed weapon//
        return equipementItem.BaseEquipementData.EquipementType == equipementType;
    }

    public void SetCurrentMouseHolder(ItemUIHolder itemHolder)
    {
        m_OnMouseHolder = itemHolder;
    }
}
