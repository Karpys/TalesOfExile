using System.Collections.Generic;
using System.Linq;
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
    public static void GiveModifierBasedOnRarity(EquipementObject equipementObject,int modifierCount,Tier targetTier)
    {
        if(modifierCount <= 0)
            return;
        
        ModifierPool targetPool = ModifierLibraryController.Instance.GetViaKey(targetTier);

        Modifier[] modifiers = targetPool.Modifier.MultipleDraw(modifierCount).Select(m => m.RangeToModifier()).ToArray();
        equipementObject.SetAdditionalModifiers(modifiers);
    }

    public static Modifier RangeToModifier(this RangeModifier rangeModifier)
    {
        //Add Switch Case for non float/int modifier like spell addition
        string modifierValue = ((int) Random.Range(rangeModifier.Range.x, rangeModifier.Range.y + 1)).ToString();
        return new Modifier(rangeModifier.Type, modifierValue);
    }
}
