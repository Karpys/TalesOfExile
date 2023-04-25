using System;
using System.Linq;
using UnityEngine;

public class PlayerInventoryUI : MonoBehaviour
{
    [SerializeField] private Transform m_ItemGroup = null;
    [SerializeField] private int m_ItemCount = 0;
    [SerializeField] private ItemUIHolder m_ItemUIHolderPrefab = null;

    private ItemUIHolder[] m_ItemContainer = null;
    private PlayerInventory m_Inventory = null;

    private void Awake()
    {
        m_ItemContainer = new ItemUIHolder[m_ItemCount];
        
        for (int i = 0; i < m_ItemCount; i++)
        {
            ItemUIHolder itemHolder = Instantiate(m_ItemUIHolderPrefab, m_ItemGroup);
            itemHolder.SetId(i);
            m_ItemContainer[i] = itemHolder;
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
}