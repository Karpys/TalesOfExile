using KarpysDev.Script.Entities;
using KarpysDev.Script.Spell;
using KarpysDev.Script.Spell.DamageSpell;
using UnityEngine;

namespace KarpysDev.Script.Manager
{
    public class DamageManager : SingletonMonoBehavior<DamageManager>
    {
        public float TryDamageEnemy(BoardEntity damageTo,DamageSource damageSource,MainDamageType mainDamageType,TriggerSpellData triggerSpellData)
        {
            //Add Block//
            return DamageStep(damageTo,damageSource,mainDamageType,triggerSpellData);//Add DamageClass
        }

        private float DamageStep(BoardEntity damageTo,DamageSource damageSource,MainDamageType mainDamageType,TriggerSpellData triggerSpellData)
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
    }
}
