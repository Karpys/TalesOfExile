using KarpysDev.Script.Items;
using UnityEngine;

namespace KarpysDev.Script.UI.ItemContainer.V2
{
    public class PlayerWeaponHolder : PlayerEquipementHolder
    {
        [Header("Player Weapon Specifics")] 
        [SerializeField] private PlayerWeaponHolder m_OtherWeaponHolder = null;
        [SerializeField] private bool m_IsMainWeapon = false;
        [SerializeField] private PlayerInventoryUI m_InventoryUI = null;
        [SerializeField] private PlayerWeaponHolder m_MainWeaponHolder = null;
        [SerializeField] private PlayerWeaponHolder m_SubWeaponHolder = null;

        private bool m_NeedSwap = false;
        
        public override void ApplyItem()
        {
            if (TempItem != null)
            {
                if (!(TempItem is EquipementItem equipementItem))
                    return;
                        
                if (equipementItem.IsTwoHandedWeapon)
                {
                    if (!m_IsMainWeapon)
                    {
                        m_OtherWeaponHolder.ReleaseToInventory();
                        m_NeedSwap = true;
                        return;
                    }
                    else
                    {
                        m_OtherWeaponHolder.ReleaseToInventory();
                        base.ApplyItem();
                        UpdateTwoHandedShadow();
                        return;
                    }
                }
                else
                {
                    //Two handed equiped need to be released//
                    if (!m_IsMainWeapon && m_OtherWeaponHolder.AttachedItem is EquipementItem {IsTwoHandedWeapon: true})
                    {
                        m_OtherWeaponHolder.ReleaseToInventory();
                        m_NeedSwap = true;
                        return;
                    }
                }
            }

            base.ApplyItem();
            UpdateTwoHandedShadow();
        }

        private void UpdateTwoHandedShadow()
        {
            if (m_MainWeaponHolder.AttachedItem is EquipementItem{IsTwoHandedWeapon:true})
            {
                m_SubWeaponHolder.ApplyShadow(m_MainWeaponHolder.AttachedItem.Data.InUIVisual);
            }
            else if(m_SubWeaponHolder.AttachedItem == null)
            {
                m_SubWeaponHolder.DefaultDisplay();
            }
        }

        private void ApplyShadow(Sprite itemShadowSprite)
        {
            m_ItemVisual.sprite = itemShadowSprite;
            m_ItemVisual.color = Color.white.setAlpha(0.5f);
        }

        public override void LateApply()
        {
            if (m_NeedSwap)
            {
                m_OtherWeaponHolder.BaseSetItem(TempItem);
                BaseSetItem(null);
                m_NeedSwap = false;
                UpdateTwoHandedShadow();
            }
        }

        private void BaseSetItem(Item item)
        {
            TempReceiveItem(item);
            base.ApplyItem();
        }

        private void ReleaseToInventory()
        {
            ItemUIHolder[] freeHolders = m_InventoryUI.GetFreeHolderInPlayerInventory();
            base.TempReceiveItem(null);
            freeHolders[0].SetItem(AttachedItem);
            ApplyItem();
        }
    }
}