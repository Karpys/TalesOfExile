using System;
using KarpysDev.Script.Manager;
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

        private static string GOLD_ICON = " <sprite name=\"GoldIcon\">";
        
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
            m_GoldText.text = m_GoldValue.ToString("0") + GOLD_ICON;
        }

        private void Update()
        {
            m_ClearGoldItem?.UpdateClock();
        }

        private float GetGoldValue()
        {
            if (Item != null)
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
            if(Item != null)
                m_ClearGoldItem = new Clock(m_DeleteTime, ClearGoldItem);
        }

        private void ClearGoldItem()
        {
            if(Item == null)
                return;
            
            Transform player = GameManager.Instance.PlayerEntity.transform;
            GoldManager.Instance.SpawnGoldAmount(player.position,player,5,250);
            SetItem(null);
            m_PlayerInventoryUI.UpdateInventoryCollection(this);
            UpdateGold();
        }
    }
}