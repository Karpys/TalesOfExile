using KarpysDev.Script.Entities.EquipementRelated;
using KarpysDev.Script.Items;
using UnityEngine;

namespace KarpysDev.Script.UI.ItemContainer.V2
{
    using Entities;

    public class PlayerEquipementHolder : ItemUIHolder
    {
        [Header("Player Equipement Specifics")]
        [SerializeField] private Sprite m_ShadowSprite = null;
        [SerializeField] private EquipementType m_EquipementType = EquipementType.Weapon;
        
        protected BoardEntity m_AttachedEntity = null;
        protected override void DefaultDisplay()
        {
            base.DefaultDisplay();
            m_ItemVisual.sprite = m_ShadowSprite;
            m_ItemVisual.color = Color.white;
        }

        public void AssignEntity(BoardEntity entity)
        {
            m_AttachedEntity = entity;
        }

        public override bool CanReceiveItem(Item item, ItemHolderGroup holderSource)
        {
            if (holderSource != ItemHolderGroup.PlayerInventory)
                return false;
            
            if (item is EquipementItem equipementItem && equipementItem.Type == m_EquipementType)
            {
                return true;
            }else if (item == null)
            {
                return true;
            }

            return false;
        }

        protected override void OnItemSet(Item item)
        {
            ((EquipementItem) item)?.Equip(m_AttachedEntity);
        }

        protected override void OnItemRemoved(Item item)
        {
            ((EquipementItem) item)?.UnEquip(m_AttachedEntity);
        }
    }
}