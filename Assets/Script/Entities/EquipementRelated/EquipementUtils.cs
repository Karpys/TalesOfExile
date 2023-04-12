using UnityEngine;

public static class EquipementUtils
{
    public static void ApplyEquipementStats(EquipementObject equipementObject, BoardEntity entity, bool recomputeStats = true)
    {
        foreach (Modifier modifier in equipementObject.ItemModifiers)
        {
            ModifierUtils.ApplyModifier(modifier,entity);
        }
        
        if(recomputeStats)
            entity.ComputeAllSpells();
    }
    
    public static void UnapplyEquipementStats(EquipementObject equipementObject, BoardEntity entity)
    {
        
        foreach (Modifier modifier in equipementObject.ItemModifiers)
        {
            ModifierUtils.UnapplyModifier(modifier,entity);
        }
        
        entity.ComputeAllSpells();
    }

    public static void Equip(EquipementObject equipementObject, BoardEntity entity)
    {
        EntityEquipement entityEquipement = entity.EntityEquipement;
        EquipementSocket targetSocket = null;

        //Todo:Need to Handle Main/OffHand System Here
        foreach (EquipementSocket socket in entityEquipement.EquipementSockets)
        {
            if (socket.Type == equipementObject.Type)
            {
                targetSocket = socket;
                break;
            }
        }
        
        if (!targetSocket.Empty)
        {
            UnEquip(targetSocket,entity);
        }
        
        ApplyEquipementStats(equipementObject,entity);
        targetSocket.equipementObject = equipementObject;
    }
    public static void UnEquip(EquipementSocket socket,BoardEntity entity)
    {
        //TODO:Sent Equipement to Inventory to entity => Inventory Only Player//
        UnapplyEquipementStats(socket.equipementObject,entity);
        socket.equipementObject.UnEquip();
        socket.equipementObject = null;
    }
}
