using KarpysDev.Script.Items;
using KarpysDev.Script.Manager.Library;
using UnityEngine;
using UnityEngine.UI;

namespace KarpysDev.Script.UI.ItemContainer
{
    public class ItemUIHolder : MonoBehaviour
    {
        [SerializeField] private Sprite m_DefaultItemBorder = null;
        [SerializeField] private Sprite m_ItemBorder = null;
    
        [SerializeField] protected Image m_ItemVisual = null;
        [SerializeField] private Image m_ItemRarityBorder = null;

        [SerializeField] private ItemHolderGroup m_HolderGroup = ItemHolderGroup.Stash;
        private Item m_AttachedItem = null;
        private bool m_MouseOn = false;
        private int m_Id = -1;
        public Item Item => m_AttachedItem;
        public int Id => m_Id;
        public ItemHolderGroup HolderGroup => m_HolderGroup;
        public bool MouseOn
        {
            get => m_MouseOn;
            set => m_MouseOn = value;
        }
    
        public void SetId(int id)
        {
            m_Id = id;
        }

        public void SetGroup(ItemHolderGroup holderGroup)
        {
            m_HolderGroup = holderGroup;
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

        protected virtual void DefaultDisplay()
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


    public enum ItemHolderGroup
    {
        PlayerInventory = 1,
        PlayerEquipement = 2,
        Stash = 4,
        SellPopup = 4,
    }
}