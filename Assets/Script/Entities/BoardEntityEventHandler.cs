using System;
using UnityEngine;

public class BoardEntityEventHandler : MonoBehaviour
{
    public Action<IntSocket> OnRequestBlockSpell = null;
    public Action OnDeath = null;
    //TriggerSpellData can be null
    public Action<BoardEntity,DamageSource,TriggerSpellData> OnGetDamageFrom = null;
    public Action<BoardEntity,DamageSource,TriggerSpellData> OnDoDamageTo = null;
    public Action<BoardEntity> OnSpellRecompute = null;
    public Action OnBehave = null;
    public void TriggerGetDamageAction(BoardEntity damageFrom, DamageSource damageSource,TriggerSpellData spellDamage)
    {
        OnGetDamageFrom?.Invoke(damageFrom,damageSource,spellDamage);
    }

    public void TriggerDoDamageAction(BoardEntity damageTo, DamageSource damageSource,TriggerSpellData spellDamage)
    {
        OnDoDamageTo?.Invoke(damageTo,damageSource,spellDamage);
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
