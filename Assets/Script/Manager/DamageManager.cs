using KarpysDev.Script.Entities;
using KarpysDev.Script.Spell.DamageSpell;
using UnityEngine;

namespace KarpysDev.Script.Manager
{
    public class DamageManager : SingletonMonoBehavior<DamageManager>
    {
        public float TryDamageEnemy(BoardEntity damageTo,DamageSource damageSource,MainDamageType mainDamageType)
        {
            //Add Block//
            return DamageStep(damageTo,damageSource,mainDamageType);//Add DamageClass
        }

        private float DamageStep(BoardEntity damageTo,DamageSource damageSource,MainDamageType mainDamageType)
        {
            DamageSource mitigiedDamageSource = new DamageSource(damageSource);
        
            //x => Flat / y => Percentage//
            Vector2 damageReduction = damageTo.EntityStats.GetDamageReduction(mainDamageType, damageSource.DamageType);
            mitigiedDamageSource.Damage = (mitigiedDamageSource.Damage - damageReduction.x) * (1 - damageReduction.y / 100);
        
            Debug.Log("Entity : " + damageTo.gameObject.name + " take :" + damageSource.Damage + " " + mitigiedDamageSource.DamageType + " damage");
            return mitigiedDamageSource.Damage;
        }
    }
}
