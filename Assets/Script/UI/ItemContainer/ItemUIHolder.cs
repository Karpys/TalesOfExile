using UnityEngine;
using UnityEngine.UI;

public class ItemUIHolder : MonoBehaviour
{
    [SerializeField] private Sprite m_DefaultItemBorder = null;
    [SerializeField] private Sprite m_ItemBorder = null;
    
    [SerializeField] private Image m_ItemVisual = null;
    [SerializeField] private Image m_ItemRarityBorder = null;

    private Item m_AttachedItem = null;
    private bool m_MouseOn = false;
    private int m_Id = -1;
    public Item Item => m_AttachedItem;
    public int Id => m_Id;
    public bool MouseOn
    {
        get => m_MouseOn;
        set => m_MouseOn = value;
    }
    
    public void SetId(int id)
    {
        m_Id = id;
    }
    
    public void SetItem(Item item)
    {
        if (item == null)
        {
            DefaultDisplay();
            return;
        }
        
        m_AttachedItem = item;
        m_ItemVisual.sprite = item.Data.InUIVisual;
        m_ItemVisual.color = Color.white;

        RarityParameter rarityParameter = RarityLibrary.Instance.GetParametersViaKey(item.Rarity);
        m_ItemRarityBorder.sprite = m_ItemBorder;
        m_ItemRarityBorder.color = rarityParameter.RarityColor;
    }

    private void DefaultDisplay()
    {
        m_AttachedItem = null;
        //Set to default sprite
        m_ItemRarityBorder.sprite = m_DefaultItemBorder;
        m_ItemRarityBorder.color = Color.white;
    }
    
    public void DisplayItemUseOption()
    {
        if(m_AttachedItem == null)
            return;
        
        ItemButtonOptionController.Instance.DisplayButtonOption(this);
    }
}