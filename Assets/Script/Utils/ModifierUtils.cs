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
            case ModifierType.IncreaseMaxLife:
                entity.Life.ChangeMaxLifeValue(modifier.FloatValue);
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
            case ModifierType.IncreaseMaxLife:
                entity.Life.ChangeMaxLifeValue(-modifier.FloatValue);
                break;
            default:
                Debug.LogError("MODIFIER EQUIPEMENT HAS NOT BEEN SET UP");
                break;
        }
    }

    //Need to be call with a modifier count
    //Cant draww same modifier twice
    public static void GiveModifier(EquipementObject equipementObject, Tier targetTier)
    {
        ModifierPool targetPool = ModifierLibraryController.Instance.GetViaKey(targetTier);
        RangeModifier rangedModifier = targetPool.Modifier.Draw();
        
        //Add Switch Case for non float/int modifier like spell addition
        Modifier[] modifiers = new Modifier[1];
        
        string modifierValue = ((int) Random.Range(rangedModifier.Range.x, rangedModifier.Range.y + 1)).ToString();
        modifiers[0] = new Modifier(rangedModifier.Type, modifierValue);
        equipementObject.SetAdditionalModifiers(modifiers);
    }
}
