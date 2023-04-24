using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

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
            case ModifierType.IncreaseMaxLife:
                entity.Life.ChangeMaxLifeValue(modifier.FloatValue);
                break;
            case ModifierType.SpellAddition:
                SpellData spellToAdd = SpellLibrary.Instance.GetSpellViaKey(modifier.Value);
                entity.AddSpellToSpellList(spellToAdd);
                break;
            case ModifierType.AddThrowRockPassif:
                Buff buff = BuffLibrary.Instance.AddBuffToViaKey(BuffType.RockThrowBuff, entity);
                buff.InitializeBuff(entity,entity,0,modifier.FloatValue);
                buff.SetBuffType(BuffGroup.Buff,BuffCooldown.Passive);
                buff.EnableVisual(false);
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
            case ModifierType.IncreaseMaxLife:
                entity.Life.ChangeMaxLifeValue(-modifier.FloatValue);
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
            case ModifierType.AddThrowRockPassif:
                entity.Buffs.TryRemovePassiveOffTypeAndValue(BuffType.RockThrowBuff,modifier.FloatValue);
                break;
            default:
                Debug.LogError("MODIFIER EQUIPEMENT HAS NOT BEEN SET UP");
                break;
        }
    }

    /*switch (modifier.Type)
    {
        case ModifierType.UpCold:
        case ModifierType.UpFire:
        case ModifierType.UpPhysical:
        case ModifierType.IncreaseMaxLife:
        case ModifierType.SpellAddition:
    }*/
    
    public static string GetModifierDescription(Modifier modifier)
    {
        switch (modifier.Type)
        {
            case ModifierType.UpCold:
                return "+" + modifier.Value + "% Cold damage";
            case ModifierType.UpFire:
                return "+" + modifier.Value + "% Fire damage";
            case ModifierType.UpPhysical:
                return "+" + modifier.Value + "% Physical damage";
            case ModifierType.IncreaseMaxLife:
                return "+" + modifier.Value + " Maximum life";
            case ModifierType.SpellAddition:
                return "Add " + modifier.Value + " spell";
            case ModifierType.AddThrowRockPassif:
                return "Throw Rock dealing " + modifier.Value + " damage";
            default:
                Debug.LogError("Modifier type has not been set up " + modifier.Type);
                return "Not Recognised";
        }
    }
    
    public static void GiveModifierBasedOnRarityAndType(EquipementItem equipementItem,int modifierCount,Tier targetTier)
    {
        if(modifierCount <= 0)
            return;
        
        ModifierPool targetPool = ModifierLibraryController.Instance.GetViaKey(targetTier,equipementItem.BaseEquipementData.EquipementType);

        Modifier[] modifiers = targetPool.Modifier.MultipleDraw(modifierCount).Select(m => m.RangeToModifier()).ToArray();
        equipementItem.SetAdditionalModifiers(modifiers);
    }

    private static Modifier RangeToModifier(this RangeModifier rangeModifier)
    {
        string modifierValue;
        
        switch (rangeModifier.Type)
        {
            //String type
            //Maybe need a split + random to have a range of random spell addition ?//
            case ModifierType.SpellAddition:
                modifierValue = rangeModifier.Params;
                break;
            default:
                modifierValue = ((int) Random.Range(rangeModifier.Range.x, rangeModifier.Range.y + 1)).ToString();
                break;
        }
        
        return new Modifier(rangeModifier.Type, modifierValue);
    }
}
