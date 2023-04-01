using UnityEngine;

public static class EquipementUtils
{
    public static void InitEquip(EquipementObject equipementObject,BoardEntity entity)
    {
        EntityEquipement entityEquipement = entity.EntityEquipement;
        EquipementSocket targetSocket = null;

        foreach (EquipementSocket socket in entityEquipement.EquipementSockets)
        {
            if (socket.Type == equipementObject.Type && socket.Empty)
            {
                targetSocket = socket;
            }
        }

        if (targetSocket == null)
        {
            Debug.Log("No More Free Space for this type : " + equipementObject.Type);
            return;
        }
        
        targetSocket.equipementObject = equipementObject;
        ApplyEquipementStats(equipementObject,entity,false);
    }

    public static void ApplyEquipementStats(EquipementObject equipementObject, BoardEntity entity, bool recomputeStats = true)
    {
        foreach (Modifier modifier in equipementObject.Modifiers)
        {
            ModifierUtils.ApplyModifier(modifier,entity);
        }
        
        if(recomputeStats)
            entity.ComputeAllSpells();
    }
    
    public static void UnapplyEquipementStats(EquipementObject equipementObject, BoardEntity entity)
    {
        
        foreach (Modifier modifier in equipementObject.Modifiers)
        {
            ModifierUtils.UnapplyModifier(modifier,entity);
        }
        
        entity.ComputeAllSpells();
    }

    public static void Unequip(EquipementSocket socket,BoardEntity entity)
    {
        //TODO:Sent Equipement to Inventory to entity => Inventory Only Player//
        UnapplyEquipementStats(socket.equipementObject,entity);
        socket.equipementObject = null;
    }
}
