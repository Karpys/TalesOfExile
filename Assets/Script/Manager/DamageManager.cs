using KarpysDev.Script.Entities;
using KarpysDev.Script.Spell;
using KarpysDev.Script.Spell.DamageSpell;
using UnityEngine;

namespace KarpysDev.Script.Manager
{
    public static class DamageManager
    {
        public static bool BlendDisplayDamage = true;

        public static float DamageStep(BoardEntity damageTo,DamageSource damageSource,MainDamageType mainDamageType,TriggerSpellData triggerSpellData,bool displayDamage, float displayDelay,float efficiency)
        {
            DamageSource mitigiedDamageSource = new DamageSource(damageSource);
        
            //x => Flat / y => Percentage//
            Vector2 damageReduction = damageTo.EntityStats.GetDamageReduction(mainDamageType, damageSource.DamageType);
            mitigiedDamageSource.Damage = (mitigiedDamageSource.Damage * efficiency - damageReduction.x) * (1 - damageReduction.y / 100);
        
            damageTo.EntityEvent.OnGetDamageFromSpell?.Invoke(triggerSpellData.AttachedEntity,mitigiedDamageSource,triggerSpellData);
            //DamageToOnDamageTaken//
            Debug.Log("Entity : " + damageTo.gameObject.name + " take :" + damageSource.Damage + " " + mitigiedDamageSource.DamageType + " damage");

            if (displayDamage && !BlendDisplayDamage)
            {
                FloatingTextManager.Instance.SpawnFloatingText(damageTo.WorldPosition,mitigiedDamageSource.Damage.ToString("0"),ColorLibraryManager.Instance.GetDamageColor(mitigiedDamageSource.DamageType),displayDelay);
            }
            
            return mitigiedDamageSource.Damage;
        }

        public static float HealTarget(BoardEntity entity, float healValue,bool displayText,float delay = 0)
        {
            //Todo : Add HealModifier ?
            entity.Life.ChangeLifeValue(healValue);
            if (!displayText) return healValue;
            
            FloatingTextManager.Instance.SpawnFloatingText(entity.WorldPosition,"+" + healValue.ToString("0"),ColorLibraryManager.Instance.GetHealColor(),delay);
            return healValue;
        }
        
        public static void DirectDamage(BoardEntity damageTo,DamageSource damageSource,MainDamageType mainDamageType,BoardEntity damagefrom = null,bool displayDamage = true, float displayDelay = 0)
        {
            DamageSource mitigiedDamageSource = new DamageSource(damageSource);
        
            //x => Flat / y => Percentage//
            Vector2 damageReduction = damageTo.EntityStats.GetDamageReduction(mainDamageType, damageSource.DamageType);
            mitigiedDamageSource.Damage = (mitigiedDamageSource.Damage - damageReduction.x) * (1 - damageReduction.y / 100);
        
            if (displayDamage)
            {
                FloatingTextManager.Instance.SpawnFloatingText(damageTo.WorldPosition,mitigiedDamageSource.Damage.ToString("0"),ColorLibraryManager.Instance.GetDamageColor(mitigiedDamageSource.DamageType),displayDelay);
            }

            if (damagefrom)
            {
                damageTo.TakeDamage(mitigiedDamageSource.Damage,damagefrom);
            }
            else
            {
                damageTo.TakeDamage(mitigiedDamageSource.Damage);
            }
        }

        public static float GetDamageModifier(DamageParameters damageParameters, EntityStats stats,float bonusModifier = 0)
        {
            float modifier = bonusModifier;

            foreach (SubDamageType subDamageType in damageParameters.DamageType.SubDamageTypes)
            {
                modifier += stats.GetDamageModifier(subDamageType);
            }

            modifier += stats.GetMainTypeModifier(damageParameters.DamageType.MainDamageType);

            return (modifier + 100) / 100;
        }
        
        public static float GetDamageModifier(SubDamageType[] subDamage, EntityStats stats)
        {
            float modifier = 0;

            foreach (SubDamageType subDamageType in subDamage)
            {
                modifier += stats.GetDamageModifier(subDamageType);
            }
            
            return (modifier + 100) / 100;
        }
        
        public static float GetDamageModifier(MainDamageType mainDamageType,SubDamageType[] subDamage, EntityStats stats)
        {
            float modifier = 0;

            foreach (SubDamageType subDamageType in subDamage)
            {
                modifier += stats.GetDamageModifier(subDamageType);
            }
            
            modifier += stats.GetMainTypeModifier(mainDamageType);
            
            return (modifier + 100) / 100;
        }

        public static string ToDescription(this DamageSource damageSource)
        {
            //Todo: Add Damage color//
            return Mathf.Floor(damageSource.Damage) + " " + damageSource.DamageType + " damage";
        }
    }
}
