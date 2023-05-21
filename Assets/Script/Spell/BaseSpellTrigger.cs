using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpellTrigger
{
    protected float m_SpellAnimDelay = 0;
    protected int m_SpellPriority = 0;

    protected SpellData m_AttachedSpell = null;

    public Action<CastInfo> OnCastSpell = null;
    //Cooldown Part//
    public int SpellPriority => GetSpellPriority();
    public SpellData SpellData => m_AttachedSpell;
    protected virtual int GetSpellPriority()
    {
        return m_SpellPriority;
    }
    
    public void SetAttachedSpell(SpellData spellData,int priority)
    {
        m_SpellPriority = priority;
        m_AttachedSpell = spellData;
    }

    public virtual void CastSpell(TriggerSpellData spellData,SpellTiles spellTiles)
    {
        CastInfo castInfo = GetCastInfo(spellData);
        
        Trigger(spellData,spellTiles,castInfo);
        OnCastSpell?.Invoke(castInfo);
    }

    protected virtual CastInfo GetCastInfo(TriggerSpellData spellData)
    {
        if (OnCastSpell != null)
        {
            return new CastInfo(spellData);
        }

        return null;
    }
    public abstract void Trigger(TriggerSpellData spellData,SpellTiles spellTiles,CastInfo castInfo);

    public abstract void ComputeSpellData(BoardEntity entity);
}

public class CastInfo
{
    private SpellData m_SpellCasted = null;
    public SpellData SpellCasted => m_SpellCasted;

    public CastInfo(SpellData spellCasted)
    {
        m_SpellCasted = spellCasted;
    }
    
    public virtual void AddHitEntity(BoardEntity boardEntity)
    {
    }
}

public class DamageCastInfo:CastInfo
{
    private List<BoardEntity> m_HitEntity = new List<BoardEntity>();
    public List<BoardEntity> HitEntity => m_HitEntity;

    public override void AddHitEntity(BoardEntity boardEntity)
    {
        m_HitEntity.Add(boardEntity);
    }

    public DamageCastInfo(SpellData spellCasted) : base(spellCasted)
    {}
}