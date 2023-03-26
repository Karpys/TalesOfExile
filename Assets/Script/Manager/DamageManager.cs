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
        
        //CALL BACK DAMAGE DONE SYSTEM TEST//
        if(mitigiedDamageSource.DamageType == SubDamageType.Physical)
            damageFrom.EntityEvent.OnPhysicalDamageDone?.Invoke(damageFrom,damageTo);
        //Add Damage reduction class or Vector2Int => First Value brut reduction / Second Value PercentageReduction
        //Get That from DamageTo BoardEntityStats class reference//
        //Apply Multiplier / Divide on mitigiedDamageSource//
        //Trigger Event link to the damageSourceType//
        Debug.Log("Entity : " + damageTo.gameObject.name + " take :" + damageSource.Damage + " " + damageSource.DamageType + " damage");
        return mitigiedDamageSource.Damage;
    }
}
