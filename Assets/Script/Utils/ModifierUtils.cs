using UnityEngine;

public static class ModifierUtils
{
    public static void ApplyModifier(Modifier modifier,BoardEntity entity)
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
                Debug.LogError("MODIFIER HAS NOT BEEN SET UP");
                break;
        }
    }
    
    public static void UnapplyModifier(Modifier modifier, BoardEntity entity)
    {
        switch (modifier.Type)
        {
            case ModifierType.UpCold:
                entity.EntityStats.ColdDamageModifier -= modifier.FloatValue;
                break;
            case ModifierType.UpFire:
                entity.EntityStats.FireDamageModifier -= modifier.FloatValue;
                break;
            case ModifierType.UpPhysical:
                entity.EntityStats.PhysicalDamageModifier -= modifier.FloatValue;
                break;
            case ModifierType.SpellAddition:
                SpellData spellToAdd = entity.GetSpellViaKey(modifier.Value);
                
                if (spellToAdd == null)
                {
                    Debug.Log("No Spell key to remove found :" + modifier.Value);
                    break;
                }
                
                entity.RemoveSpellToSpellList(spellToAdd);
                break;
            default:
                Debug.LogError("MODIFIER EQUIPEMENT HAS NOT BEEN SET UP");
                break;
        }
    }
}
