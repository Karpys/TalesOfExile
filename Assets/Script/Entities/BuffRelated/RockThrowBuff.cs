using System.Collections.Generic;
using System.Linq;
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

        m_Receiver.EntityEvent.OnSpellCast += AddRockThrowCallBack;
        m_Receiver.EntityEvent.OnSpellRecompute += Recompute;
    }


    private void AddRockThrowCallBack(CastInfo castInfo)
    {
        if (castInfo.SpellCasted.Data.SpellGroups.Contains(SpellGroup.Rock))
        {
            foreach (BoardEntity boardEntity in castInfo.HitEntity)
            {
                if (!ReferenceEquals(boardEntity, null))
                {
                    if(GameManager.Instance.AddCallBackAction(ThrowRocks))
                        m_RockReceiver.Add(boardEntity.EntityPosition);
                }
            }
        }
    }
    
    private void ThrowRocks()
    {
        for (int i = 0; i < m_RockReceiver.Count; i++)
        {
            Vector2Int receiver = m_RockReceiver[i];
            
            SpellCastUtils.CastSpellAt(Trigger, receiver,m_Receiver.EntityPosition);
        }

        m_RockReceiver.Clear();
    }

    protected override void UnApply()
    {
        m_Receiver.EntityEvent.OnSpellCast -= AddRockThrowCallBack;
        m_Receiver.EntityEvent.OnSpellRecompute -= Recompute;
    }

    private void Recompute()
    {
        Trigger.SpellTrigger.ComputeSpellData(m_Receiver);
    }
}
