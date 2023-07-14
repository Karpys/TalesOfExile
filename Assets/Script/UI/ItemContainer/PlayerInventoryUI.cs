using System.Linq;
using KarpysDev.Script.Items;
using KarpysDev.Script.Manager;
using KarpysDev.Script.Map_Related;
using KarpysDev.Script.Widget;
using UnityEngine;

namespace KarpysDev.Script.UI.ItemContainer
{
    public class PlayerInventoryUI : MonoBehaviour
    {
        [SerializeField] private Transform m_ItemGroup = null;
        [SerializeField] private int m_ItemCount = 0;
        [SerializeField] private ItemUIHolder m_ItemUIHolderPrefab = null;

        [Header("Equipement Holder")]
        [SerializeField] private ItemUIHolder[] m_EquipementHolder = null;
        private ItemUIHolder[] m_ItemHolders = null;
        private PlayerInventory m_Inventory = null;

        [Header("Popup holders")]
        [SerializeField] private GoldPopupItemUIHolder m_GoldPopupHolder = null;

        public GoldPopupItemUIHolder GoldPopupUIHolder => m_GoldPopupHolder;

        private void Awake()
        {
            m_ItemHolders = new ItemUIHolder[m_ItemCount + 1];
        
            for (int i = 0; i < m_ItemCount; i++)
            {
                ItemUIHolder itemHolder = Instantiate(m_ItemUIHolderPrefab, m_ItemGroup);
                itemHolder.SetId(i);
                itemHolder.SetGroup(ItemHolderGroup.PlayerInventory);
                m_ItemHolders[i] = itemHolder;
            }
            
            m_GoldPopupHolder.SetId(m_ItemCount);
            m_ItemHolders[m_ItemCount] = m_GoldPopupHolder;

            for (int i = 0; i < m_EquipementHolder.Length; i++)
            {
                m_EquipementHolder[i].SetId(i);
            }
        }

        private void OnDestroy()
        {
            if (m_Inventory)
                m_Inventory.A_OnPickUp -= RefreshWhenOpen;
        }

        public void RefreshInventoryDisplay()
        {
            for (int i = 0; i < m_ItemHolders.Length; i++)
            {
                m_ItemHolders[i].SetItem(m_Inventory.Inventory[i]);
            }

            for (int i = 0; i < m_EquipementHolder.Length; i++)
            {
                m_EquipementHolder[i].SetItem(m_Inventory.Equipement[i]);
            }
        }

        private void RefreshWhenOpen()
        {
            if (!gameObject.activeInHierarchy)
                return;
            RefreshInventoryDisplay();
        }

        public void SetPlayerInventory(PlayerInventory inventory)
        {
            if (m_Inventory)
                m_Inventory.A_OnPickUp -= RefreshWhenOpen;
        
            m_Inventory = inventory;
            m_Inventory.A_OnPickUp += RefreshWhenOpen;
        }
    
        public void SwapInventoryHolderItem(ItemUIHolder holder1, ItemUIHolder holder2)
        {
            
            Item tempItem = holder1.Item;
            holder1.SetItem(holder2.Item);
            holder2.SetItem(tempItem);
            UpdateInventoryCollection(holder1);
            UpdateInventoryCollection(holder2);
        }

        public void UpdateInventoryCollection(ItemUIHolder holder)
        {
            m_Inventory.UpdateItem(holder.Item,holder.Id);
        }

        private void SetItemToTargetGroup(Item targetItem,ItemHolderGroup targetGroup, int targetId)
        {
            Item[] targetGroupItem = GetItemArrayViaGroup(targetGroup);
            targetGroupItem[targetId] = targetItem;

            if (targetItem != null)
            {
                Debug.Log("Place Item to : " + targetItem.Data.ObjectName);
            }
        }

        public void EquipementInventorySwap(ItemUIHolder inventoryHolder, ItemUIHolder equipementHolder)
        {
            Item tempItem = equipementHolder.Item;
            SetItemToTargetGroup(inventoryHolder.Item,ItemHolderGroup.PlayerEquipement,equipementHolder.Id);
            SetItemToTargetGroup(tempItem,ItemHolderGroup.PlayerInventory,inventoryHolder.Id);

            equipementHolder.SetItem(inventoryHolder.Item);
            inventoryHolder.SetItem(tempItem);
        }

        public void DropInventoryItem(ItemUIHolder inventoryHolder)
        {
            Tile playerTile = MapData.Instance.GetTile(GameManager.Instance.PlayerEntity.EntityPosition);
            Tile freeTile = TileHelper.GetFreeClosestAround(playerTile, MousePosition.Instance.MouseWorldPosition);
        
            if(freeTile == null)
                return;
        
            LootController.Instance.SpawnLoot(inventoryHolder.Item,playerTile,freeTile);
            inventoryHolder.SetItem(null);
            UpdateInventoryCollection(inventoryHolder);
        }

        private Item[] GetItemArrayViaGroup(ItemHolderGroup targetGroup)
        {
            switch (targetGroup)
            {
                case ItemHolderGroup.PlayerEquipement:
                    return m_Inventory.Equipement;
                case ItemHolderGroup.PlayerInventory:
                    return m_Inventory.Inventory;
                default:
                    return null;
            }
        }

        public ItemUIHolder[] GetFreeHolderInPlayerInventory()
        {
            return m_ItemHolders.Take(m_ItemCount).Where(e => e.Item == null).ToArray();
        }
    }
}