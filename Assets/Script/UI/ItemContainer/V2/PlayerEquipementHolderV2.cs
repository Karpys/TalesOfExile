using KarpysDev.Script.Entities.EquipementRelated;
using KarpysDev.Script.Items;
using UnityEngine;

namespace KarpysDev.Script.UI.ItemContainer.V2
{
    public class PlayerEquipementHolderV2 : ItemUIHolderV2
    {
        [Header("Player Equipement Specifics")]
        [SerializeField] private Sprite m_ShadowSprite = null;
        [SerializeField] private EquipementType m_EquipementType = EquipementType.Weapon;

        protected override void DefaultDisplay()
        {
            base.DefaultDisplay();
            m_ItemVisual.sprite = m_ShadowSprite;
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
            ((EquipementItem) item)?.Equip();
        }

        protected override void OnItemRemoved(Item item)
        {
            ((EquipementItem) item)?.UnEquip();
        }
    }
}