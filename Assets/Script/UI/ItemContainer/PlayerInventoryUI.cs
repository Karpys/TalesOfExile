using System.Linq;
using KarpysDev.Script.Items;
using KarpysDev.Script.Manager;
using KarpysDev.Script.Map_Related;
using KarpysDev.Script.UI.ItemContainer.V2;
using KarpysDev.Script.Widget;
using UnityEngine;

namespace KarpysDev.Script.UI.ItemContainer
{
    public class PlayerInventoryUI : MonoBehaviour
    {
        [SerializeField] private Transform m_ItemGroup = null;
        [SerializeField] private int m_ItemCount = 0;
        [SerializeField] private ItemUIHolder m_ItemUIHolderPrefab = null;
        [SerializeField] private ItemUIHolderV2 m_ItemUIHolderPrefabV2 = null;

        [Header("Equipement Holder")]
        [SerializeField] private ItemUIHolder[] m_EquipementHolder = null;
        [SerializeField] private PlayerEquipementHolderV2[] m_EquipementHolderV2 = null;
        [SerializeField] private WeaponEquipementUIHolder m_MainWeaponEquipementUIHolder = null;

        private ItemUIHolder[] m_ItemHolders = null;
        private ItemUIHolderV2[] m_ItemHoldersV2 = null;
        private PlayerInventory m_Inventory = null;

        [Header("Popup holders")]
        [SerializeField] private GoldPopupItemUIHolder m_GoldPopupHolder = null;

        public GoldPopupItemUIHolder GoldPopupUIHolder => m_GoldPopupHolder;

        private void Awake()
        {
            m_ItemHoldersV2 = new ItemUIHolderV2[m_ItemCount];
        
            for (int i = 0; i < m_ItemCount; i++)
            {
                ItemUIHolderV2 itemHolder = Instantiate(m_ItemUIHolderPrefabV2, m_ItemGroup);
                //itemHolder.SetId(i);
                m_ItemHoldersV2[i] = itemHolder;
            }
            
            //m_GoldPopupHolder.SetId(m_ItemCount);
            //m_ItemHolders[m_ItemCount] = m_GoldPopupHolder;

            /*for (int i = 0; i < m_EquipementHolder.Length; i++)
            {
                m_EquipementHolder[i].SetId(i);
            }*/
        }

        public void SetPlayerInventory(PlayerInventory inventory)
        {
            m_Inventory = inventory;
            inventory.AssignInventoryHolders(m_ItemHoldersV2);
            inventory.AssignEquipementHolders(m_EquipementHolderV2);
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
            //m_Inventory.UpdateItem(holder.Item,holder.Id);
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
                    return null;
                case ItemHolderGroup.PlayerInventory:
                    return null;
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