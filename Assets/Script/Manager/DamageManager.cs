using KarpysDev.Script.Entities;
using KarpysDev.Script.Spell;
using KarpysDev.Script.Spell.DamageSpell;
using UnityEngine;

namespace KarpysDev.Script.Manager
{
    public static class DamageManager
    {
        public static bool BlendDisplayDamage = true;

        public static float DamageStep(BoardEntity damageTo,DamageSource damageSource,MainDamageType mainDamageType,TriggerSpellData triggerSpellData,bool displayDamage, float displayDelay)
        {
            DamageSource mitigiedDamageSource = new DamageSource(damageSource);
        
            //x => Flat / y => Percentage//
            Vector2 damageReduction = damageTo.EntityStats.GetDamageReduction(mainDamageType, damageSource.DamageType);
            mitigiedDamageSource.Damage = (mitigiedDamageSource.Damage - damageReduction.x) * (1 - damageReduction.y / 100);
        
            damageTo.EntityEvent.OnGetDamageFromSpell?.Invoke(triggerSpellData.AttachedEntity,mitigiedDamageSource,triggerSpellData);
            //DamageToOnDamageTaken//
            Debug.Log("Entity : " + damageTo.gameObject.name + " take :" + damageSource.Damage + " " + mitigiedDamageSource.DamageType + " damage");

            if (displayDamage && !BlendDisplayDamage)
            {
                FloatingTextManager.Instance.SpawnFloatingText(damageTo.WorldPosition,mitigiedDamageSource.Damage,ColorLibraryManager.Instance.GetDamageColor(mitigiedDamageSource.DamageType),displayDelay);
            }
            
            return mitigiedDamageSource.Damage;
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

        public static string ToDescription(this DamageSource damageSource)
        {
            //Todo: Add Damage color//
            return Mathf.Floor(damageSource.Damage) + " " + damageSource.DamageType + " damage";
        }
    }
}
