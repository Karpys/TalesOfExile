using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class InventoryUIHolder:MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private Image m_RarityImage = null;
    [SerializeField] private Image m_ObjectVisual = null;
    [SerializeField] private TMP_Text m_ObjectName = null;
    [SerializeField] private TMP_Text m_Category = null;
    [SerializeField] private TMP_Text m_StackCount = null;
    [SerializeField] private TMP_Text m_SellPrice = null;
    [SerializeField] private Image m_IsEquiped = null;

    private InventoryObject m_InventoryObject = null;
    public InventoryObject InventoryObject => m_InventoryObject;
    
    public void InitalizeUIHolder(InventoryObject inventoryObject)
    {
        m_InventoryObject = inventoryObject;
        m_ObjectVisual.sprite = m_InventoryObject.Data.InUIVisual;
        m_ObjectName.text = m_InventoryObject.Data.ObjectName;
        m_Category.text = m_InventoryObject.Data.ObjectType.ToString();
        RefreshEquipedState();
        
        m_InventoryObject.SetHolder(this);
    }

    public void DisplayItemUseOption()
    {
        ItemButtonOptionController.Instance.DisplayButtonOption(this);
    }
    
    //Button Action//
    public void RefreshEquipedState()
    {
        EquipementObject equipementObject = m_InventoryObject as EquipementObject;

        if (equipementObject == null)
        {
            m_IsEquiped.color = Color.gray;
        }
        else
        {
            m_IsEquiped.color = equipementObject.IsEquiped ? Color.green : Color.red;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Add RecordUI
    }

    //In RecordUIManager? time same rinventoryUIHolder => case display item//
    public void OnPointerExit(PointerEventData eventData)
    {
        //Remove Record UI
    }

    
}