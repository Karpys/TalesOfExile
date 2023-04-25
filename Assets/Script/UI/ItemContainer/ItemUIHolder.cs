using UnityEngine;
using UnityEngine.UI;

public class ItemUIHolder : MonoBehaviour
{
    [SerializeField] private Sprite m_DefaultItemBorder = null;
    [SerializeField] private Sprite m_ItemBorder = null;
    
    [SerializeField] private Image m_ItemVisual = null;
    [SerializeField] private Image m_ItemRarityBorder = null;

    private Item m_AttachedItem = null;

    public Item Item => m_AttachedItem;

    public void InitializeHolder(Item item)
    {
        m_AttachedItem = item;
        m_ItemVisual.sprite = item.Data.InUIVisual;
        m_ItemVisual.color = Color.white;

        RarityParameter rarityParameter = RarityLibrary.Instance.GetParametersViaKey(item.Rarity);
        m_ItemRarityBorder.sprite = m_ItemBorder;
        m_ItemRarityBorder.color = rarityParameter.RarityColor;
    }

    public void DefaultDisplay()
    {
        m_AttachedItem = null;
        //Set to default sprite
        m_ItemVisual.color = Color.white.setAlpha(0);
        
        m_ItemRarityBorder.sprite = m_DefaultItemBorder;
    }
    
    public void DisplayItemUseOption()
    {
        if(m_AttachedItem == null)
            return;
        
        ItemButtonOptionController.Instance.DisplayButtonOption(this);
    }
}