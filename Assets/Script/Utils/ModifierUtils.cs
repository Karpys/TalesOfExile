using System;
using System.Collections.Generic;
using System.Linq;
using KarpysDev.KarpysUtils;
using KarpysDev.Script.Entities;
using KarpysDev.Script.Entities.BuffRelated;
using KarpysDev.Script.Entities.EquipementRelated;
using KarpysDev.Script.Items;
using KarpysDev.Script.Manager.Library;
using KarpysDev.Script.Spell;
using UnityEngine;
using Random = UnityEngine.Random;

namespace KarpysDev.Script.Utils
{
    public static class ModifierUtils
    {
        private static readonly Dictionary<ModifierType, Action<Modifier, BoardEntity>> applyActions;
        private static readonly Dictionary<ModifierType, Action<Modifier, BoardEntity>> unapplyActions;
        private static readonly Dictionary<ModifierType, Func<Modifier,string>> descriptionModifiers;

        static ModifierUtils()
        {
            applyActions = new Dictionary<ModifierType, Action<Modifier, BoardEntity>>()
            {
                //Damage type
                {ModifierType.UpCold, (m, e) => e.EntityStats.DamageTypeModifier.ChangeColdValue(m.FloatValue)},
                {ModifierType.UpFire, (m, e) => e.EntityStats.DamageTypeModifier.ChangeFireValue(m.FloatValue)},
                {ModifierType.UpPhysical, (m, e) => e.EntityStats.DamageTypeModifier.ChangePhysicalValue(m.FloatValue)},
                {ModifierType.UpLightning, (m, e) => e.EntityStats.DamageTypeModifier.ChangeLightningValue(m.FloatValue)},
                //GlobalDamage
                {ModifierType.IncreaseWeaponForce, (m, e) => e.EntityStats.WeaponForce += m.FloatValue },
                //Resistance
                {ModifierType.UpColdResistance,(m,e)=> e.EntityStats.DamageTypeReduction.ChangeColdValue(m.FloatValue)},          
                {ModifierType.UpFireResistance,(m,e) => e.EntityStats.DamageTypeReduction.ChangeFireValue(m.FloatValue)},          
                {ModifierType.UpPhysicalResistance,(m, e) => e.EntityStats.DamageTypeReduction.ChangePhysicalValue(m.FloatValue)},  
                {ModifierType.UpLightningResistance, (m, e) => e.EntityStats.DamageTypeReduction.ChangeLightningValue(m.FloatValue)},
                {ModifierType.IncreaseMaxLife, (m, e) => e.Life.ChangeMaxLifeValue(m.FloatValue) },
                //Misc
                {ModifierType.CanUseBowTalent, (m, e) => e.EntityStats.IsBowUser += 1 },
                {ModifierType.SpellAddition, (m, e) =>
                    {
                        SpellInfo spellToAdd = SpellLibrary.Instance.GetSpellViaKey(m.Value,SpellLearnType.Equipement);
                        e.AddSpellToSpellList(spellToAdd);
                        e.UpdateSpellPriority();
                    }
                },
                {ModifierType.AddThrowRockPassif, (m, e) =>
                    {
                        //Todo:Get spell info via spell info dictionary//
                        e.Buffs.TryAddPassive(PassiveBuffType.RockThrowPassive,m.FloatValue,out bool added);

                        if (!added)
                        {
                            e.Buffs.AddPassive(new RockThrowBuff(e, e, BuffType.RockThrowBuff,BuffGroup.Buff,0,m.FloatValue,null),PassiveBuffType.RockThrowPassive);
                        }
                    }
                },
            };

            unapplyActions = new Dictionary<ModifierType, Action<Modifier, BoardEntity>>()
            {
                //Damage type
                {ModifierType.UpCold, (m, e) => e.EntityStats.DamageTypeModifier.ChangeColdValue(-m.FloatValue)},
                {ModifierType.UpFire, (m, e) => e.EntityStats.DamageTypeModifier.ChangeFireValue(-m.FloatValue)},
                {ModifierType.UpPhysical, (m, e) => e.EntityStats.DamageTypeModifier.ChangePhysicalValue(-m.FloatValue)},
                {ModifierType.UpLightning, (m, e) => e.EntityStats.DamageTypeModifier.ChangeLightningValue(-m.FloatValue)},
                //Global Damage
                {ModifierType.IncreaseWeaponForce, (m, e) => e.EntityStats.WeaponForce -= m.FloatValue },
                //Resistance
                {ModifierType.UpColdResistance,(m,e)=> e.EntityStats.DamageTypeReduction.ChangeColdValue(-m.FloatValue)},          
                {ModifierType.UpFireResistance,(m,e) => e.EntityStats.DamageTypeReduction.ChangeFireValue(-m.FloatValue)},          
                {ModifierType.UpPhysicalResistance,(m, e) => e.EntityStats.DamageTypeReduction.ChangePhysicalValue(-m.FloatValue)},  
                {ModifierType.UpLightningResistance, (m, e) => e.EntityStats.DamageTypeReduction.ChangeLightningValue(-m.FloatValue)},
                //Misc
                {ModifierType.CanUseBowTalent, (m, e) => e.EntityStats.IsBowUser -= 1 },
                {ModifierType.IncreaseMaxLife, (m, e) => e.Life.ChangeMaxLifeValue(-m.FloatValue)},
                {ModifierType.SpellAddition, (m, e) =>
                    {
                        TriggerSpellData spellToAdd = e.GetSpellViaKey(m.Value);
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
                        e.Buffs.TryRemovePassive(PassiveBuffType.RockThrowPassive,m.FloatValue);
                    }
                },
            };
        
            descriptionModifiers = new Dictionary<ModifierType, Func<Modifier, string>>()
            {
                //Damage Type
                { ModifierType.UpCold, modifier => $"+{modifier.Value}% Cold damage" },
                { ModifierType.UpFire, modifier => $"+{modifier.Value}% Fire damage" },
                { ModifierType.UpLightning, modifier => $"+{modifier.Value}% Lightning damage" },
                { ModifierType.UpPhysical, modifier => $"+{modifier.Value}% Physical damage" },
                { ModifierType.UpElemental, modifier => $"+{modifier.Value}% Elemental damage" },
                //Global Damage
                { ModifierType.IncreaseProjectileDamage, modifier => $"+{modifier.Value}% Projectile damage" },
                { ModifierType.IncreaseWeaponForce, modifier => $"Increase Weapon damage by {modifier.Value}" },
                { ModifierType.IncreaseSpellDamage, modifier => $"+{modifier.Value}% Spell damage" },
                //Resistance
                {ModifierType.UpColdResistance,modifier => $"+{modifier.Value}% Cold resistance"},
                {ModifierType.UpFireResistance,modifier =>$"+{modifier.Value}% Fire resistance"},
                {ModifierType.UpPhysicalResistance,modifier => $"+{modifier.Value}% Physical resistance"},
                {ModifierType.UpLightningResistance,modifier => $"+{modifier.Value}% Lightning resistance"},
                {ModifierType.UpElementResistance,modifier => $"+{modifier.Value}% Elemental resistance"},
                { ModifierType.IncreaseMaxLife, modifier => $"+{modifier.Value} Maximum Life" },
                //Misc
                { ModifierType.SpellAddition, modifier => $"Add {modifier.Value} spell" },
                { ModifierType.AddThrowRockPassif, modifier => $"Throw Rock dealing {modifier.Value} damage" },
                { ModifierType.CanUseBowTalent, _ => "Can use bow talent" },
            };
        }
        public static void ApplyModifier(Modifier modifier,BoardEntity entity)
        {
            if (applyActions.TryGetValue(modifier.Type, out var action))
            {
                action.Invoke(modifier,entity);
            }
            else
            {
                modifier.Type.LogError("Has not been set up");
            }
        }
    
        public static void UnapplyModifier(Modifier modifier, BoardEntity entity)
        {
            if (unapplyActions.TryGetValue(modifier.Type, out var action))
            {
                action.Invoke(modifier,entity);
            }
            else
            {
                modifier.Type.Log("Modifier has not been set up");
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
            if (descriptionModifiers.TryGetValue(modifier.Type, out var descriptionFunc))
            {
                return descriptionFunc(modifier);
            }
            else
            {
                Debug.LogError("Modifier type has not been set up: " + modifier.Type);
                return "Not Recognized";
            }
        }
    
        public static void GiveModifierBasedOnRarityAndType(EquipementItem equipementItem,int modifierCount)
        {
            if(modifierCount <= 0)
                return;
        
            ModifierPool targetPool = equipementItem.EquipementData.ModifierPool.m_ModifierPool;

            List<RangeModifier> rangeModifiers = targetPool.Modifier.MultipleDraw(modifierCount);

            int rangeModifierCount = rangeModifiers.Count;
            Modifier[] modifiers = new Modifier[rangeModifierCount];

            for (int i = 0; i < rangeModifierCount; i++)
            {
                modifiers[i] = rangeModifiers[i].RangeToModifier();
            }
            
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
}
