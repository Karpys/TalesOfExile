using UnityEngine;

public class BuffGiverTrigger : SelectionSpellTrigger
{
    protected object[] m_BuffArgs = null;

    private BuffGroup m_BuffGroup = BuffGroup.Buff;
    private BuffType m_BuffType = BuffType.None;
    protected int m_BuffDuration = 0;
    protected float m_BuffValue = 0;
    public BuffGiverTrigger(BaseSpellTriggerScriptable baseScriptable,BuffGroup buffGroup,BuffType buffType,int buffDuration,float buffValue) : base(baseScriptable)
    {
        m_BuffGroup = buffGroup;
        m_BuffType = buffType;
        m_BuffDuration = buffDuration;
        m_BuffValue = buffValue;
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

    protected override EntityGroup GetEntityGroup(TriggerSpellData spellData)
    {
        if (m_BuffGroup == BuffGroup.Buff)
            return spellData.AttachedEntity.EntityGroup;

        return base.GetEntityGroup(spellData);
    }
}