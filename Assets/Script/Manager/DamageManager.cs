using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class DamageManager : SingletonMonoBehavior<DamageManager>
{
    public void TryDamageEnnemy(BoardEntity damageTo, BoardEntity damageFrom,DamageSource damageSource)
    {
        DamageStep(damageTo,damageFrom,damageSource);//Add DamageClass
    }

    private void DamageStep(BoardEntity damageTo,BoardEntity damageFrom,DamageSource damageSource)
    {
        //Do Something//
        //Do damage//
        damageTo.ChangeLifeValue(-damageSource.Damage);
        Debug.Log("Entity : " + damageTo.gameObject.name + " take :" + damageSource.Damage + " " + damageSource.DamageType + " damage");
    }
}
