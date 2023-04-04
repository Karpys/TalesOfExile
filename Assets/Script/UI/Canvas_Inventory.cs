using System;
using System.Collections.Generic;
using System.Linq;
using TweenCustom;
using UnityEngine;

public class Canvas_Inventory : MonoBehaviour
{
    [SerializeField] private InventoryUIHolder m_InventoryHolder = null;
    [SerializeField] private Transform m_InventoryContainer = null;
    [SerializeField] private Transform m_ItemLayoutContainer = null;
    [SerializeField] private ViewportAdaptativeSize m_ScrollViewSize = null;
    [SerializeField] private int m_StartPoolHolder = 20;

    private PlayerInventory m_Inventory = null;
    private List<InventoryUIHolder> m_PoolHolder = new List<InventoryUIHolder>();
    private bool m_IsShown = false;

    private void Awake()
    {
        for (int i = 0; i < m_StartPoolHolder; i++)
        {
            AddInventoryHolder();
        }
    }

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

    private InventoryUIHolder AddInventoryHolder()
    {
        InventoryUIHolder holder = Instantiate(m_InventoryHolder, m_ItemLayoutContainer);
        m_PoolHolder.Add(holder);
        return holder;
    }

    public void SetPlayerInventory(PlayerInventory inventory)
    {
        m_Inventory = inventory;
    }
    
    private void Show()
    {
        m_InventoryContainer.transform.DoKill();
        m_InventoryContainer.gameObject.SetActive(true);
        m_InventoryContainer.transform.localScale = Vector3.zero;
        m_InventoryContainer.transform.DoScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.EASE_OUT_SIN);
        DisplayInventory();
        m_ScrollViewSize.AdaptSize();
    }

    private void Hide()
    {
        m_InventoryContainer.transform.DoKill();
        m_InventoryContainer.transform.DoScale(Vector3.zero, 0.5f).SetEase(Ease.EASE_OUT_SIN).OnComplete(() =>
        {
            m_InventoryContainer.gameObject.SetActive(false);
        });
    }

    private void DisplayInventory()
    {
        PoolCheck(m_Inventory.Inventory.Count);

        int inventoryObjectCount = m_Inventory.Inventory.Count;
        
        for (int i = 0; i < inventoryObjectCount; i++)
        {
            m_PoolHolder[i].gameObject.SetActive(true);
            m_PoolHolder[i].InitalizeUIHolder(m_Inventory.Inventory[i]);
        }

        for (int i = inventoryObjectCount; i < m_PoolHolder.Count; i++)
        {
            m_PoolHolder[i].gameObject.SetActive(false);
        }
    }

    private void PoolCheck(int objectCount)
    {
        if(objectCount <= m_PoolHolder.Count)
            return;

        for (int i = m_PoolHolder.Count; i < objectCount; i++)
        {
            AddInventoryHolder();
        }
    }
}