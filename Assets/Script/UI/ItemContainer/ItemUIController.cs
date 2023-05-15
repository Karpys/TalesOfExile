using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUIController : SingletonMonoBehavior<ItemUIController>
{
    [SerializeField] private PlayerInventoryUI m_PlayerInventoryUI = null;
    [SerializeField] private ItemFade m_ItemFade = null;
    private ItemUIHolder m_OnClickHolder = null;
    private ItemUIHolder m_OnMouseHolder = null;

    private bool m_DragBegin = false;
    public ItemUIHolder OnMouseHolder => m_OnMouseHolder;
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
                    m_ItemFade.Initialize(m_OnClickHolder.Item.Data.InUIVisual);
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
                else if (m_OnClickHolder.MouseOn)
                {
                    //Perform Same Holder Action
                }
                else
                {
                    PerformSingleHolderAction(m_OnClickHolder);
                }

                m_DragBegin = false;
                m_ItemFade.Clear();
            }
        }
    }


    private void PerformSingleHolderAction(ItemUIHolder holder)
    {
        int holderActionType = (int) holder.HolderGroup;
        
        if (holderActionType == 1)
        {
            m_PlayerInventoryUI.DropInventoryItem(m_OnClickHolder);
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
                
                if (itemToEquip.EquipementData.EquipementType == EquipementType.Weapon)
                {
                    WeaponEquipement( equipementItemUIHolder,itemToEquip,inventoryHolder);
                }
                else
                {
                    NonEquipement(itemToEquip, equipementHolder,inventoryHolder);
                }
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

    private void NonEquipement(EquipementItem itemToEquip,ItemUIHolder equipementHolder,ItemUIHolder inventoryHolder)
    {
        itemToEquip.Equip();

        if (equipementHolder.Item is EquipementItem itemEquiped)
        {
            itemEquiped.UnEquip();
        }
                
        m_PlayerInventoryUI.EquipementInventorySwap(inventoryHolder,equipementHolder);
    }

    private void WeaponEquipement(EquipementItemUIHolder equipementItemUIHolder, EquipementItem itemToEquip,ItemUIHolder inventoryHolder)
    {
        WeaponEquipementItemdata weaponData = itemToEquip.Data as WeaponEquipementItemdata;
        WeaponEquipementUIHolder weaponHolder = equipementItemUIHolder as WeaponEquipementUIHolder;

        if (weaponData.TwoHanded)
        {
            EquipementItemUIHolder[] weaponHolders = weaponHolder.GetWeaponEquiped();
            ItemUIHolder[] freeHolder = GetFreeHolderInPlayerInventory();
            
            if (weaponHolders.Length - 1 > freeHolder.Length)
                return;
            
            for (int i = 0; i < weaponHolders.Length; i++)
            {
                weaponHolders[i].EquipementItem.UnEquip();
            }

            itemToEquip.Equip();
            m_PlayerInventoryUI.EquipementInventorySwap(inventoryHolder,weaponHolder.GetMain());
            
            if (weaponHolders.Length == 2 || weaponHolder.GetSub().Item != null)
            {
                m_PlayerInventoryUI.EquipementInventorySwap(freeHolder[0],weaponHolder.GetSub());
            }
        }
        else
        {
            //Check de two handed//
            if (weaponHolder.IsTwoHandedCurrentlyEquiped())
            {
                //send back two handed and place this
                weaponHolder = weaponHolder.GetMain();
                
            }
            
            itemToEquip.Equip();

            weaponHolder.EquipementItem?.UnEquip();

            m_PlayerInventoryUI.EquipementInventorySwap(inventoryHolder,weaponHolder);
        }
    }

    private bool CanPlaceEquipement(EquipementItem equipementItem, EquipementType equipementType)
    {
        return equipementItem.EquipementData.EquipementType == equipementType;
    }

    public void SetCurrentMouseHolder(ItemUIHolder itemHolder)
    {
        m_OnMouseHolder = itemHolder;
    }

    private ItemUIHolder[] GetFreeHolderInPlayerInventory()
    {
        return m_PlayerInventoryUI.GetFreeHolderInPlayerInventory();
    }
}
