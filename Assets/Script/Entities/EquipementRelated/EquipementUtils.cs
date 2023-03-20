using UnityEngine;

public static class EquipementUtils
{
    public static void InitEquip(Equipement equipement,BoardEntity entity)
    {
        EntityEquipement entityEquipement = entity.EntityEquipement;
        EquipementSocket targetSocket = null;

        foreach (EquipementSocket socket in entityEquipement.EquipementSockets)
        {
            if (socket.Type == equipement.Type && socket.Empty)
            {
                targetSocket = socket;
            }
        }

        if (targetSocket == null)
        {
            Debug.Log("No More Free Space for this type : " + equipement.Type);
            return;
        }
        
        targetSocket.Equipement = equipement;
        ApplyEquipementStats(equipement,entity,false);
    }

    public static void ApplyEquipementStats(Equipement equipement, BoardEntity entity, bool recomputeStats = true)
    {
        foreach (Modifier modifier in equipement.Modifiers)
        {
            ModifierUtils.ApplyModifier(modifier,entity);
        }
        
        if(recomputeStats)
            entity.ComputeAllSpells();
    }
    
    public static void UnapplyEquipementStats(Equipement equipement, BoardEntity entity)
    {
        
        foreach (Modifier modifier in equipement.Modifiers)
        {
            ModifierUtils.UnapplyModifier(modifier,entity);
        }
        
        entity.ComputeAllSpells();
    }

    public static void Unequip(EquipementSocket socket,BoardEntity entity)
    {
        //TODO:Sent Equipement to Inventory to entity => Inventory Only Player//
        UnapplyEquipementStats(socket.Equipement,entity);
        socket.Equipement = null;
    }
}
