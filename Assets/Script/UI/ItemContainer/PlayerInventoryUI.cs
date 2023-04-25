using System;
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
            m_ItemContainer[i] = itemHolder;
        }
    }
    
    public void DisplayInventory()
    {
        for (int i = 0; i < m_ItemContainer.Length; i++)
        {
            m_ItemContainer[i].SetItem(m_Inventory.Inventory[i]);
        }
    }

    public void SetPlayerInventory(PlayerInventory inventory)
    {
        m_Inventory = inventory;
    }
}