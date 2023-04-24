using System.Collections.Generic;
using UnityEngine;

public class RockThrowBuff : Buff
{
    [SerializeField] private SpellData m_TriggerSpellData = null;
    private TriggerSpellData Trigger => m_TriggerSpellData as TriggerSpellData;
    private List<Vector2Int> m_RockReceiver = new List<Vector2Int>();
    
    protected override void Apply()
    {
        m_TriggerSpellData = m_Receiver.RegisterSpell(m_TriggerSpellData);
        
        ((DamageSpellTrigger)Trigger.SpellTrigger).SetInitialDamageSource(m_BuffValue);
        Trigger.SpellTrigger.ComputeSpellData(m_Receiver);
        
        m_Receiver.EntityEvent.OnDoDamageTo += AddRockThrowCallBack;
        m_Receiver.EntityEvent.OnSpellRecompute += Trigger.SpellTrigger.ComputeSpellData;
    }

    private void AddRockThrowCallBack(BoardEntity receiver,DamageSource damageSource,TriggerSpellData _)
    {
        if(damageSource.DamageType != SubDamageType.Physical)
            return;
        
        if(GameManager.Instance.AddCallBackAction(ThrowRocks))
            m_RockReceiver.Add(receiver.EntityPosition);
    }

    private void ThrowRocks()
    {
        for (int i = 0; i < m_RockReceiver.Count; i++)
        {
            Vector2Int receiver = m_RockReceiver[i];
            
            m_Receiver.CastSpellAt(Trigger, receiver);
        }

        m_RockReceiver.Clear();
    }

    protected override void UnApply()
    {
        m_Receiver.EntityEvent.OnDoDamageTo -= AddRockThrowCallBack;
        m_Receiver.EntityEvent.OnSpellRecompute -= Trigger.SpellTrigger.ComputeSpellData;
    }
}
