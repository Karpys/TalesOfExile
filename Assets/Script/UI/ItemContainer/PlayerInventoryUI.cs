using System;
using System.Linq;
using UnityEngine;

public class PlayerInventoryUI : MonoBehaviour
{
    [SerializeField] private Transform m_ItemGroup = null;
    [SerializeField] private int m_ItemCount = 0;
    [SerializeField] private ItemUIHolder m_ItemUIHolderPrefab = null;

    [Header("Equipement Holder")]
    [SerializeField] private ItemUIHolder[] m_EquipementHolder = null;
    private ItemUIHolder[] m_ItemContainer = null;
    private PlayerInventory m_Inventory = null;

    private void Awake()
    {
        m_ItemContainer = new ItemUIHolder[m_ItemCount];
        
        for (int i = 0; i < m_ItemCount; i++)
        {
            ItemUIHolder itemHolder = Instantiate(m_ItemUIHolderPrefab, m_ItemGroup);
            itemHolder.SetId(i);
            itemHolder.SetGroup(ItemHolderGroup.PlayerInventory);
            m_ItemContainer[i] = itemHolder;
        }

        for (int i = 0; i < m_EquipementHolder.Length; i++)
        {
            m_EquipementHolder[i].SetId(i);
        }
    }

    private void OnDestroy()
    {
        if (m_Inventory)
            m_Inventory.A_OnPickUp -= RefreshWhenOpen;
    }

    public void RefreshInventoryDisplay()
    {
        for (int i = 0; i < m_ItemContainer.Length; i++)
        {
            m_ItemContainer[i].SetItem(m_Inventory.Inventory[i]);
        }

        for (int i = 0; i < m_EquipementHolder.Length; i++)
        {
            m_EquipementHolder[i].SetItem(m_Inventory.Equipement[i]);
        }
    }

    private void RefreshWhenOpen()
    {
        if (!gameObject.activeInHierarchy)
            return;
        RefreshInventoryDisplay();
    }

    public void SetPlayerInventory(PlayerInventory inventory)
    {
        if (m_Inventory)
            m_Inventory.A_OnPickUp -= RefreshWhenOpen;
        
        m_Inventory = inventory;
        m_Inventory.A_OnPickUp += RefreshWhenOpen;
    }
    
    public void SwapItem(int idHolder1, int idHolder2)
    {
        m_Inventory.SwapItem(idHolder1, idHolder2);
        m_ItemContainer[idHolder1].SetItem(m_Inventory.Inventory[idHolder1]);
        m_ItemContainer[idHolder2].SetItem(m_Inventory.Inventory[idHolder2]);
    }

    private void SetItemToTargetGroup(Item targetItem,ItemHolderGroup targetGroup, int targetId)
    {
        Item[] targetGroupItem = GetItemArrayViaGroup(targetGroup);
        targetGroupItem[targetId] = targetItem;

        if (targetItem != null)
        {
            Debug.Log("Place Item to : " + targetItem.Data.ObjectName);
        }
    }

    public void EquipementInventorySwap(ItemUIHolder inventoryHolder, ItemUIHolder equipementHolder)
    {
        Item tempItem = equipementHolder.Item;
        SetItemToTargetGroup(inventoryHolder.Item,ItemHolderGroup.PlayerEquipement,equipementHolder.Id);
        SetItemToTargetGroup(tempItem,ItemHolderGroup.PlayerInventory,inventoryHolder.Id);

        //TODO:Refresh Only swaped holder//
        //m_ItemContainer[inventoryHolder.Id].SetItem(m_Inventory.Inventory[inventoryHolder.Id]);
        //m_EquipementHolder[equipementHolder.Id].SetItem(m_Inventory.Equipement[equipementHolder.Id]);
        RefreshInventoryDisplay();
    }

    private Item[] GetItemArrayViaGroup(ItemHolderGroup targetGroup)
    {
        switch (targetGroup)
        {
            case ItemHolderGroup.PlayerEquipement:
                return m_Inventory.Equipement;
            case ItemHolderGroup.PlayerInventory:
                return m_Inventory.Inventory;
            default:
                return null;
        }
    }
}