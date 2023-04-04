using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIHolder:MonoBehaviour
{
    [SerializeField] private Image m_RarityImage = null;
    [SerializeField] private Image m_ObjectVisual = null;
    [SerializeField] private TMP_Text m_ObjectName = null;
    [SerializeField] private TMP_Text m_Category = null;
    [SerializeField] private TMP_Text m_StackCount = null;
    [SerializeField] private TMP_Text m_SellPrice = null;
    [SerializeField] private Image m_IsEquiped = null;

    private InventoryObject m_InventoryObject = null;
    public void InitalizeUIHolder(InventoryObject inventoryObject)
    {
        m_InventoryObject = inventoryObject;

        m_ObjectVisual.sprite = m_InventoryObject.Data.InUIVisual;
        m_ObjectName.text = m_InventoryObject.Data.ObjectName;
        m_Category.text = m_InventoryObject.Data.ObjectType.ToString();
    }

    public void DisplayItemUseOption()
    {
        ItemButtonOptionController.Instance.DisplayButtonOption(m_InventoryObject);
    }
}