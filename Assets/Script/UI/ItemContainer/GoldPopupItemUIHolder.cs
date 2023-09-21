using System;
using KarpysDev.Script.Items;
using KarpysDev.Script.Manager;
using KarpysDev.Script.UI.ItemContainer.V2;
using KarpysDev.Script.Widget;
using TMPro;
using UnityEngine;

namespace KarpysDev.Script.UI.ItemContainer
{
    public class GoldPopupItemUIHolder : ItemUIHolder
    {
        [SerializeField] private TMP_Text m_GoldText = null;
        [SerializeField] private PlayerInventoryUI m_PlayerInventoryUI = null;
        [SerializeField] private float m_DeleteTime = 0.5f;

        private float m_GoldValue = 0;

        private Clock m_ClearGoldItem = null;

        private bool m_IsOpen = false;

        public bool IsOpen => m_IsOpen;

        public void SetOpenState(bool open)
        {
            m_IsOpen = open;
        }
        
        public void UpdateGold()
        {
            m_GoldValue = GetGoldValue();
            m_GoldText.text = m_GoldValue.ToString("0") + GoldManager.GOLD_ICON;
        }

        private void Update()
        {
            m_ClearGoldItem?.UpdateClock();
        }

        private float GetGoldValue()
        {
            if (AttachedItem != null)
            {
                return 250f;
            }
            else
            {
                return 0f;
            }
        }

        public void TryLaunchDelete()
        {
            if(AttachedItem != null)
                m_ClearGoldItem = new Clock(m_DeleteTime, ClearGoldItem);
        }

        protected override void OnItemSet(Item item)
        {
            base.OnItemSet(item);
            UpdateGold();
            TryLaunchDelete();
        }

        protected override void OnItemRemoved(Item item)
        {
            base.OnItemRemoved(item);
            UpdateGold();
        }

        private void ClearGoldItem()
        {
            if(AttachedItem == null)
                return;
            
            Transform player = GameManager.Instance.PlayerEntity.transform;
            GoldManager.Instance.SpawnGoldAmount(player.position,player,5,250);
            SetItem(null);
        }

        public override bool CanReceiveItem(Item item, ItemHolderGroup holderSource)
        {
            if (holderSource != ItemHolderGroup.PlayerInventory)
                return false;

            return true;
        }
    }
}