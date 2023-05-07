using System;
using UnityEngine;

public class BoardEntityEventHandler : MonoBehaviour
{
    public Action<IntSocket> OnRequestBlockSpell = null;
    public Action OnDeath = null;
    //TriggerSpellData can be null
    public Action<BoardEntity,DamageSource,TriggerSpellData> OnGetDamageFrom = null;
    public Action<BoardEntity,DamageSource,TriggerSpellData> OnDoDamageTo = null;
    public Action<TriggerSpellData> OnGetHit = null;
    public Action<CastInfo> OnSpellCast = null;

    public Action<DamageSpellTrigger,FloatSocket> OnRequestSpellDamage = null;
    public Action OnSpellRecompute = null;
    public Action OnBehave = null;
    public void TriggerGetDamageAction(BoardEntity damageFrom, DamageSource damageSource,TriggerSpellData spellDamage)
    {
        OnGetDamageFrom?.Invoke(damageFrom,damageSource,spellDamage);
    }

    public void TriggerDoDamageAction(BoardEntity damageTo, DamageSource damageSource,TriggerSpellData spellDamage)
    {
        OnDoDamageTo?.Invoke(damageTo,damageSource,spellDamage);
    }

    public void TriggerCastInfoEvent(CastInfo castInfo)
    {
        OnSpellCast?.Invoke(castInfo);
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

public class FloatSocket
{
    private float m_Value = 0;

    public float Value
    {
        get => m_Value;
        set => m_Value = value;
    }

    public FloatSocket(float baseValue)
    {
        m_Value = baseValue;
    }
    
    public FloatSocket()
    {
    }
}

