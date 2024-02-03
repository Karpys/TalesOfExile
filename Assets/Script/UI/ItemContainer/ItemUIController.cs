using KarpysDev.Script.Entities.EquipementRelated;
using KarpysDev.Script.Items;
using KarpysDev.Script.UI.ItemContainer.V2;
using KarpysDev.Script.UI.Pointer;
using UnityEngine;

namespace KarpysDev.Script.UI.ItemContainer
{
    using KarpysUtils;

    public class ItemUIController : SingletonMonoBehavior<ItemUIController>
    {
        [SerializeField] private PlayerInventoryUI m_PlayerInventoryUI = null;
        [SerializeField] private UISelectionFade m_ItemFade = null;
        [SerializeField] private GoldPopupItemUIHolder m_GoldPopupHolder = null;
        
        
        private ItemUIHolder m_OnClickHolder = null;
        private ItemUIHolder m_OnMouseHolder = null;

        private bool m_DragBegin = false;
        public ItemUIHolder OnMouseHolder => m_OnMouseHolder;
        private void Update()
        {
            if (!m_DragBegin)
            {
                if(m_OnMouseHolder == null)
                    return;
            
                if (Input.GetMouseButtonDown(0))
                {
                    if (m_OnMouseHolder.MouseOn && m_OnMouseHolder.AttachedItem != null)
                    {
                        Debug.Log("Select item : " + m_OnMouseHolder.AttachedItem.Data.ObjectName);
                        m_OnClickHolder = m_OnMouseHolder;
                        m_ItemFade.Initialize(m_OnClickHolder.AttachedItem.Data.InUIVisual);
                        m_DragBegin = true;
                    }
                }
            }
            else
            {
                if (Input.GetMouseButtonUp(0))
                {
                    if (m_OnMouseHolder != m_OnClickHolder && m_OnMouseHolder.MouseOn)
                    {
                        PerformHolderAction(m_OnClickHolder,m_OnMouseHolder);
                        //PerformHolderAction(m_OnClickHolder,m_OnMouseHolder);
                    }
                    else if (m_OnClickHolder.MouseOn)
                    {
                        //Perform Same Holder Action
                        PerformSameHolderAction(m_OnClickHolder);
                    }
                    else
                    {
                        PerformSingleHolderAction(m_OnClickHolder);
                    }

                    m_DragBegin = false;
                    m_ItemFade.Clear();
                }
            }
        }

        private void PerformSameHolderAction(ItemUIHolder singleHolder)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                PerformHolderAction(singleHolder,m_GoldPopupHolder);
            }
        }

        private void PerformSingleHolderAction(ItemUIHolder holder)
        {
            int holderActionType = (int) holder.ItemHolderGroupSource;
        
            if (holderActionType == 1)
            {
                m_PlayerInventoryUI.DropInventoryItem(m_OnClickHolder);
            }
        }

        private void PerformHolderAction(ItemUIHolder holder1, ItemUIHolder holder2)
        {
            Item item1 = holder1.AttachedItem;
            Item item2 = holder2.AttachedItem;

            Debug.Log("Try Perform Holder Action");
            Debug.Log("Holder 1 Can Receive Item : " +  holder1.CanReceiveItem(item2,holder2.ItemHolderGroupSource));
            Debug.Log("Holder 2 Can Receive Item : " +  holder2.CanReceiveItem(item1,holder1.ItemHolderGroupSource));
            
            if (holder1.CanReceiveItem(item2,holder2.ItemHolderGroupSource) && holder2.CanReceiveItem(item1, holder1.ItemHolderGroupSource))
            {
                holder2.TempReceiveItem(item1);
                holder1.TempReceiveItem(item2);
                holder1.ApplyItem();
                holder2.ApplyItem();
                holder1.LateApply();
                holder2.LateApply();
            }
        }

        /*private void SwapSellPopup(ItemUIHolder holder1, ItemUIHolder holder2)
        {
            m_PlayerInventoryUI.SwapInventoryHolderItem(holder1,holder2);
            m_PlayerInventoryUI.GoldPopupUIHolder.UpdateGold();
            m_PlayerInventoryUI.GoldPopupUIHolder.TryLaunchDelete();
        }*/

        public void SetCurrentMouseHolder(ItemUIHolder itemHolder)
        {
            m_OnMouseHolder = itemHolder;
        }
    }
}
