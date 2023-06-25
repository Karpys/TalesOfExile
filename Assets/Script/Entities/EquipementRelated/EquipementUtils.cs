using KarpysDev.Script.Items;
using KarpysDev.Script.Utils;
using UnityEngine;

namespace KarpysDev.Script.Entities.EquipementRelated
{
    public static class EquipementUtils
    {
        public static void ApplyEquipementStats(EquipementItem equipementItem, BoardEntity entity, bool recomputeStats = true)
        {
            Debug.Log(equipementItem.Data.ObjectName);
            foreach (Modifier modifier in equipementItem.ItemModifiers)
            {
                ModifierUtils.ApplyModifier(modifier,entity);
            }
        
            if(recomputeStats)
                entity.ComputeAllSpells();
        }
    
        public static void UnapplyEquipementStats(EquipementItem equipementItem, BoardEntity entity)
        {
            foreach (Modifier modifier in equipementItem.ItemModifiers)
            {
                ModifierUtils.UnapplyModifier(modifier,entity);
            }
        
            entity.ComputeAllSpells();
        }

        public static void Equip(EquipementItem equipementItem, BoardEntity entity)
        {
            ApplyEquipementStats(equipementItem,entity);
        }

        public static void UnEquip(EquipementItem item, BoardEntity entity)
        {
            UnapplyEquipementStats(item,entity);
        }
    }
}
