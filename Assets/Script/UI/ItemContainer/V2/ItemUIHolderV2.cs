using KarpysDev.Script.Items;
using UnityEngine;
using UnityEngine.UI;

namespace KarpysDev.Script.UI.ItemContainer.V2
{
    public abstract class ItemUIHolderV2 : MonoBehaviour
    {
        [SerializeField] private Sprite m_DefaultItemBorder = null;
        [SerializeField] private Sprite m_DefinedItemBorder = null;
    
        [SerializeField] protected Image m_ItemVisual = null;
        [SerializeField] private Image m_ItemRarityBorder = null;

        [Header("Source")] 
        [SerializeField] private ItemHolderGroup m_HolderGroupSource = ItemHolderGroup.Stash;
        
        private Item m_AttachedItem = null;
        private Item m_TempItem = null;
        private bool m_MouseOn = false;

        public ItemHolderGroup ItemHolderGroupSource => m_HolderGroupSource;
        public Item AttachedItem => m_AttachedItem;
        public bool MouseOn
        {
            get => m_MouseOn;
            set => m_MouseOn = value;
        }

        protected virtual void DefaultDisplay()
        {
            m_ItemRarityBorder.sprite = m_DefaultItemBorder;
            m_ItemRarityBorder.color = Color.white;

            m_ItemVisual.sprite = null;
        }

        protected virtual void ApplyItemVisual()
        {
            DefaultDisplay();
            m_ItemVisual.sprite = m_AttachedItem.Data.InUIVisual;
            m_ItemRarityBorder.sprite = m_DefinedItemBorder;
            
            Debug.Log("Apply Item Visual");
        }
        
        public void TempReceiveItem(Item item)
        {
            m_TempItem = item;
        }

        public virtual void ApplyItem()
        {
            if (m_TempItem != m_AttachedItem)
            {
                OnItemRemoved(m_AttachedItem);
            }
            
            m_AttachedItem = m_TempItem;
            m_TempItem = null;
            
            if(m_AttachedItem == null)
                DefaultDisplay();
            else
            {
                ApplyItemVisual();
            }
            
            OnItemSet(m_AttachedItem);
        }

        public void SetItem(Item item)
        {
            TempReceiveItem(item);
            ApplyItem();
        }
        
        protected virtual void OnItemSet(Item item)
        {
            return;
        }

        protected virtual void OnItemRemoved(Item item)
        {
            return;
        }

        public abstract bool CanReceiveItem(Item item, ItemHolderGroup holderSource);
    }
}