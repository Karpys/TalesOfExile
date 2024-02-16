using System.Linq;
using KarpysDev.Script.Items;
using KarpysDev.Script.Manager;
using KarpysDev.Script.Map_Related;
using KarpysDev.Script.UI.ItemContainer.V2;
using KarpysDev.Script.Widget;
using UnityEngine;

namespace KarpysDev.Script.UI.ItemContainer
{
    using Entities;

    public class PlayerInventoryUI : MonoBehaviour
    {
        [SerializeField] private Transform m_ItemGroup = null;
        [SerializeField] private int m_ItemCount = 0;
        [SerializeField] private ItemUIHolder m_ItemUIHolderPrefab = null;

        [Header("Equipement Holder")]
        [SerializeField] private PlayerEquipementHolder[] m_EquipementHolder = null;

        private ItemUIHolder[] m_ItemHolders = null;
        private PlayerInventory m_Inventory = null;

        //[Header("Popup holders")]
        //[SerializeField] private GoldPopupItemUIHolder m_GoldPopupHolder = null;

        //public GoldPopupItemUIHolder GoldPopupUIHolder => m_GoldPopupHolder;

        private void Awake()
        {
            m_ItemHolders = new ItemUIHolder[m_ItemCount];
        
            for (int i = 0; i < m_ItemCount; i++)
            {
                ItemUIHolder itemHolder = Instantiate(m_ItemUIHolderPrefab, m_ItemGroup);
                //itemHolder.SetId(i);
                m_ItemHolders[i] = itemHolder;
            }
            
            //m_GoldPopupHolder.SetId(m_ItemCount);
            //m_ItemHolders[m_ItemCount] = m_GoldPopupHolder;

            /*for (int i = 0; i < m_EquipementHolder.Length; i++)
            {
                m_EquipementHolder[i].SetId(i);
            }*/
        }

        public void SetPlayerInventory(PlayerInventory inventory,BoardEntity player)
        {
            foreach (PlayerEquipementHolder playerEquipementHolder in m_EquipementHolder)
            {
                playerEquipementHolder.AssignEntity(player);
            }
            
            m_Inventory = inventory;
            inventory.AssignInventoryHolders(m_ItemHolders);
            inventory.AssignEquipementHolders(m_EquipementHolder);
            
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



        public void DropInventoryItem(ItemUIHolder inventoryHolder)
        {
            Tile playerTile = MapData.Instance.GetTile(GameManager.Instance.PlayerEntity.EntityPosition);
            Tile freeTile = TileHelper.GetFreeClosestAround(playerTile, MousePosition.Instance.MouseWorldPosition);
        
            if(freeTile == null)
                return;
        
            LootController.Instance.SpawnLoot(inventoryHolder.AttachedItem,playerTile,freeTile);
            inventoryHolder.SetItem(null);
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
            return m_ItemHolders.Take(m_ItemCount).Where(e => e.AttachedItem == null).ToArray();
        }
    }
}