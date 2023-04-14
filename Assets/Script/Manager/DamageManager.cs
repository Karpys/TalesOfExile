using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class DamageManager : SingletonMonoBehavior<DamageManager>
{
    public float TryDamageEnemy(BoardEntity damageTo, BoardEntity damageFrom,DamageSource damageSource,MainDamageType mainDamageType,TriggerSpellData triggerSpellData = null)
    {
        //Add Block//
        return DamageStep(damageTo,damageFrom,damageSource,mainDamageType,triggerSpellData);//Add DamageClass
    }

    private float DamageStep(BoardEntity damageTo,BoardEntity damageFrom,DamageSource damageSource,MainDamageType mainDamageType,TriggerSpellData triggerSpellData = null)
    {
        DamageSource mitigiedDamageSource = new DamageSource(damageSource);
        
        //x => Flat / y => Percentage//
        Vector2 damageReduction = damageTo.EntityStats.GetDamageReduction(mainDamageType, damageSource.DamageType);
        mitigiedDamageSource.Damage = (mitigiedDamageSource.Damage - damageReduction.x) * (1 - damageReduction.y / 100);

        damageTo.EntityEvent.TriggerGetDamageAction(damageFrom,damageSource,triggerSpellData);
        damageFrom.EntityEvent.TriggerDoDamageAction(damageTo,damageSource,triggerSpellData);
        
        Debug.Log("Entity : " + damageTo.gameObject.name + " take :" + damageSource.Damage + " " + mitigiedDamageSource.DamageType + " damage");
        return mitigiedDamageSource.Damage;
    }
}
