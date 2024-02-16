namespace KarpysDev.Script.UI.ItemContainer.V2
{
    using Items;
    using UnityEngine;
    using KarpysUtils;
    using Spell.DamageSpell;
    using ColorExtensions = ColorExtensions;

    public class PlayerWeaponHolder : PlayerEquipementHolder
    {
        [Header("Player Weapon Specifics")] 
        [SerializeField] private PlayerWeaponHolder m_OtherWeaponHolder = null;
        [SerializeField] private bool m_IsMainWeapon = false;
        [SerializeField] private PlayerInventoryUI m_InventoryUI = null;
        [SerializeField] private PlayerWeaponHolder m_MainWeaponHolder = null;
        [SerializeField] private PlayerWeaponHolder m_SubWeaponHolder = null;
        [SerializeField] private WeaponTarget m_WeaponTarget = WeaponTarget.MainWeapon;

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
            m_ItemVisual.color = ColorExtensions.setAlpha(Color.white, 0.5f);
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

        protected override void OnItemSet(Item item)
        {
            if (item is WeaponItem weaponItem)
            {
                weaponItem.EquipWeapon(m_AttachedEntity,m_WeaponTarget);
            }
        }

        protected override void OnItemRemoved(Item item)
        {
            if (item is WeaponItem weaponItem)
            {
                weaponItem.UnEquipWeapon(m_AttachedEntity,m_WeaponTarget);
            }
        }
    }
}