using UnityEngine;

public static class EquipementUtils
{
    public static void ApplyEquipementStats(EquipementItem equipementItem, BoardEntity entity, bool recomputeStats = true)
    {
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
        EntityEquipement entityEquipement = entity.EntityEquipement;
        EquipementSocket targetSocket = null;

        //Todo:Need to Handle Main/OffHand System Here
        foreach (EquipementSocket socket in entityEquipement.EquipementSockets)
        {
            if (socket.Type == equipementItem.Type)
            {
                targetSocket = socket;
                break;
            }
        }
        
        if (!targetSocket.Empty)
        {
            UnEquip(targetSocket,entity);
        }
        
        ApplyEquipementStats(equipementItem,entity);
        targetSocket.EquipementItem = equipementItem;
    }
    public static void UnEquip(EquipementSocket socket,BoardEntity entity)
    {
        //TODO:Sent Equipement to Inventory to entity => Inventory Only Player//
        UnapplyEquipementStats(socket.EquipementItem,entity);
        socket.EquipementItem.UnEquip();
        socket.EquipementItem = null;
    }
}
