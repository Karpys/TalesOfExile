using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class DamageManager : SingletonMonoBehavior<DamageManager>
{
    public float TryDamageEnemy(BoardEntity damageTo, BoardEntity damageFrom,DamageSource damageSource)
    {
        return DamageStep(damageTo,damageFrom,damageSource);//Add DamageClass
    }

    private float DamageStep(BoardEntity damageTo,BoardEntity damageFrom,DamageSource damageSource)
    {
        //Do Something//
        //Do damage//
        damageTo.TakeDamage(damageSource.Damage);
        Debug.Log("Entity : " + damageTo.gameObject.name + " take :" + damageSource.Damage + " " + damageSource.DamageType + " damage");
        return -damageSource.Damage;
    }
}
