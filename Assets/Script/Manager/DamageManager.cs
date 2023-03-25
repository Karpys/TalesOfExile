using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class DamageManager : SingletonMonoBehavior<DamageManager>
{
    public float TryDamageEnemy(BoardEntity damageTo, BoardEntity damageFrom,DamageSource damageSource)
    {
        //Add Block//
        return DamageStep(damageTo,damageFrom,damageSource);//Add DamageClass
    }

    private float DamageStep(BoardEntity damageTo,BoardEntity damageFrom,DamageSource damageSource)
    {
        DamageSource mitigiedDamageSource = new DamageSource(damageSource);
        //Add Damage reduction class or Vector2Int => First Value brut reduction / Second Value PercentageReduction
        //Get That from DamageTo BoardEntityStats class reference//
        //Apply Multiplier / Divide on mitigiedDamageSource//
        //Trigger Event link to the damageSourceType//
        //Do Something//
        //Do damage//
        Debug.Log("Entity : " + damageTo.gameObject.name + " take :" + damageSource.Damage + " " + damageSource.DamageType + " damage");
        return mitigiedDamageSource.Damage;
    }
}
