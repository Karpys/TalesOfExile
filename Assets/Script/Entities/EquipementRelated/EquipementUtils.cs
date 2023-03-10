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
        ApplyEquipementStats(equipement,entity);
    }

    public static void ApplyEquipementStats(Equipement equipement, BoardEntity entity)
    {
        foreach (Modifier modifier in equipement.Modifiers)
        {
            switch (modifier.Type)
            {
                case ModifierType.UpCold:
                    entity.EntityStats.ColdDamageModifier += modifier.FloatValue;
                    break;
                case ModifierType.UpFire:
                    entity.EntityStats.FireDamageModifier += modifier.FloatValue;
                    break;
                case ModifierType.UpPhysical:
                    entity.EntityStats.PhysicalDamageModifier += modifier.FloatValue;
                    break;
                case ModifierType.SpellAddition:
                    SpellData spellToAdd = SpellLibrary.Instance.GetSpellViaKey(modifier.Value);
                    entity.AddSpellToSpellList(spellToAdd);
                    break;
                default:
                    Debug.LogError("MODIFIER EQUIPEMENT HAS NOT BEEN SET UP");
                    break;
            }
        }
    }
    
    public static void UnapplyEquipementStats(Equipement equipement, BoardEntity entity)
    {
        bool needRecompute = false;
        
        foreach (Modifier modifier in equipement.Modifiers)
        {
            switch (modifier.Type)
            {
                case ModifierType.UpCold:
                    entity.EntityStats.ColdDamageModifier -= modifier.FloatValue;
                    needRecompute = true;
                    break;
                case ModifierType.UpFire:
                    entity.EntityStats.FireDamageModifier -= modifier.FloatValue;
                    needRecompute = true;
                    break;
                case ModifierType.UpPhysical:
                    entity.EntityStats.PhysicalDamageModifier -= modifier.FloatValue;
                    needRecompute = true;
                    break;
                case ModifierType.SpellAddition:
                    SpellData spellToAdd = entity.GetSpellViaKey(modifier.Value);
                    
                    if (spellToAdd == null)
                    {
                        Debug.Log("No Spell key to remove found :" + modifier.Value);
                        break;
                    }
                    
                    entity.RemoveSpellToSpellList(spellToAdd);
                    needRecompute = true;
                    break;
                default:
                    Debug.LogError("MODIFIER EQUIPEMENT HAS NOT BEEN SET UP");
                    break;
            }
        }
        
        if(needRecompute)
            entity.ComputeAllSpells();
    }

    public static void Unequip(EquipementSocket socket,BoardEntity entity)
    {
        //TODO:Sent Equipement to Inventory to entity => Inventory Only Player//
        UnapplyEquipementStats(socket.Equipement,entity);
        socket.Equipement = null;
    }
}
