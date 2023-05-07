using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpellTrigger
{
    protected float m_SpellAnimDelay = 0;
    protected int m_SpellPriority = 0;

    protected SpellData m_AttachedSpell = null;
    //Cooldown Part//
    public int SpellPriority => GetSpellPriority();
    protected virtual int GetSpellPriority()
    {
        Debug.Log("Get Spell Prio");
        return m_SpellPriority;
    }
    
    public void SetAttachedSpell(SpellData spellData,int priority)
    {
        m_SpellPriority = priority;
        m_AttachedSpell = spellData;
    }

    public void CastSpell(TriggerSpellData spellData,SpellTiles spellTiles)
    {
        CastInfo castInfo = new CastInfo(spellData);
        Trigger(spellData,spellTiles,castInfo);
        spellData.AttachedEntity.EntityEvent.TriggerCastInfoEvent(castInfo);
    }
    public abstract void Trigger(TriggerSpellData spellData,SpellTiles spellTiles,CastInfo castInfo);

    public abstract void ComputeSpellData(BoardEntity entity);
}

public class CastInfo
{
    private List<BoardEntity> m_HitEntity = new List<BoardEntity>();
    private SpellData m_SpellCasted = null;

    public SpellData SpellCasted => m_SpellCasted;
    public List<BoardEntity> HitEntity => m_HitEntity;
    public void AddHitEntity(BoardEntity boardEntity)
    {
        m_HitEntity.Add(boardEntity);
    }

    public CastInfo(SpellData spellCasted)
    {
        m_SpellCasted = spellCasted;
    }
}