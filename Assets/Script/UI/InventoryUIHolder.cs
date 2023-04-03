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

    private InventoryObjectData m_ObjectBase = null;
    public void InitalizeUIHolder(InventoryObject inventoryObject)
    {
        m_ObjectBase = inventoryObject.Data;

        m_ObjectVisual.sprite = m_ObjectBase.InUIVisual;
        m_ObjectName.text = m_ObjectBase.ObjectName;
        m_Category.text = m_ObjectBase.ObjectType.ToString();
    }
}