using System.Collections.Generic;
using UnityEngine;

public class RockThrowBuff : Buff
{
    [SerializeField] private SpellData m_TriggerSpellData = null;
    private TriggerSpellData Trigger => m_TriggerSpellData as TriggerSpellData;
    private List<BoardEntity> m_RockReceiver = new List<BoardEntity>();
    protected override void Apply()
    {
        m_TriggerSpellData = m_Receiver.RegisterSpell(m_TriggerSpellData);
        m_Receiver.EntityEvent.OnPhysicalDamageDone += AddRockThrowCallBack;
    }

    private void AddRockThrowCallBack(BoardEntity _, BoardEntity receiver)
    {
        if(GameManager.Instance.AddCallBackAction(ThrowRocks))
            m_RockReceiver.Add(receiver);
    }

    private void ThrowRocks()
    {
        for (int i = 0; i < m_RockReceiver.Count; i++)
        {
            BoardEntity receiver = m_RockReceiver[i];
            
            if(receiver)
                m_Receiver.CastSpellAt(Trigger, receiver.EntityPosition);
        }

        m_RockReceiver.Clear();
    }

    protected override void UnApply()
    {
        m_Receiver.EntityEvent.OnPhysicalDamageDone -= AddRockThrowCallBack;
    }
}