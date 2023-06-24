using System;
using System.Collections.Generic;
using System.Linq;
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
                {ModifierType.UpCold, (m, e) => e.EntityStats.ColdDamageModifier += m.FloatValue },
                {ModifierType.UpFire, (m, e) => e.EntityStats.FireDamageModifier += m.FloatValue },
                {ModifierType.UpPhysical, (m, e) => e.EntityStats.PhysicalDamageModifier += m.FloatValue },
                {ModifierType.UpLightning, (m, e) => e.EntityStats.LightningDamageModifier += m.FloatValue},
                {ModifierType.UpElemental, (m, e) => e.EntityStats.ElementalDamageModifier += m.FloatValue},
                //GlobalDamage
                {ModifierType.IncreaseProjectileDamage, (m, e) => e.EntityStats.ProjectileModifier += m.FloatValue},
                {ModifierType.IncreaseSpellDamage, (m, e) => e.EntityStats.SpellModifier += m.FloatValue},
                {ModifierType.IncreaseWeaponForce, (m, e) => e.EntityStats.WeaponForce += m.FloatValue },
                //Resistance
                {ModifierType.UpColdResistance,(m,e) => e.EntityStats.ColdDamageReduction += m.FloatValue},
                {ModifierType.UpFireResistance,(m,e) => e.EntityStats.FireDamageReduction += m.FloatValue},
                {ModifierType.UpPhysicalResistance,(m,e) => e.EntityStats.PhysicalDamageReduction += m.FloatValue},
                {ModifierType.UpLightningResistance,(m,e) => e.EntityStats.LightningDamageReduction += m.FloatValue},
                {ModifierType.UpElementResistance,(m,e) => e.EntityStats.ElementalDamageReduction += m.FloatValue},
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
                        Buff buff = BuffLibrary.Instance.AddBuffToViaKey(BuffType.RockThrowBuff, e);
                        buff.SetBuffCooldown(BuffCooldown.Passive);
                        buff.SetBuffKey((int)BuffType.RockThrowBuff + " " + m.Value);
                        buff.EnableVisual(false);
                        buff.InitializeAsPassive(e, e,m.FloatValue);
                    }
                },
            };

            unapplyActions = new Dictionary<ModifierType, Action<Modifier, BoardEntity>>()
            {
                //Damage type
                {ModifierType.UpCold, (m, e) => e.EntityStats.ColdDamageModifier -= m.FloatValue},
                {ModifierType.UpFire, (m, e) => e.EntityStats.FireDamageModifier -= m.FloatValue},
                {ModifierType.UpPhysical, (m, e) => e.EntityStats.PhysicalDamageModifier -= m.FloatValue},
                {ModifierType.UpLightning, (m, e) => e.EntityStats.LightningDamageModifier -= m.FloatValue},
                {ModifierType.UpElemental, (m, e) => e.EntityStats.ElementalDamageModifier -= m.FloatValue},
                //Global Damage
                {ModifierType.IncreaseProjectileDamage, (m, e) => e.EntityStats.ProjectileModifier -= m.FloatValue},
                {ModifierType.IncreaseSpellDamage, (m, e) => e.EntityStats.SpellModifier -= m.FloatValue},
                {ModifierType.IncreaseWeaponForce, (m, e) => e.EntityStats.WeaponForce -= m.FloatValue },
                //Resistance
                {ModifierType.UpColdResistance,(m,e) => e.EntityStats.ColdDamageReduction -= m.FloatValue},
                {ModifierType.UpFireResistance,(m,e) => e.EntityStats.FireDamageReduction -= m.FloatValue},
                {ModifierType.UpPhysicalResistance,(m,e) => e.EntityStats.PhysicalDamageReduction -= m.FloatValue},
                {ModifierType.UpLightningResistance,(m,e) => e.EntityStats.LightningDamageReduction -= m.FloatValue},
                {ModifierType.UpElementResistance,(m,e) => e.EntityStats.ElementalDamageReduction -= m.FloatValue},
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
                        e.Buffs.TryRemovePassive(BuffType.RockThrowBuff,m.FloatValue);
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
                Debug.LogError("MODIFIER HAS NOT BEEN SET UP");
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
}
