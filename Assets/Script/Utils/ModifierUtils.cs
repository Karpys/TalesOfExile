using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public static class ModifierUtils
{
    private static readonly Dictionary<ModifierType, Action<Modifier, BoardEntity>> applyActions;
    private static readonly Dictionary<ModifierType, Action<Modifier, BoardEntity>> unapplyActions;

    static ModifierUtils()
    {
        applyActions = new Dictionary<ModifierType, Action<Modifier, BoardEntity>>()
        {
            {ModifierType.UpCold, (m, e) => e.EntityStats.ColdDamageModifier += m.FloatValue },
            {ModifierType.UpFire, (m, e) => e.EntityStats.FireDamageModifier += m.FloatValue },
            {ModifierType.UpPhysical, (m, e) => e.EntityStats.PhysicalDamageModifier += m.FloatValue },
            {ModifierType.IncreaseSpellDamage, (m, e) => e.EntityStats.SpellModifier += m.FloatValue},
            {ModifierType.IncreaseWeaponForce, (m, e) => e.EntityStats.WeaponForce += m.FloatValue },
            {ModifierType.CanUseBowTalent, (m, e) => e.EntityStats.IsBowUser += 1 },
            {ModifierType.IncreaseMaxLife, (m, e) => e.Life.ChangeMaxLifeValue(m.FloatValue) },
            {ModifierType.SpellAddition, (m, e) =>
                {
                    SpellInfo spellToAdd = SpellLibrary.Instance.GetSpellViaKey(m.Value);
                    e.AddSpellToSpellList(spellToAdd);
                    e.UpdateSpellPriority();
                }
            },
            {ModifierType.AddThrowRockPassif, (m, e) =>
                {
                    Buff buff = BuffLibrary.Instance.AddBuffToViaKey(BuffType.RockThrowBuff, e);
                    buff.InitializeBuff(e, e, 0, m.FloatValue);
                    buff.SetBuffType(BuffGroup.Buff, BuffCooldown.Passive);
                    buff.SetBuffKey((int)BuffType.RockThrowBuff + " " + m.Value);
                    buff.EnableVisual(false);
                }
            },
        };

        unapplyActions = new Dictionary<ModifierType, Action<Modifier, BoardEntity>>()
        {
            {ModifierType.UpCold, (m, e) => e.EntityStats.ColdDamageModifier -= m.FloatValue},
            {ModifierType.UpFire, (m, e) => e.EntityStats.FireDamageModifier -= m.FloatValue},
            {ModifierType.UpPhysical, (m, e) => e.EntityStats.PhysicalDamageModifier -= m.FloatValue},
            {ModifierType.IncreaseSpellDamage, (m, e) => e.EntityStats.SpellModifier -= m.FloatValue},
            {ModifierType.IncreaseWeaponForce, (m, e) => e.EntityStats.WeaponForce -= m.FloatValue },
            {ModifierType.CanUseBowTalent, (m, e) => e.EntityStats.IsBowUser -= 1 },
            {ModifierType.IncreaseMaxLife, (m, e) => e.Life.ChangeMaxLifeValue(-m.FloatValue)},
            {ModifierType.SpellAddition, (m, e) =>
                {
                    SpellInfo spellToAdd = e.GetSpellViaKey(m.Value);
                    if (spellToAdd == null)
                    {
                        Debug.Log("No Spell key to remove found :" + m.Value);
                        return;
                    }

                    e.RemoveSpellToSpellList(spellToAdd);
                    e.UpdateSpellPriority();
                }
            },
            {ModifierType.AddThrowRockPassif, (m, e) =>
                {
                    e.Buffs.TryRemoveBuffViaKey((int) BuffType.RockThrowBuff + " " + m.Value);
                }
            },
        };
    }
    public static void ApplyModifier(Modifier modifier,BoardEntity entity)
    {
        if (applyActions.TryGetValue(modifier.Type, out Action<Modifier, BoardEntity> action))
        {
            action.Invoke(modifier,entity);
        }
        else
        {
            Debug.LogError("MODIFIER HAS NOT BEEN SET UP");
        }
    }
    
    public static void UnapplyModifier(Modifier modifier, BoardEntity entity)
    {
        if (unapplyActions.TryGetValue(modifier.Type, out Action<Modifier, BoardEntity> action))
        {
            action.Invoke(modifier,entity);
        }
        else
        {
            Debug.LogError("MODIFIER HAS NOT BEEN SET UP");
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
            case ModifierType.IncreaseWeaponForce:
                return "Increase weapon damage by " + modifier.Value;
            case ModifierType.IncreaseMaxLife:
                return "+" + modifier.Value + " Maximum Life";
            case ModifierType.SpellAddition:
                return "Add " + modifier.Value + " spell";
            case ModifierType.AddThrowRockPassif:
                return "Throw Rock dealing " + modifier.Value + " damage";
            case ModifierType.CanUseBowTalent:
                return "Can use bow talent";
            case ModifierType.IncreaseSpellDamage:
                return "+" + modifier.Value + "% Spell damage";
            default:
                Debug.LogError("Modifier type has not been set up " + modifier.Type);
                return "Not Recognised";
        }
    }
    
    public static void GiveModifierBasedOnRarityAndType(EquipementItem equipementItem,int modifierCount)
    {
        if(modifierCount <= 0)
            return;
        
        ModifierPool targetPool = equipementItem.EquipementData.ModifierPool.m_ModifierPool;

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
