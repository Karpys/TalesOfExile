using KarpysDev.Script.Entities;
using KarpysDev.Script.Spell;
using KarpysDev.Script.Spell.DamageSpell;
using UnityEngine;

namespace KarpysDev.Script.Manager
{
    public static class DamageManager
    {
        public static float TryDamageEnemy(BoardEntity damageTo,DamageSource damageSource,MainDamageType mainDamageType,TriggerSpellData triggerSpellData)
        {
            //Add Block//
            return DamageStep(damageTo,damageSource,mainDamageType,triggerSpellData);//Add DamageClass
        }

        private static float DamageStep(BoardEntity damageTo,DamageSource damageSource,MainDamageType mainDamageType,TriggerSpellData triggerSpellData)
        {
            DamageSource mitigiedDamageSource = new DamageSource(damageSource);
        
            //x => Flat / y => Percentage//
            Vector2 damageReduction = damageTo.EntityStats.GetDamageReduction(mainDamageType, damageSource.DamageType);
            mitigiedDamageSource.Damage = (mitigiedDamageSource.Damage - damageReduction.x) * (1 - damageReduction.y / 100);
        
            damageTo.EntityEvent.OnGetDamageFromSpell?.Invoke(triggerSpellData.AttachedEntity,mitigiedDamageSource,triggerSpellData);
            //DamageToOnDamageTaken//
            Debug.Log("Entity : " + damageTo.gameObject.name + " take :" + damageSource.Damage + " " + mitigiedDamageSource.DamageType + " damage");
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
    }
}
