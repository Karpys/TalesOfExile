using UnityEngine;

public class BuffGiverTrigger : SelectionSpellTrigger
{
    protected object[] m_BuffArgs = null;
    
    private BuffType m_BuffType = BuffType.None;
    private int m_BuffDuration = 0;
    private float m_BuffValue = 0;
    public BuffGiverTrigger(BaseSpellTriggerScriptable baseScriptable,BuffType buffType,int buffDuration,float buffValue) : base(baseScriptable)
    {
        m_BuffType = buffType;
        m_BuffDuration = buffDuration;
        m_BuffValue = buffValue;
    }

    public override void ComputeSpellPriority()
    {
        m_SpellPriority = (int)m_BuffValue;
    }

    protected override void EntityHit(BoardEntity entity, TriggerSpellData spellData, EntityGroup targetGroup, Vector2Int spellOrigin)
    {
        base.EntityHit(entity, spellData, targetGroup, spellOrigin);
        GiveBuff(spellData.AttachedEntity,entity,m_BuffArgs);
    }

    private void GiveBuff(BoardEntity caster,BoardEntity receiver,object[] args = null)
    {
        BuffLibrary.Instance.AddBuffToViaKey(m_BuffType, receiver).InitializeBuff(caster, receiver, m_BuffDuration, m_BuffValue,args);
    }
}