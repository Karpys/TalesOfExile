using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class InventoryUIHolder:MonoBehaviour
{
    [SerializeField] private Image m_RarityImage = null;
    [SerializeField] private Image m_ObjectVisual = null;
    [SerializeField] private TMP_Text m_ObjectName = null;
    [SerializeField] private TMP_Text m_Category = null;
    [SerializeField] private TMP_Text m_StackCount = null;
    [SerializeField] private TMP_Text m_SellPrice = null;
    [SerializeField] private Image m_IsEquiped = null;

    [Header("Option Button Transform")] 
    [SerializeField] private RectTransform m_OptionButtonPosition = null;
    private Item m_Item = null;
    public Item Item => m_Item;
    public RectTransform OptionButtonPosition => m_OptionButtonPosition;
    
    public void InitalizeUIHolder(Item item)
    {
        RarityParameter rarityParameter = RarityLibrary.Instance.GetParametersViaKey(item.Rarity);
        
        m_Item = item;
        m_ObjectVisual.sprite = m_Item.Data.InUIVisual;
        m_ObjectName.text = m_Item.Data.ObjectName;
        m_Category.text = m_Item.Data.ObjectType.ToString();
        m_RarityImage.color = rarityParameter.RarityColor;
        
        RefreshEquipedState();
        m_Item.SetHolder(this);
    }

    public void DisplayItemUseOption()
    {
       // ItemButtonOptionController.Instance.DisplayButtonOption(this);
    }
    
    //Button Action//
    public void RefreshEquipedState()
    {
        EquipementItem equipementItem = m_Item as EquipementItem;

        if (equipementItem == null)
        {
            m_IsEquiped.color = Color.gray;
        }
        else
        {
            m_IsEquiped.color = equipementItem.IsEquiped ? Color.green : Color.red;
        }
    }
}