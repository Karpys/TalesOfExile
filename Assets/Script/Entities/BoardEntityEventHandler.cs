using System;
using UnityEngine;

public class BoardEntityEventHandler : MonoBehaviour
{
    public Action<IntSocket> OnRequestBlockSpell = null;
    public Action OnDeath = null;
    public Action<BoardEntity,DamageSource> OnGetDamageFrom = null;
    public Action<BoardEntity,DamageSource> OnDoDamageTo = null;
    public Action<BoardEntity> OnSpellRecompute = null;
    public void TriggerGetDamageAction(BoardEntity damageFrom, DamageSource damageSource)
    {
        OnGetDamageFrom?.Invoke(damageFrom,damageSource);
    }

    public void TriggerDoDamageAction(BoardEntity damageTo, DamageSource damageSource)
    {
        OnDoDamageTo?.Invoke(damageTo,damageSource);
    }
}

public class IntSocket
{
    private int m_Value = 0;

    public int Value
    {
        get => m_Value;
        set => m_Value = value;
    }

    public IntSocket(int baseValue)
    {
        m_Value = baseValue;
    }
}
