using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RockThrowBuff : Buff
{
    [SerializeField] private SpellInfo m_SpellInfo = null;
    
    private TriggerSpellData m_TriggerSpellData = null;
    private List<Vector2Int> m_RockReceiver = new List<Vector2Int>();
    
    protected override void Apply()
    {
        m_TriggerSpellData = m_Receiver.RegisterSpell(m_SpellInfo) as TriggerSpellData;
        
        ((DamageSpellTrigger)m_TriggerSpellData.SpellTrigger).SetInitialDamageSource(m_BuffValue);
        m_TriggerSpellData.SpellTrigger.ComputeSpellData(m_Receiver);

        m_Receiver.EntityEvent.OnRequestCastEvent += AddThrowCallBack;
        m_Receiver.ComputeAllSpells();
        
        m_Receiver.EntityEvent.OnSpellRecompute += Recompute;
    }

    private void AddThrowCallBack(BaseSpellTrigger spell)
    {
        if (spell.SpellData.Data.SpellGroups.Contains(SpellGroup.Rock))
        {
            spell.OnCastSpell += ThrowRocks;
        }
    }

    private void ThrowRocks(CastInfo castInfo)
    {
        if (!(castInfo is DamageCastInfo damageCastInfo)) return;
        
        GameManager.Instance.AddCallBackAction(ThrowRocks);
        m_RockReceiver.AddRange(damageCastInfo.HitEntity.Where(en => !ReferenceEquals(en, null)).Select(en => en.EntityPosition).ToList());
    }
    
    
    private void ThrowRocks()
    {
        for (int i = 0; i < m_RockReceiver.Count; i++)
        {
            Vector2Int receiver = m_RockReceiver[i];
            
            SpellCastUtils.CastSpellAt(m_TriggerSpellData, receiver,m_Receiver.EntityPosition);
        }

        m_RockReceiver.Clear();
    }

    protected override void UnApply()
    {
        m_Receiver.EntityEvent.OnRequestCastEvent -= AddThrowCallBack;
        m_Receiver.ComputeAllSpells();
        
        m_Receiver.EntityEvent.OnSpellRecompute -= Recompute;
    }

    private void Recompute()
    {
        m_TriggerSpellData.SpellTrigger.ComputeSpellData(m_Receiver);
    }
}
